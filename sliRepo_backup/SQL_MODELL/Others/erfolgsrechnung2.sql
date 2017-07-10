
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

