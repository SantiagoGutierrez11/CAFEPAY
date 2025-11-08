--------------------------------------------------------
--  DROPS
--------------------------------------------------------
DROP TRIGGER COLLECT_BI_SET_ID;
DROP TRIGGER COLLECT_BI_VALIDATE_ZERO;
DROP TRIGGER COLLECT_BI_CALCULATE_AMOUNT;
DROP TRIGGER PAYMENT_AI_UPDATE_TOTAL;
DROP TABLE COLLECT CASCADE CONSTRAINTS;
DROP TABLE PAYMENT CASCADE CONSTRAINTS;
DROP TABLE COLLECTOR_HARVEST_SEQ CASCADE CONSTRAINTS;
DROP TABLE COLLECT_STATUS_DOMAIN CASCADE CONSTRAINTS;
DROP TABLE PAYMENT_STATUS_DOMAIN CASCADE CONSTRAINTS;

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
-- TABLA PAYMENT (FACTURA/PAGO)
--------------------------------------------------------
CREATE TABLE PAYMENT (
    IDPAYMENT       NUMBER(10,0)  NOT NULL,
    WORKER_CODE     VARCHAR2(6)   NOT NULL,   -- FK a COLLECTOR
    PAYMENT_DATE    DATE          NOT NULL,    -- Fecha en que se realizó el pago
    TOTAL_AMOUNT    NUMBER(12,2)  NOT NULL,    -- Monto total de la factura
    STATUS_ID       NUMBER        NOT NULL,    -- FK a PAYMENT_STATUS_DOMAIN
    NOTES           VARCHAR2(200),             -- Observaciones opcionales
    CREATED_DATE    DATE          DEFAULT SYSDATE NOT NULL
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

-- Secuencia para IDPAYMENT
CREATE SEQUENCE PAYMENT_SEQ START WITH 1 INCREMENT BY 1;

--------------------------------------------------------
-- DOMINIO DE ESTADO DE LA RECOLECTA
--------------------------------------------------------
CREATE TABLE COLLECT_STATUS_DOMAIN (
    STATUS_ID   NUMBER       NOT NULL,
    STATUS_NAME VARCHAR2(15) NOT NULL,
    IS_COUNTABLE NUMBER(1,0) NOT NULL  -- 1=cuenta como recolecta, 0=no cuenta (ZERO)
);

ALTER TABLE COLLECT_STATUS_DOMAIN
  ADD CONSTRAINT COLLECT_STATUS_PK PRIMARY KEY (STATUS_ID) ENABLE;

-- Estado ZERO: establece la asociación pero no cuenta como recolecta real
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME, IS_COUNTABLE) 
VALUES (0, 'ZERO', 0);

-- Estados que sí cuentan como recolecta
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME, IS_COUNTABLE) 
VALUES (1, 'REGISTRADO', 1);

INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME, IS_COUNTABLE) 
VALUES (2, 'PAGADO', 1);

--------------------------------------------------------
-- TABLA COLLECT (RECOLECTA)
--------------------------------------------------------
CREATE TABLE COLLECT (
    WORKER_CODE     VARCHAR2(6)   NOT NULL,   -- FK a COLLECTOR
    IDPLOT          NUMBER(10,0)  NOT NULL,   -- FK a HARVEST (parte 1)
    IDHARVEST       NUMBER(10,0)  NOT NULL,   -- FK a HARVEST (parte 2)
    IDCOLLECT       NUMBER(10,0)  NOT NULL,   -- ID secuencial por collector+harvest
    COLLECTDATE     DATE          NOT NULL,
    KILOS           NUMBER(10,2)  NOT NULL,
    AMOUNT_TO_PAY   NUMBER(12,2),             -- Monto calculado: KILOS * PRICEPERKILO
    IDPAYMENT       NUMBER(10,0),             -- FK a PAYMENT (nullable)
    STATUS_ID       NUMBER        NOT NULL    -- FK a COLLECT_STATUS_DOMAIN
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
-- FK a COLLECTOR
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_COLLECTOR 
  FOREIGN KEY (WORKER_CODE)
  REFERENCES COLLECTOR (WORKER_CODE) ENABLE;

-- FK a HARVEST (llave compuesta)
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_HARVEST 
  FOREIGN KEY (IDPLOT, IDHARVEST)
  REFERENCES HARVEST (IDPLOT, IDHARVEST) ENABLE;

-- FK a COLLECT_STATUS_DOMAIN
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_STATUS 
  FOREIGN KEY (STATUS_ID)
  REFERENCES COLLECT_STATUS_DOMAIN (STATUS_ID) ENABLE;

-- FK a PAYMENT
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_FK_PAYMENT 
  FOREIGN KEY (IDPAYMENT)
  REFERENCES PAYMENT (IDPAYMENT) ENABLE;

--------------------------------------------------------
-- CHECK CONSTRAINTS
--------------------------------------------------------
-- Los kilos deben ser >= 0
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_KILOS 
  CHECK (KILOS >= 0) ENABLE;

-- STATUS_ID debe ser válido (0, 1, 2)
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_STATUS 
  CHECK (STATUS_ID IN (0, 1, 2)) ENABLE;

-- Si es ZERO, los kilos deben ser 0 y AMOUNT_TO_PAY debe ser 0 o NULL
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_ZERO_KILOS 
  CHECK (
    (STATUS_ID = 0 AND KILOS = 0 AND (AMOUNT_TO_PAY IS NULL OR AMOUNT_TO_PAY = 0)) OR 
    (STATUS_ID != 0 AND KILOS >= 0)
  ) ENABLE;

-- Si está PAGADO (status=2), debe tener IDPAYMENT
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_PAID_HAS_PAYMENT 
  CHECK (
    (STATUS_ID = 2 AND IDPAYMENT IS NOT NULL) OR 
    (STATUS_ID != 2)
  ) ENABLE;

-- AMOUNT_TO_PAY debe ser >= 0
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_AMOUNT 
  CHECK (AMOUNT_TO_PAY IS NULL OR AMOUNT_TO_PAY >= 0) ENABLE;

--------------------------------------------------------
-- REGLA DE NEGOCIO: Solo un ZERO por asociación collector-harvest
-- Índice único condicional
--------------------------------------------------------
CREATE UNIQUE INDEX UNQ_COLLECT_ONE_ZERO
ON COLLECT (
  CASE WHEN STATUS_ID = 0 THEN WORKER_CODE || '-' || IDPLOT || '-' || IDHARVEST END
);

--------------------------------------------------------
-- TABLA AUXILIAR: correlativo por collector+harvest
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
-- TRIGGER 1: IDCOLLECT incremental por WORKER_CODE+IDPLOT+IDHARVEST
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
SHOW ERRORS TRIGGER COLLECT_BI_SET_ID;

--------------------------------------------------------
-- TRIGGER 2: Validar solo un ZERO por asociación
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_VALIDATE_ZERO
BEFORE INSERT OR UPDATE OF STATUS_ID ON COLLECT
FOR EACH ROW
DECLARE
    v_exists NUMBER;
BEGIN
    -- Validar: Solo un registro ZERO por asociación collector-harvest
    IF :NEW.STATUS_ID = 0 THEN
        IF INSERTING THEN
            SELECT COUNT(*) INTO v_exists
              FROM COLLECT C
             WHERE C.WORKER_CODE = :NEW.WORKER_CODE
               AND C.IDPLOT = :NEW.IDPLOT
               AND C.IDHARVEST = :NEW.IDHARVEST
               AND C.STATUS_ID = 0;
        ELSE -- UPDATING
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
                'Ya existe un registro ZERO para esta asociación. ' ||
                'WORKER_CODE=' || :NEW.WORKER_CODE || 
                ', IDPLOT=' || :NEW.IDPLOT || 
                ', IDHARVEST=' || :NEW.IDHARVEST);
        END IF;
    END IF;
END;
/
SHOW ERRORS TRIGGER COLLECT_BI_VALIDATE_ZERO;

--------------------------------------------------------
-- TRIGGER 3: Calcular AMOUNT_TO_PAY automáticamente
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_CALCULATE_AMOUNT
BEFORE INSERT OR UPDATE OF KILOS, IDPLOT, IDHARVEST ON COLLECT
FOR EACH ROW
DECLARE
    v_price_per_kilo NUMBER(10,2);
BEGIN
    -- Solo calcular si NO es ZERO (STATUS_ID != 0) y hay kilos
    IF :NEW.STATUS_ID != 0 AND :NEW.KILOS > 0 THEN
        BEGIN
            -- Obtener el precio por kilo de la cosecha
            SELECT PRICEPERKILO INTO v_price_per_kilo
              FROM HARVEST
             WHERE IDPLOT = :NEW.IDPLOT
               AND IDHARVEST = :NEW.IDHARVEST;
            
            -- Calcular el monto: KILOS * PRICEPERKILO
            :NEW.AMOUNT_TO_PAY := :NEW.KILOS * v_price_per_kilo;
            
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                RAISE_APPLICATION_ERROR(-20053,
                    'No se encontró la cosecha especificada. ' ||
                    'IDPLOT=' || :NEW.IDPLOT || ', IDHARVEST=' || :NEW.IDHARVEST);
        END;
    ELSE
        -- Si es ZERO, el monto debe ser 0
        :NEW.AMOUNT_TO_PAY := 0;
    END IF;
END;
/
SHOW ERRORS TRIGGER COLLECT_BI_CALCULATE_AMOUNT;

--------------------------------------------------------
-- TRIGGER 4: Actualizar TOTAL_AMOUNT en PAYMENT
--------------------------------------------------------
CREATE OR REPLACE TRIGGER PAYMENT_AI_UPDATE_TOTAL
AFTER INSERT OR UPDATE OF IDPAYMENT, AMOUNT_TO_PAY OR DELETE ON COLLECT
FOR EACH ROW
DECLARE
    v_total NUMBER(12,2);
    v_payment_id NUMBER(10,0);
BEGIN
    -- Determinar qué IDPAYMENT actualizar
    IF DELETING THEN
        v_payment_id := :OLD.IDPAYMENT;
    ELSIF :NEW.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :NEW.IDPAYMENT;
    ELSIF :OLD.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :OLD.IDPAYMENT;
    END IF;

    -- Si hay un IDPAYMENT, recalcular el total
    IF v_payment_id IS NOT NULL THEN
        SELECT NVL(SUM(AMOUNT_TO_PAY), 0) INTO v_total
          FROM COLLECT
         WHERE IDPAYMENT = v_payment_id
           AND STATUS_ID != 0;  -- Excluir registros ZERO
        
        UPDATE PAYMENT
           SET TOTAL_AMOUNT = v_total
         WHERE IDPAYMENT = v_payment_id;
    END IF;
END;
/
SHOW ERRORS TRIGGER PAYMENT_AI_UPDATE_TOTAL;

--------------------------------------------------------
-- DATOS DE EJEMPLO
--------------------------------------------------------

-- Recolector W00001 en Cosecha (1,3) - "El Cafetal Norte" activa
-- Precio por kilo en cosecha (1,3) = 3.75
-- Registro ZERO para establecer la asociación
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-01', 0, 0);

-- Recolectas reales del mismo collector (pendientes de pago)
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-02', 45.50, 1);
-- AMOUNT_TO_PAY = 45.50 * 3.75 = 170.625

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-03', 52.30, 1);
-- AMOUNT_TO_PAY = 52.30 * 3.75 = 196.125

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-04', 48.75, 1);
-- AMOUNT_TO_PAY = 48.75 * 3.75 = 182.8125

-- Crear una factura para W00001
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID, NOTES)
VALUES (PAYMENT_SEQ.NEXTVAL, 'W00001', DATE '2024-06-10', 0, 1, 'Pago semanal');

-- Asociar las recolectas a la factura y marcarlas como PAGADAS
UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL,
       STATUS_ID = 2
 WHERE WORKER_CODE = 'W00001'
   AND IDPLOT = 1
   AND IDHARVEST = 3
   AND STATUS_ID = 1;

-- El trigger PAYMENT_AI_UPDATE_TOTAL calculará automáticamente el TOTAL_AMOUNT

-- Recolector W00002 en Cosecha (2,3) - "La Montañita" activa
-- Precio por kilo en cosecha (2,3) = 2000.22
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-05', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-06', 38.20, 1);
-- AMOUNT_TO_PAY = 38.20 * 2000.22 = 76408.404

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-07', 41.80, 1);
-- AMOUNT_TO_PAY = 41.80 * 2000.22 = 83609.196

-- Recolector W00003 en Cosecha (1,3)
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-01', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-02', 55.60, 1);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-03', 49.90, 1);

-- Crear factura y pagar
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID)
VALUES (PAYMENT_SEQ.NEXTVAL, 'W00003', DATE '2024-06-08', 0, 2);

UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL,
       STATUS_ID = 2
 WHERE WORKER_CODE = 'W00003'
   AND IDPLOT = 1
   AND IDHARVEST = 3
   AND STATUS_ID = 1;

-- Recolector W00004 en múltiples cosechas
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00004', 1, 3, NULL, DATE '2024-06-01', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00004', 1, 3, NULL, DATE '2024-06-05', 60.25, 1);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00004', 2, 3, NULL, DATE '2025-01-05', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00004', 2, 3, NULL, DATE '2025-01-08', 44.70, 1);

COMMIT;