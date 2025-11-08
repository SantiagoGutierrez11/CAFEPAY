--------------------------------------------------------
--  DROPS
--------------------------------------------------------
DROP TRIGGER COLLECT_BI_SET_ID;
DROP TRIGGER COLLECT_BI_VALIDATE_ZERO;
DROP TRIGGER COLLECT_BI_CALCULATE_AMOUNT;
DROP TRIGGER COLLECT_BI_SET_COUNTABLE;
DROP TRIGGER COLLECT_BU_VALIDATE_PAYMENT;
DROP TRIGGER PAYMENT_AI_UPDATE_TOTAL;
DROP SEQUENCE PAYMENT_SEQ;
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
-- SECUENCIA GLOBAL PARA PAYMENT
--------------------------------------------------------
CREATE SEQUENCE PAYMENT_SEQ START WITH 1 INCREMENT BY 1 NOCACHE;

--------------------------------------------------------
-- TABLA PAYMENT (FACTURA/PAGO)
-- CLAVE PRIMARIA SIMPLE: IDPAYMENT (global)
--------------------------------------------------------
CREATE TABLE PAYMENT (
    IDPAYMENT       NUMBER(10,0)  NOT NULL,   -- ID global autoincrementable
    WORKER_CODE     VARCHAR2(6)   NOT NULL,   -- FK a COLLECTOR
    PAYMENT_DATE    DATE          NOT NULL,   -- Fecha en que se realizó el pago
    TOTAL_AMOUNT    NUMBER(12,2)  NOT NULL,   -- Monto total de la factura
    STATUS_ID       NUMBER        NOT NULL,   -- FK a PAYMENT_STATUS_DOMAIN
    NOTES           VARCHAR2(500)             -- Observaciones: método pago, bonos, etc.
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

-- Estado ZERO: establece la asociación pero no cuenta como recolecta real
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) 
VALUES (0, 'ZERO');

-- Estados que sí cuentan como recolecta
INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) 
VALUES (1, 'REGISTRADO');

INSERT INTO COLLECT_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) 
VALUES (2, 'PAGADO');

--------------------------------------------------------
-- TABLA AUXILIAR: Secuencia de COLLECT por WORKER_CODE+IDPLOT+IDHARVEST
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
-- CLAVE PRIMARIA COMPUESTA: (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT)
-- AHORA INCLUYE IS_COUNTABLE
--------------------------------------------------------
CREATE TABLE COLLECT (
    WORKER_CODE     VARCHAR2(6)   NOT NULL,   -- FK a COLLECTOR (parte de PK)
    IDPLOT          NUMBER(10,0)  NOT NULL,   -- FK a HARVEST parte 1 (parte de PK)
    IDHARVEST       NUMBER(10,0)  NOT NULL,   -- FK a HARVEST parte 2 (parte de PK)
    IDCOLLECT       NUMBER(10,0)  NOT NULL,   -- ID secuencial por collector+harvest (parte de PK)
    COLLECTDATE     DATE          NOT NULL,
    KILOS           NUMBER(10,2)  NOT NULL,
    AMOUNT_TO_PAY   NUMBER(12,2),             -- Monto calculado: KILOS * PRICEPERKILO
    IDPAYMENT       NUMBER(10,0),             -- FK a PAYMENT (nullable)
    STATUS_ID       NUMBER        NOT NULL,   -- FK a COLLECT_STATUS_DOMAIN
    IS_COUNTABLE    NUMBER(1,0)   NOT NULL    -- 1=cuenta como recolecta, 0=no cuenta (ZERO)
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

-- FK a PAYMENT (llave simple ahora)
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

-- IS_COUNTABLE debe ser 0 o 1
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_COUNTABLE 
  CHECK (IS_COUNTABLE IN (0, 1)) ENABLE;

-- Si es ZERO (status=0), debe ser NO contable (is_countable=0)
-- Si NO es ZERO, debe ser contable (is_countable=1)
ALTER TABLE COLLECT
  ADD CONSTRAINT COLLECT_CHK_ZERO_COUNTABLE 
  CHECK (
    (STATUS_ID = 0 AND IS_COUNTABLE = 0) OR 
    (STATUS_ID != 0 AND IS_COUNTABLE = 1)
  ) ENABLE;

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
-- TRIGGER 1 PAYMENT: IDPAYMENT incremental GLOBAL
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
SHOW ERRORS TRIGGER PAYMENT_BI_SET_ID;

--------------------------------------------------------
-- TRIGGER 2 COLLECT: IDCOLLECT incremental por WORKER_CODE+IDPLOT+IDHARVEST
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
-- TRIGGER 3 COLLECT: Establecer IS_COUNTABLE según STATUS_ID
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BI_SET_COUNTABLE
BEFORE INSERT OR UPDATE OF STATUS_ID ON COLLECT
FOR EACH ROW
BEGIN
    -- Establecer IS_COUNTABLE según el STATUS_ID
    IF :NEW.STATUS_ID = 0 THEN
        :NEW.IS_COUNTABLE := 0;  -- ZERO no cuenta
    ELSE
        :NEW.IS_COUNTABLE := 1;  -- REGISTRADO y PAGADO sí cuentan
    END IF;
END;
/
SHOW ERRORS TRIGGER COLLECT_BI_SET_COUNTABLE;

--------------------------------------------------------
-- TRIGGER 4 COLLECT: Validar solo un ZERO por asociación
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
-- TRIGGER 5 COLLECT: Calcular AMOUNT_TO_PAY automáticamente
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
-- TRIGGER 6 COLLECT: Validar que solo se facturen recolectas REGISTRADAS
--------------------------------------------------------
CREATE OR REPLACE TRIGGER COLLECT_BU_VALIDATE_PAYMENT
BEFORE UPDATE OF IDPAYMENT, STATUS_ID ON COLLECT
FOR EACH ROW
BEGIN
    -- Validación 1: No se puede asignar factura a recolectas ZERO
    IF :NEW.STATUS_ID = 0 AND :NEW.IDPAYMENT IS NOT NULL THEN
        RAISE_APPLICATION_ERROR(-20054,
            'No se puede asignar factura a un registro ZERO. ' ||
            'WORKER_CODE=' || :NEW.WORKER_CODE);
    END IF;

    -- Validación 2: Solo se pueden facturar recolectas REGISTRADAS (no pagadas)
    IF :NEW.IDPAYMENT IS NOT NULL AND :OLD.IDPAYMENT IS NULL THEN
        IF :OLD.STATUS_ID != 1 THEN
            RAISE_APPLICATION_ERROR(-20055,
                'Solo se pueden facturar recolectas con estado REGISTRADO. ' ||
                'Estado actual: ' || :OLD.STATUS_ID);
        END IF;
        -- Auto-marcar como PAGADO al asignar factura
        :NEW.STATUS_ID := 2;
    END IF;

    -- Validación 3: No se puede cambiar la factura de una recolecta ya PAGADA
    IF :OLD.STATUS_ID = 2 AND :OLD.IDPAYMENT IS NOT NULL THEN
        IF :NEW.IDPAYMENT != :OLD.IDPAYMENT OR :NEW.IDPAYMENT IS NULL THEN
            RAISE_APPLICATION_ERROR(-20056,
                'No se puede modificar la factura de una recolecta ya PAGADA. ' ||
                'IDPAYMENT actual: ' || :OLD.IDPAYMENT);
        END IF;
    END IF;
END;
/
SHOW ERRORS TRIGGER COLLECT_BU_VALIDATE_PAYMENT;

--------------------------------------------------------
-- TRIGGER 7 PAYMENT: Actualizar TOTAL_AMOUNT automáticamente
--------------------------------------------------------
CREATE OR REPLACE TRIGGER PAYMENT_AI_UPDATE_TOTAL
AFTER INSERT OR UPDATE OF IDPAYMENT, AMOUNT_TO_PAY OR DELETE ON COLLECT
FOR EACH ROW
DECLARE
    v_total NUMBER(12,2);
    v_payment_id NUMBER(10,0);
BEGIN
    -- Determinar qué PAYMENT actualizar
    IF DELETING THEN
        v_payment_id := :OLD.IDPAYMENT;
    ELSIF :NEW.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :NEW.IDPAYMENT;
    ELSIF :OLD.IDPAYMENT IS NOT NULL THEN
        v_payment_id := :OLD.IDPAYMENT;
    END IF;
    
    -- Si hay un IDPAYMENT, recalcular el total
    IF v_payment_id IS NOT NULL THEN
        -- Suma todas las recolectas PAGADAS de esta factura
        SELECT NVL(SUM(AMOUNT_TO_PAY), 0) INTO v_total
          FROM COLLECT
         WHERE IDPAYMENT = v_payment_id
           AND STATUS_ID = 2;  -- Solo recolectas PAGADAS
        
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

-- Crear PRIMERA factura para W00001 (IDPAYMENT será 1 automáticamente)
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID, NOTES)
VALUES (NULL, 'W00001', DATE '2024-06-10', 0, 1, 'Pago semanal #1');

-- Asociar SOLO las recolectas REGISTRADAS (STATUS_ID = 1) a la factura
-- El trigger automáticamente las marcará como PAGADAS (STATUS_ID = 2)
UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL
 WHERE WORKER_CODE = 'W00001'
   AND IDPLOT = 1
   AND IDHARVEST = 3
   AND STATUS_ID = 1;  -- Solo las REGISTRADAS

-- Más recolectas de W00001 para segunda factura
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-11', 55.00, 1);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00001', 1, 3, NULL, DATE '2024-06-12', 60.25, 1);

-- Crear SEGUNDA factura para W00001 (IDPAYMENT será 2 automáticamente)
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID, NOTES)
VALUES (NULL, 'W00001', DATE '2024-06-17', 0, 1, 'Pago semanal #2');

UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL
 WHERE WORKER_CODE = 'W00001'
   AND IDPLOT = 1
   AND IDHARVEST = 3
   AND IDCOLLECT IN (5, 6)
   AND STATUS_ID = 1;

-- Recolector W00002 en Cosecha (2,3) - "La Montañita" activa
-- Precio por kilo en cosecha (2,3) = 2000.22
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-05', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-06', 38.20, 1);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00002', 2, 3, NULL, DATE '2025-01-07', 41.80, 1);

-- Crear factura para W00002 (IDPAYMENT será 3 en secuencia global)
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID, NOTES)
VALUES (NULL, 'W00002', DATE '2025-01-10', 0, 1, 'Pago semanal #1');

UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL
 WHERE WORKER_CODE = 'W00002'
   AND IDPLOT = 2
   AND IDHARVEST = 3
   AND STATUS_ID = 1;

-- Recolector W00003 en Cosecha (1,3)
INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-01', 0, 0);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-02', 55.60, 1);

INSERT INTO COLLECT (WORKER_CODE, IDPLOT, IDHARVEST, IDCOLLECT, COLLECTDATE, KILOS, STATUS_ID)
VALUES ('W00003', 1, 3, NULL, DATE '2024-06-03', 49.90, 1);

-- Crear factura para W00003 (IDPAYMENT será 4 en secuencia global)
INSERT INTO PAYMENT (IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID)
VALUES (NULL, 'W00003', DATE '2024-06-08', 0, 2);

UPDATE COLLECT 
   SET IDPAYMENT = PAYMENT_SEQ.CURRVAL
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

--------------------------------------------------------
-- CONSULTAS PARA VERIFICAR
--------------------------------------------------------

-- Ver todas las facturas (IDs globales: 1, 2, 3, 4...)
SELECT IDPAYMENT, WORKER_CODE, PAYMENT_DATE, TOTAL_AMOUNT, STATUS_ID, NOTES
FROM PAYMENT