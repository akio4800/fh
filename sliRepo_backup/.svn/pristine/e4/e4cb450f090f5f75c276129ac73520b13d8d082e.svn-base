
DROP TABLE Monatsabrechnung CASCADE;
DROP TABLE Auftraggeberdaten CASCADE;
DROP TABLE DienstVerhaeltnis CASCADE;
Drop Table Dokumente CASCADE;
DROP TABLE Auftraggeber CASCADE;
DROP TABLE PersoenlicherAssistent CASCADE;
DROP TABLE Person CASCADE;
DROP TABLE Adresse CASCADE;
DROP TABLE Leistungseintrag CASCADE;
DROP TABLE Taetigkeit CASCADE;



CREATE TABLE Adresse
(
	AdressID	  INTEGER  NOT NULL ,
	Strasse		  VARCHAR(30)  NULL ,
	Stadt		  VARCHAR(20)  NULL ,
	Land		  VARCHAR(20)  NULL ,
	HausNummer	  INTEGER  NULL ,
	Stiege		  INTEGER  NULL ,
	Stock		  INTEGER  NULL ,
	PLZ		  INTEGER  NULL ,
	Tuer		  INTEGER  NULL 
);



CREATE UNIQUE INDEX XPKAdresse ON Adresse
(AdressID  ASC);



ALTER TABLE Adresse
	ADD CONSTRAINT PKAdresse PRIMARY KEY (AdressID);



CREATE TABLE Auftraggeber
(
	Aktiv		  BOOLEAN  NULL ,
	BewilligungsAnfang DATE NULL ,
	BewilligungsEnde  DATE  NULL ,
	EintrittsDatum	  DATE  NULL ,
	AgID	  INTEGER  NOT NULL,
	Kontaktperson INTEGER NULL
);



CREATE UNIQUE INDEX XPKAuftraggeber ON Auftraggeber
(AgID  ASC);



ALTER TABLE Auftraggeber
	ADD CONSTRAINT  PKAuftraggeber PRIMARY KEY (AgID);



CREATE TABLE Auftraggeberdaten
(
	FahrtkostenzusatzKM  NUMERIC(7,2)  NOT NULL ,
	StundensatzAuszahlung  NUMERIC(7,2)  NOT NULL ,
	StundenSatz	  NUMERIC(7,2)  NOT NULL ,
	BeitragEinkommen  NUMERIC(7,2)  NOT NULL ,
	Monat		  Numeric(2)  NOT NULL ,
	Jahr		  Numeric(4)  NOT NULL ,
	AgID	  INTEGER  NOT NULL ,
	FahrtkostenZusatz  NUMERIC(7,2)  NOT NULL ,
	BetreuungsbedarfH  INTEGER  NOT NULL ,
	PflegegeldStufe	  INTEGER  NOT NULL ,
	EinkommenMonat	  NUMERIC(7,2)  NOT NULL 
);



ALTER TABLE Auftraggeberdaten
	ADD CONSTRAINT  PKDatenAg_Monat PRIMARY KEY (Monat,Jahr,AgID);



CREATE TABLE DienstVerhaeltnis
(
	AgID	  INTEGER  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	Dienstvertrag	  VARCHAR(50)  NULL 
);

ALTER TABLE DienstVerhaeltnis
	ADD CONSTRAINT  PKDienstVerhaeltnis PRIMARY KEY (PaID, AgID);



CREATE TABLE Dokumente
(
	Name		  VARCHAR(20)  NOT NULL ,
	Datum		  DATE  NOT NULL ,
	PaID	  	  INTEGER  NOT NULL ,
	File		  VARCHAR(50)  NULL ,
	erforderlich	  BOOLEAN  NULL 
);


ALTER TABLE Dokumente
	ADD CONSTRAINT  PKDokumente PRIMARY KEY (Name,PaID,Datum);



CREATE TABLE Monatsabrechnung
(
	AnzahPrivatlKM	  INTEGER  NOT NULL ,
	Monat		  Numeric(2)  NOT NULL ,
	Jahr		  Numeric(4)  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	AgID	  INTEGER  NOT NULL ,
	StundenAnzahl	  INTEGER  NOT NULL ,
	AnzahlKMabgerechnet  INTEGER  NOT NULL,
	rehaVon		  DATE  NULL ,
	rehaBis		  DATE  NULL 
);

ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  PKAg_PgProMonat PRIMARY KEY (Monat,Jahr,PaID,AgID);



CREATE TABLE PersoenlicherAssistent
(
	Aktiv		  BOOLEAN  NULL ,
	PaID	  INTEGER  NOT NULL ,
	AbgabeDatumUnterlagen  DATE  NULL 
);



CREATE UNIQUE INDEX XPKPersoenlicherAssistent ON PersoenlicherAssistent
(PaID  ASC);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  PKPersoenlicherAssistent PRIMARY KEY (PaID);



CREATE TABLE Person
(
	PersonID	  INTEGER  NOT NULL ,
	eMail		  VARCHAR(30)  NULL ,
	VorName		  VARCHAR(20)  NOT NULL ,
	NachName	  VARCHAR(20)  NOT NULL ,
	TelNummer	  NUMERIC(20)  NULL ,
	AdressID	  INTEGER  NULL ,
	MobilTelefonnummer  NUMERIC(20)  NULL 
);



CREATE UNIQUE INDEX XPKPerson ON Person
(PersonID  ASC);


ALTER TABLE Person
	ADD CONSTRAINT  PKPerson PRIMARY KEY (PersonID);



ALTER TABLE Auftraggeber
	ADD CONSTRAINT  is_a FOREIGN KEY (AgID) REFERENCES Person(PersonID) ON DELETE CASCADE;



ALTER TABLE Auftraggeber
	ADD CONSTRAINT  hat_Kontaktperson FOREIGN KEY (Kontaktperson) REFERENCES Person(PersonID) ON DELETE SET NULL;



ALTER TABLE Auftraggeberdaten
	ADD CONSTRAINT  FK_Ag_Daten FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);



ALTER TABLE DienstVerhaeltnis
	ADD CONSTRAINT  FK_Ag_DV FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);



ALTER TABLE DienstVerhaeltnis
	ADD CONSTRAINT  FK_Pa_DV FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Dokumente
	ADD CONSTRAINT  FK_Pa_Dok FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  FK_Pa_MA FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  FK_DatenAg_MA FOREIGN KEY (Monat,Jahr,AgID) REFERENCES Auftraggeberdaten(Monat,Jahr,AgID);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  is_a FOREIGN KEY (PaID) REFERENCES Person(PersonID) ON DELETE CASCADE;



ALTER TABLE Person
	ADD CONSTRAINT  hat FOREIGN KEY (AdressID) REFERENCES Adresse(AdressID) ON DELETE SET NULL;

CREATE TABLE Leistungseintrag
(
	EintragID	  INTEGER  NOT NULL ,
	Monat		  Numeric(2)  NOT NULL ,
	Jahr		  Numeric(4)  NOT NULL ,
	PaID	  	  INTEGER  NOT NULL ,
	AgID	  	  INTEGER  NOT NULL ,
	von		  VARCHAR(20)  NULL ,
	bis		  VARCHAR(20)  NULL ,
	Tag		  INTEGER  NULL ,
	abrechenbareKM	  INTEGER  NULL ,
	Taetigkeit1	  INTEGER  NULL ,
	Taetigkeit2 	  INTEGER  NULL,
	Taetigkeit3 	  INTEGER  NULL
);



CREATE UNIQUE INDEX IndexLeistungseintrag ON Leistungseintrag
(EintragID  ASC);



ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  XPKLeistungseintrag PRIMARY KEY (EintragID,Monat,Jahr,AgID,PaID);


CREATE TABLE Taetigkeit
(
	ID		  INTEGER  NOT NULL ,
	Name		  VARCHAR(50)  NULL 
);



CREATE UNIQUE INDEX IndexTaetigkeit ON Taetigkeit
(ID  ASC);



ALTER TABLE Taetigkeit
	ADD CONSTRAINT  XPKTaetigkeit PRIMARY KEY (ID);


ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  R_20 FOREIGN KEY (Monat,Jahr,PaID, AgID) REFERENCES Monatsabrechnung(Monat,Jahr,PaID, AgID);



ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  R_21 FOREIGN KEY (Taetigkeit1) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;

ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  R_22 FOREIGN KEY (Taetigkeit2) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;

ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  R_23 FOREIGN KEY (Taetigkeit3) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;


	

DROP SEQUENCE PersonIDGen;
CREATE SEQUENCE PersonIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;

DROP SEQUENCE AdressIDGen;
CREATE SEQUENCE AdressIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;

INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'AG1@gmail.com','Hans','Auftraggeber1',06994444,NULL,NULL);
