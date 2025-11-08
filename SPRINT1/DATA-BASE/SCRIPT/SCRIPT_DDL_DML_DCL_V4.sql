ALTER SESSION SET "_ORACLE_SCRIPT"=TRUE;
DROP USER adminCAFEPAY;
CREATE USER adminCAFEPAY IDENTIFIED BY adminCAFEPAY;
GRANT DBA, CONNECT TO adminCAFEPAY;


--------------------------------------------------------
--  Eliminación previa (si existe)
--------------------------------------------------------
DROP TABLE COLLECTOR CASCADE CONSTRAINTS;
DROP TABLE COLLECTOR_STATUS_DOMAIN CASCADE CONSTRAINTS;

--------------------------------------------------------
--  Tabla de dominio estático para el estado del recolector
--------------------------------------------------------
CREATE TABLE COLLECTOR_STATUS_DOMAIN (
    STATUS_ID NUMBER NOT NULL,
    STATUS_NAME VARCHAR2(10) NOT NULL
);

--------------------------------------------------------
--  Datos estáticos del dominio de estado
--------------------------------------------------------
INSERT INTO COLLECTOR_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (1, 'ACTIVE');
INSERT INTO COLLECTOR_STATUS_DOMAIN (STATUS_ID, STATUS_NAME) VALUES (2, 'INACTIVE');

--------------------------------------------------------
--  Creación de tabla COLLECTOR
--------------------------------------------------------
CREATE TABLE COLLECTOR (
    WORKER_CODE       VARCHAR2(6) NOT NULL,  -- código único de trabajador, clave primaria
    ID                NUMBER(10,0) NOT NULL,        -- FK a PERSON (si existe)
    FIRST_NAME        VARCHAR2(30) NOT NULL,
    LAST_NAME         VARCHAR2(30) NOT NULL,
    PHONE             NUMBER(10,0) NOT NULL,
    STATUS_ID         NUMBER NOT NULL         -- FK a COLLECTOR_STATUS_DOMAIN
);

--------------------------------------------------------
--  Definición de restricciones (Constraints)
--------------------------------------------------------

--------------------------------------------------------
--  Collector
--------------------------------------------------------
-- Clave primaria
ALTER TABLE COLLECTOR 
    ADD CONSTRAINT COLLECTOR_PK PRIMARY KEY (WORKER_CODE) ENABLE;
ALTER TABLE COLLECTOR_STATUS_DOMAIN 
    ADD CONSTRAINT COLLECTOR_STATUS_PK PRIMARY KEY (STATUS_ID) ENABLE;

-- Clave única (para el id personal)
ALTER TABLE COLLECTOR 
    ADD CONSTRAINT COLLECTOR_UQ_ID UNIQUE (ID) ENABLE;

-- Llave foránea a la tabla de dominio COLLECTOR_STATUS_DOMAIN
ALTER TABLE COLLECTOR 
    ADD CONSTRAINT COLLECTOR_FK_STATUS 
    FOREIGN KEY (STATUS_ID) 
    REFERENCES COLLECTOR_STATUS_DOMAIN (STATUS_ID) ENABLE;
-- Check de status
ALTER TABLE COLLECTOR 
  ADD CONSTRAINT COLLECTOR_CHK_STATUS CHECK (STATUS_ID IN (1,2)) ENABLE;
--------------------------------------------------------


-- 10 INSERTS DE EJEMPLO
INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00001', 25483761, 'Juan',   'Pérez',     3012345678, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00002', 48951236, 'María',  'Gómez',     3029876543, 2);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00003', 712459863, 'Andrés', 'López',     3105551234, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00004', 96325874, 'Luisa',  'Martínez',  3217778888, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00005', 185296347, 'Carlos', 'Rojas',     3024449999, 2);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00006', 54218639, 'Sofía',  'Ramírez',   3116663333, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00007', 8741259630, 'David',  'Torres',    3202225555, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00008', 63987412, 'Paula',  'Castro',    3128881111, 2);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00009', 296385174, 'Felipe', 'Mejía',     3019994444, 1);

INSERT INTO COLLECTOR (WORKER_CODE, ID, FIRST_NAME, LAST_NAME, PHONE, STATUS_ID)
VALUES ('W00010', 457812396, 'Natalia','Hernández', 3223336666, 1);

COMMIT;