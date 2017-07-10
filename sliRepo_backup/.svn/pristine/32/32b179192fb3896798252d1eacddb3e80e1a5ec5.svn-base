
DROP TABLE Erfolgsrechnung;
DROP TABLE Konten;
DROP TABLE SonstigeErfolgsrechnung;
DROP TABLE Kostenstelle;
DROP TABLE Sonstige_Daten;

CREATE TABLE Erfolgsrechnung
(
	KontoNr  INTEGER  NOT NULL,
	KostenstellenID  INTEGER  NOT NULL,
	Wert  INTEGER  ,
	Art  VARCHAR(20)  NOT NULL,
	Jahr  INTEGER  NOT NULL
);

ALTER TABLE Erfolgsrechnung
	ADD CONSTRAINT  XPKhatKontenKosten  PRIMARY KEY (KontoNr,KostenstellenID,Art,Jahr)
;

CREATE TABLE Konten
(
	KontoNr  INTEGER  NOT NULL,
	Name  VARCHAR(50)  
);

ALTER TABLE Konten
	ADD CONSTRAINT  PKKonten  PRIMARY KEY (KontoNr)
;

CREATE TABLE Kostenstelle
(
	KostenstellenID  INTEGER  NOT NULL,
	Name  VARCHAR(30)  
);

ALTER TABLE Kostenstelle
	ADD CONSTRAINT  PKKostenstelle  PRIMARY KEY (KostenstellenID)
;

CREATE TABLE Sonstige_Daten
(
	Nummer  INTEGER  NOT NULL,
	Name  VARCHAR(40)  
);

ALTER TABLE Sonstige_Daten
	ADD CONSTRAINT  PKSonstige_Daten  PRIMARY KEY (Nummer)
;

CREATE TABLE SonstigeErfolgsrechnung
(
	Wert  INTEGER  ,
	Nummer  INTEGER  NOT NULL,
	KostenstellenID  INTEGER  NOT NULL,
	Jahr  INTEGER  NOT NULL,
	Art  VARCHAR(20)  NOT NULL
)
;

ALTER TABLE SonstigeErfolgsrechnung
	ADD CONSTRAINT  PKhatKostenSonsti  PRIMARY KEY (Nummer,KostenstellenID,Jahr,Art)
;

ALTER TABLE Erfolgsrechnung
	ADD CONSTRAINT FKErfolgsrechnung_Konto FOREIGN KEY (KontoNr) REFERENCES Konten(KontoNr)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
;

ALTER TABLE Erfolgsrechnung
	ADD CONSTRAINT FKErfolgsrechnung_Kostenstelle FOREIGN KEY (KostenstellenID) REFERENCES Kostenstelle(KostenstellenID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
;



ALTER TABLE SonstigeErfolgsrechnung
	ADD CONSTRAINT R_12 FOREIGN KEY (Nummer) REFERENCES Sonstige_Daten(Nummer)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
;


ALTER TABLE SonstigeErfolgsrechnung
	ADD CONSTRAINT R_16 FOREIGN KEY (KostenstellenID) REFERENCES Kostenstelle(KostenstellenID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
;



CREATE TRIGGER tI_Erfolgsrechnung AFTER INSERT ON Erfolgsrechnung
   REFERENCING NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Konten WHERE new.KontoNr = Konten.KontoNr) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot insert Erfolgsrechnung because Konten does not exist.')
 !!

CREATE TRIGGER tI_Erfolgsrechnun2 AFTER INSERT ON Erfolgsrechnung
   REFERENCING NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE new.KostenstellenID = Kostenstelle.KostenstellenID) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot insert Erfolgsrechnung because Kostenstelle does not exist.')
 !!

CREATE TRIGGER tU_Erfolgsrechnung NO CASCADE BEFORE UPDATE ON Erfolgsrechnung
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Konten WHERE new.KontoNr = Konten.KontoNr) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot update Erfolgsrechnung because Konten does not exist.')
 !!

CREATE TRIGGER tU_Erfolgsrechnun2 NO CASCADE BEFORE UPDATE ON Erfolgsrechnung
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE new.KostenstellenID = Kostenstelle.KostenstellenID) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot update Erfolgsrechnung because Kostenstelle does not exist.')
 !!


CREATE TRIGGER tD_Konten AFTER DELETE ON Konten
   REFERENCING OLD AS OLD FOR EACH ROW MODE DB2SQL
   WHEN ((SELECT count(*) FROM Erfolgsrechnung WHERE Erfolgsrechnung.KontoNr = old.KontoNr) > 0)
     SIGNAL SQLSTATE '75001' ('Cannot delete Konten because Erfolgsrechnung exists.')
 !!

CREATE TRIGGER tU_Konten NO CASCADE BEFORE UPDATE ON Konten
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Konten WHERE Konten.KontoNr <> Konten.KontoNr) > 0) AND
     ((SELECT count(*) FROM Erfolgsrechnung WHERE Erfolgsrechnung.KontoNr = old.KontoNr) > 0))
       SIGNAL SQLSTATE '75001' ('Cannot update Konten because Erfolgsrechnung exists.')
 !!


CREATE TRIGGER tD_Kostenstelle AFTER DELETE ON Kostenstelle
   REFERENCING OLD AS OLD FOR EACH ROW MODE DB2SQL
   WHEN ((SELECT count(*) FROM Erfolgsrechnung WHERE Erfolgsrechnung.KostenstellenID = old.KostenstellenID) > 0)
     SIGNAL SQLSTATE '75001' ('Cannot delete Kostenstelle because Erfolgsrechnung exists.')
 !!

CREATE TRIGGER tD_Kostenstelle2 AFTER DELETE ON Kostenstelle
   REFERENCING OLD AS OLD FOR EACH ROW MODE DB2SQL
   WHEN ((SELECT count(*) FROM SonstigeErfolgsrechnung WHERE SonstigeErfolgsrechnung.KostenstellenID = old.KostenstellenID) > 0)
     SIGNAL SQLSTATE '75001' ('Cannot delete Kostenstelle because SonstigeErfolgsrechnung exists.')
 !!

CREATE TRIGGER tU_Kostenstelle NO CASCADE BEFORE UPDATE ON Kostenstelle
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE Kostenstelle.KostenstellenID <> Kostenstelle.KostenstellenID) > 0) AND
     ((SELECT count(*) FROM Erfolgsrechnung WHERE Erfolgsrechnung.KostenstellenID = old.KostenstellenID) > 0))
       SIGNAL SQLSTATE '75001' ('Cannot update Kostenstelle because Erfolgsrechnung exists.')
 !!

CREATE TRIGGER tU_Kostenstelle2 NO CASCADE BEFORE UPDATE ON Kostenstelle
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE Kostenstelle.KostenstellenID <> Kostenstelle.KostenstellenID) > 0) AND
     ((SELECT count(*) FROM SonstigeErfolgsrechnung WHERE SonstigeErfolgsrechnung.KostenstellenID = old.KostenstellenID) > 0))
       SIGNAL SQLSTATE '75001' ('Cannot update Kostenstelle because SonstigeErfolgsrechnung exists.')
 !!


CREATE TRIGGER tD_Sonstige_Daten AFTER DELETE ON Sonstige_Daten
   REFERENCING OLD AS OLD FOR EACH ROW MODE DB2SQL
   WHEN ((SELECT count(*) FROM SonstigeErfolgsrechnung WHERE SonstigeErfolgsrechnung.Nummer = old.Nummer) > 0)
     SIGNAL SQLSTATE '75001' ('Cannot delete Sonstige_Daten because SonstigeErfolgsrechnung exists.')
 !!

CREATE TRIGGER tU_Sonstige_Daten NO CASCADE BEFORE UPDATE ON Sonstige_Daten
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Sonstige_Daten WHERE Sonstige_Daten.Nummer <> Sonstige_Daten.Nummer) > 0) AND
     ((SELECT count(*) FROM SonstigeErfolgsrechnung WHERE SonstigeErfolgsrechnung.Nummer = old.Nummer) > 0))
       SIGNAL SQLSTATE '75001' ('Cannot update Sonstige_Daten because SonstigeErfolgsrechnung exists.')
 !!


CREATE TRIGGER tI_SonstigeErfolgs AFTER INSERT ON SonstigeErfolgsrechnung
   REFERENCING NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Sonstige_Daten WHERE new.Nummer = Sonstige_Daten.Nummer) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot insert SonstigeErfolgsrechnung because Sonstige_Daten does not exist.')
 !!

CREATE TRIGGER tI_SonstigeErfolg2 AFTER INSERT ON SonstigeErfolgsrechnung
   REFERENCING NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE new.KostenstellenID = Kostenstelle.KostenstellenID) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot insert SonstigeErfolgsrechnung because Kostenstelle does not exist.')
 !!

CREATE TRIGGER tU_SonstigeErfolgs NO CASCADE BEFORE UPDATE ON SonstigeErfolgsrechnung
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Sonstige_Daten WHERE new.Nummer = Sonstige_Daten.Nummer) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot update SonstigeErfolgsrechnung because Sonstige_Daten does not exist.')
 !!

CREATE TRIGGER tU_SonstigeErfolg2 NO CASCADE BEFORE UPDATE ON SonstigeErfolgsrechnung
    REFERENCING OLD AS OLD NEW AS NEW FOR EACH ROW MODE DB2SQL
   WHEN (((SELECT count(*) FROM Kostenstelle WHERE new.KostenstellenID = Kostenstelle.KostenstellenID) = 0)
     )
       SIGNAL SQLSTATE '75001' ('Cannot update SonstigeErfolgsrechnung because Kostenstelle does not exist.')
 !!

