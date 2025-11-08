--------------------------------------------------------
--  DROPS (con manejo de errores)
--------------------------------------------------------
BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER COLLECT_BI_SET_ID';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER COLLECT_BI_VALIDATE_AND_SET';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER COLLECT_BI_CALCULATE_AMOUNT';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER COLLECT_BU_VALIDATE_PAYMENT';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER PAYMENT_BI_SET_ID';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TRIGGER PAYMENT_AI_UPDATE_TOTAL';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP SEQUENCE PAYMENT_SEQ';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE COLLECT CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE PAYMENT CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE COLLECTOR_HARVEST_SEQ CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE COLLECT_STATUS_DOMAIN CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

BEGIN
   EXECUTE IMMEDIATE 'DROP TABLE PAYMENT_STATUS_DOMAIN CASCADE CONSTRAINTS';
EXCEPTION WHEN OTHERS THEN NULL;
END;
/

--------------------------------------------------------
-- DOMINIO DE ESTADO DE PAGO
--------------------------------------------------------
CREATE TABLE PAYMENT_STATUS_DOMAIN (
    STATUS_ID   NUMBER       NOT NULL,
    STATUS_NAME VARCHAR2(15) NOT NULL
);

ALTER TABLE PAYMENT_STATUS_DOMAIN
  ADD CONSTRAINT PAYMENT_STATUS_PK PRIMARY KEY (STATUS_ID) ENABLE;

INSERT INTO PAYMENT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (1, 'PENDIENTE');
INSERT INTO PAYMENT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (2, 'PAGADO');
INSERT INTO PAYMENT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (3, 'ANULADO');

--------------------------------------------------------
-- SECUENCIA GLOBAL PARA PAYMENT
--------------------------------------------------------
CREATE SEQUENCE PAYMENT_SEQ START WITH 1 INCREMENT BY 1 NOCACHE;

--------------------------------------------------------
-- TABLA PAYMENT (FACTURA/PAGO)
--------------------------------------------------------
CREATE TABLE PAYMENT (
    IDPAYMENT       NUMBER(10,0)  NOT NULL,
    WORKER_CODE     VARCHAR2(6)   NOT NULL,
    PAYMENT_DATE    DATE          NOT NULL,
    TOTAL_AMOUNT    NUMBER(12,2)  NOT NULL,
    STATUS_ID       NUMBER        NOT NULL,
    NOTES           VARCHAR2(500)
);

ALTER TABLE PAYMENT
  ADD CONSTRAINT PAYMENT_PK PRIMARY KEY (IDPAYMENT) ENABLE;

ALTER TABLE PAYMENT
  ADD CONSTRAINT PAYMENT_FK_COLLECTOR 
  FOREIGN KEY (WORKER_CODE)
  REFERENCES COLLECTOR (WORKER_CODE) ENABLE;

ALTER TABLE PAYMENT
  ADD CONSTRAINT PAYMENT_FK_STATUS 
  FOREIGN KEY (STATUS_ID)
  REFERENCES PAYMENT_STATUS_DOMAIN (STATUS_ID) ENABLE;

ALTER TABLE PAYMENT
  ADD CONSTRAINT PAYMENT_CHK_AMOUNT 
  CHECK (TOTAL_AMOUNT >= 0) ENABLE;

--------------------------------------------------------
-- DOMINIO DE ESTADO DE LA RECOLECTA
--------------------------------------------------------
CREATE TABLE COLLECT_STATUS_DOMAIN (
    STATUS_ID   NUMBER       NOT NULL,
    STATUS_NAME VARCHAR2(15) NOT NULL
);

ALTER TABLE COLLECT_STATUS_DOMAIN
  ADD CONSTRAINT COLLECT_STATUS_PK PRIMARY KEY (STATUS_ID) ENABLE;

INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (0, 'ZERO');
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (1, 'REGISTRADO');
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (2, 'PAGADO');

--------------------------------------------------------
-- TABLA AUXILIAR: Secuencia de COLLECT
--------------------------------------------------------
CREATE TABLE COLLECTOR_HARVEST_SEQ (
    WORKER_CODE VARCHAR2(6)   NOT NULL,
    IDPLOT      NUMBER(10,0)  NOT NULL,
    IDHARVEST   NUMBER(10,0)  NOT NULL,
    NEXT_ID     NUMBER(10,0)  NOT NULL,
    CONSTRAINT COLLECTOR_HARVEST_SEQ_PK 
      PRIMARY KEY (WORKER_CODE, IDPLOT, IDHARVEST)
);

--------------------------------------------------------
-- TABLA COLLECT (RECOLECTA)
-- SIN DEFAULT - Se manejará con trigger
--------------------------------------------------------
CREATE TABLE COLLECT (
    WORKER_CODE     VARCHAR2(6)   NOT NULL,
    IDPLOT          NUMBER(10,0)  NOT NULL,
    IDHARVEST       NUMBER(10,0)  NOT NULL,
    IDCOLLECT       NUMBER(10,0)  NOT NULL,
    COLLECTDATE     DATE          NOT NULL,
    KILOS           NUMBER(10,2)  NOT NULL,
    AMOUNT_TO_PAY   NUMBER(12,2),
    IDPAYMENT       NUMBER(10,0),
    STATUS_ID       NUMBER        NOT NULL,
    IS_COUNTABLE    NUMBER(1,0)   NOT NULL
);

--------------------------------------------------------
-- PRIMARY KEY compuesta
--------------------------------------------------------
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_PK 
  PRIMARY KEY (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT) ENABLE;

--------------------------------------------------------
-- FOREIGN KEYS
--------------------------------------------------------
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_COLLECTOR 
  FOREIGN KEY (WORKER_CODE)
  REFERENCES COLLECTOR (WORKER_CODE) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_HARVEST 
  FOREIGN KEY (IDPLOT, IDHARVEST)
  REFERENCES HARVEST (IDPLOT, IDHARVEST) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_STATUS 
  FOREIGN KEY (STATUS_ID)
  REFERENCES COLLECT_STATUS_DOMAIN (STATUS_ID) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_PAYMENT 
  FOREIGN KEY (IDPAYMENT)
  REFERENCES PAYMENT (IDPAYMENT) ENABLE;

--------------------------------------------------------
-- CHECK CONSTRAINTS BÁSICOS
--------------------------------------------------------
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_KILOS 
  CHECK (KILOS >= 0) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_STATUS 
  CHECK (STATUS_ID IN (0, 1, 2)) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_COUNTABLE 
  CHECK (IS_COUNTABLE IN (0, 1)) ENABLE;

ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_AMOUNT 
  CHECK (AMOUNT_TO_PAY IS NULL OR AMOUNT_TO_PAY >= 0) ENABLE;

--------------------------------------------------------
-- TRIGGER 1: IDPAYMENT incremental GLOBAL
--------------------------------------------------------
CREATE OR REPLACE TRIGGER PAYMENT_BI_SET_ID
BEFORE INSERT ON PAYMENT
FOR EACH ROW
BEGIN
    IF :NEW.IDPAYMENT IS NULL THEN
        :NEW.IDPAYMENT := PAYMENT_SEQ.NEXTVAL;
    END IF;
END;
/

--------------------------------------------------------
-- TRIGGER 2: IDCOLLECT incremental por asociación
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_SET_ID
BEFORE INSERT ON COLLECT
FOR EACH ROW
DECLARE
    v_next NUMBER;
BEGIN
    IF :NEW.IDCOLLECT IS NULL THEN
        BEGIN
            SELECT NEXT_ID INTO v_next
              FROM COLLECTOR_HARVEST_SEQ
             WHERE WORKER_CODE = :NEW.WORKER_CODE
               AND IDPLOT = :NEW.IDPLOT
               AND IDHARVEST = :NEW.IDHARVEST
             FOR UPDATE;
            
            :NEW.IDCOLLECT := v_next;
            
            UPDATE COLLECTOR_HARVEST_SEQ
               SET NEXT_ID = v_next + 1
             WHERE WORKER_CODE = :NEW.WORKER_CODE
               AND IDPLOT = :NEW.IDPLOT
               AND IDHARVEST = :NEW.IDHARVEST;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                :NEW.IDCOLLECT := 1;
                INSERT INTO COLLECTOR_HARVEST_SEQ 
                  (WORKER_CODE, IDPLOT, IDHARVEST, NEXT_ID)
                VALUES 
                  (:NEW.WORKER_CODE, :NEW.IDPLOT, :NEW.IDHARVEST, 2);
        END;
    END IF;
END;
/

--------------------------------------------------------
-- TRIGGER 3: Validar y establecer valores para ZERO/REGISTRADO
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_VALIDATE_AND_SET
BEFORE INSERT OR UPDATE OF STATUS_ID ON COLLECT
FOR EACH ROW
DECLARE
    v_exists NUMBER;
BEGIN
    -- PASO 1: Establecer IS_COUNTABLE según STATUS_ID
    IF :NEW.STATUS_ID = 0 THEN
        :NEW.IS_COUNTABLE := 0;
        :NEW.KILOS := 0;
        :NEW.AMOUNT_TO_PAY := 0;
    ELSE
        :NEW.IS_COUNTABLE := 1;
    END IF;
    
    -- PASO 2: Validar solo un ZERO por asociación
    IF :NEW.STATUS_ID = 0 THEN
        IF INSERTING THEN
            SELECT COUNT(*) INTO v_exists
              FROM COLLECT C
             WHERE C.WORKER_CODE = :NEW.WORKER_CODE
               AND C.IDPLOT = :NEW.IDPLOT
               AND C.IDHARVEST = :NEW.IDHARVEST
               AND C.STATUS_ID = 0;
        ELSE
            SELECT COUNT(*) INTO v_exists
              FROM COLLECT C
             WHERE C.WORKER_CODE = :NEW.WORKER_CODE
               AND C.IDPLOT = :NEW.IDPLOT
               AND C.IDHARVEST = :NEW.IDHARVEST
               AND C.STATUS_ID = 0
               AND C.IDCOLLECT != :NEW.IDCOLLECT;
        END IF;

        IF v_exists > 0 THEN
            RAISE_APPLICATION_ERROR(-20052,
                'Ya existe un registro ZERO para esta asociacion. ' ||
                'WORKER_CODE=' || :NEW.WORKER_CODE || 
                ', IDPLOT=' || :NEW.IDPLOT || 
                ', IDHARVEST=' || :NEW.IDHARVEST);
        END IF;
    END IF;
END;
/

--------------------------------------------------------
-- TRIGGER 4: Calcular AMOUNT_TO_PAY automáticamente
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_CALCULATE_AMOUNT
BEFORE INSERT OR UPDATE OF KILOS, IDPLOT, IDHARVEST ON COLLECT
FOR EACH ROW
DECLARE
    v_price_per_kilo NUMBER(10,2);
BEGIN
    IF :NEW.STATUS_ID != 0 AND :NEW.KILOS > 0 THEN
        BEGIN
            SELECT PRICEPERKILO INTO v_price_per_kilo
              FROM HARVEST
             WHERE IDPLOT = :NEW.IDPLOT
               AND IDHARVEST = :NEW.IDHARVEST;
            
            :NEW.AMOUNT_TO_PAY := :NEW.KILOS * v_price_per_kilo;
            
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                RAISE_APPLICATION_ERROR(-20053,
                    'No se encontro la cosecha especificada. ' ||
                    'IDPLOT=' || :NEW.IDPLOT || ', IDHARVEST=' || :NEW.IDHARVEST);
        END;
    ELSIF :NEW.STATUS_ID = 0 THEN
        :NEW.KILOS := 0;
        :NEW.AMOUNT_TO_PAY := 0;
    END IF;
END;
/

--------------------------------------------------------
-- TRIGGER 5: Validar asignación de pagos
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BU_VALIDATE_PAYMENT
BEFORE UPDATE OF IDPAYMENT, STATUS_ID ON COLLECT
FOR EACH ROW
BEGIN
    IF :NEW.STATUS_ID = 0 AND :NEW.IDPAYMENT IS NOT NULL THEN
        RAISE_APPLICATION_ERROR(-20054,
            'No se puede asignar factura a un registro ZERO. ' ||
            'WORKER_CODE=' || :NEW.WORKER_CODE);
    END IF;

    IF :NEW.IDPAYMENT IS NOT NULL AND :OLD.IDPAYMENT IS NULL THEN
        IF :OLD.STATUS_ID != 1 THEN
            RAISE_APPLICATION_ERROR(-20055,
                'Solo se pueden facturar recolectas con estado REGISTRADO. ' ||
                'Estado actual: ' || :OLD.STATUS_ID);
        END IF;
        :NEW.STATUS_ID := 2;
    END IF;

    IF :OLD.STATUS_ID = 2 AND :OLD.IDPAYMENT IS NOT NULL THEN
        IF :NEW.IDPAYMENT != :OLD.IDPAYMENT OR :NEW.IDPAYMENT IS NULL THEN
            RAISE_APPLICATION_ERROR(-20056,
                'No se puede modificar la factura de una recolecta ya PAGADA. ' ||
                'IDPAYMENT actual: ' || :OLD.IDPAYMENT);
        END IF;
    END IF;
END;
/

--------------------------------------------------------
-- TRIGGER 6: Actualizar TOTAL_AMOUNT de PAYMENT
--------------------------------------------------------
CREATE OR REPLACE TRIGGER PAYMENT_AI_UPDATE_TOTAL
AFTER INSERT OR UPDATE OF IDPAYMENT, AMOUNT_TO_PAY OR DELETE ON COLLECT
FOR EACH ROW
DECLARE
    v_total NUMBER(12,2);
    v_payment_id NUMBER(10,0);
BEGIN
    IF DELETING THEN
        v_payment_id := :OLD.IDPAYMENT;
    ELSIF :NEW.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :NEW.IDPAYMENT;
    ELSIF :OLD.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :OLD.IDPAYMENT;
    END IF;
    
    IF v_payment_id IS NOT NULL THEN
        SELECT NVL(SUM(AMOUNT_TO_PAY), 0) INTO v_total
          FROM COLLECT
         WHERE IDPAYMENT = v_payment_id
           AND STATUS_ID = 2;
        
        UPDATE PAYMENT
           SET TOTAL_AMOUNT = v_total
         WHERE IDPAYMENT = v_payment_id;
    END IF;
END;
/

COMMIT;


