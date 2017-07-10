DROP TABLE Erfolgsrechnung;
DROP TABLE Konten;
DROP TABLE SonstigeErfolgsrechnung;
DROP TABLE Kostenstelle;
DROP TABLE Sonstige_Daten;

CREATE TABLE Erfolgsrechnung
(
	KontoNr  INTEGER  NOT NULL,
	Wert  INTEGER  ,
	Art  VARCHAR(20)  NOT NULL,
	Jahr  INTEGER  NOT NULL
)
;



ALTER TABLE Erfolgsrechnung
	ADD CONSTRAINT  XPKhatKontenKosten  PRIMARY KEY (KontoNr,Art,Jahr)
;



CREATE TABLE Konten
(
	KontoNr  INTEGER  NOT NULL,
	Name  VARCHAR(50)  
)
;



ALTER TABLE Konten
	ADD CONSTRAINT  XPKKonten  PRIMARY KEY (KontoNr)
;



ALTER TABLE Erfolgsrechnung
	ADD CONSTRAINT R_7 FOREIGN KEY (KontoNr) REFERENCES Konten(KontoNr)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
;