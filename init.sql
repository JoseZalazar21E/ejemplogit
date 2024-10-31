CREATE SCHEMA IF NOT EXISTS dbEncode DEFAULT CHARACTER SET utf8 ;
USE dbEncode ;

CREATE TABLE IF NOT EXISTS dbEncode.Usuarios (
  Id INT AUTO_INCREMENT NOT NULL,
  Nombre VARCHAR(45) NULL,
  CorreoElectronico VARCHAR(60) NULL,
  Password VARCHAR(45) NULL,
  PRIMARY KEY (Id),
  UNIQUE INDEX Id_UNIQUE (Id ASC) VISIBLE)
ENGINE = InnoDB;