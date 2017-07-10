
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



CREATE UNIQUE INDEX XPKDatenAg_Monat ON Auftraggeberdaten
(Monat  ASC,Jahr  ASC,AgID  ASC);



ALTER TABLE Auftraggeberdaten
	ADD CONSTRAINT  PKDatenAg_Monat PRIMARY KEY (Monat,Jahr,AgID);



CREATE TABLE DienstVerhaeltnis
(
	AgID	  INTEGER  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	Dienstvertrag	  VARCHAR(50)  NULL 
);



CREATE UNIQUE INDEX XPKDienstVerhaeltnis ON DienstVerhaeltnis
(PaID  ASC, AgID  ASC);



ALTER TABLE DienstVerhaeltnis
	ADD CONSTRAINT  PKDienstVerhaeltnis PRIMARY KEY (PaID, AgID);



CREATE TABLE Dokumente
(
	Name		  VARCHAR(20)  NOT NULL ,
	Datum		  DATE  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	File		  VARCHAR(50)  NULL ,
	erforderlich	  BOOLEAN  NULL 
);



CREATE UNIQUE INDEX XPKDokumente ON Dokumente
(Name  ASC,PaID  ASC,Datum  ASC);



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



CREATE UNIQUE INDEX XPKAg_PgProMonat ON Monatsabrechnung
(Monat  ASC,Jahr  ASC,PaID  ASC,AgID  ASC);



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


	
--INSERTS------------------------------------------------------------------------------------------

--AG1 - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (01,'Hauptweg','Linz','Oesterreich',1,1,1,4020,1);
INSERT INTO PERSON VALUES (1,'AG1@gmail.com','Hans','Auftraggeber1',06994444,1,NULL);
INSERT INTO AUFTRAGGEBER VALUES (true,to_date('05 Dec 2013', 'DD Mon YYYY'),to_date('05 Dec 2016', 'DD Mon YYYY'),to_date('05 Dec 2012', 'DD Mon YYYY'),1,NULL);
--AG2
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (02,'Nebenweg','Mainz','Deutschland',2,2,2,1020,2);
INSERT INTO PERSON VALUES (02,'AG2@gmail.com','Fritz','Auftraggeber2',0699222222,2,NULL);
INSERT INTO AUFTRAGGEBER VALUES (true,to_date('06 Dec 2013', 'DD Mon YYYY'),to_date('06 Dec 2016', 'DD Mon YYYY'),to_date('06 Dec 2012', 'DD Mon YYYY'),02,NULL);

--AG1Daten - Example
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,02,2015,1,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,02,2015,2,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,03,2015,1,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,03,2015,2,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,04,2015,1,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,04,2015,2,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,05,2015,1,0.50,30,2,1400);
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,05,2015,2,0.50,30,2,1400);


--PA - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (10,'Hauptstra�e','Linz','Oesterreich',2,2,2,4022,2);
INSERT INTO PERSON VALUES (10,'PA10@gmail.com','Peter','PA2',069922222,10,NULL);
INSERT INTO persoenlicherassistent VALUES (true,10,to_date('01 Dec 2016', 'DD Mon YYYY'));

--PA - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (11,'Mausweg','Paris','Frankreich',11,11,11,7022,11);
INSERT INTO PERSON VALUES (11,'PA11@gmail.com','Peter','PA2',069977777,11,NULL);
INSERT INTO persoenlicherassistent VALUES (true,11,to_date('02 Dec 2016', 'DD Mon YYYY'));

--Monatsabrechnung - Example
INSERT INTO Monatsabrechnung Values (50,02,2015,10,1,20,40);
INSERT INTO Monatsabrechnung Values (30,02,2015,11,2,20,40);
INSERT INTO Monatsabrechnung Values (50,03,2015,10,1,20,40);
INSERT INTO Monatsabrechnung Values (30,03,2015,11,2,20,40);
INSERT INTO Monatsabrechnung Values (50,04,2015,10,1,20,40);
INSERT INTO Monatsabrechnung Values (30,04,2015,11,2,20,40);
INSERT INTO Monatsabrechnung Values (50,05,2015,10,1,20,40);
INSERT INTO Monatsabrechnung Values (30,05,2015,11,2,20,40);

--Dienstverhaeltnis
INSERT INTO Dienstverhaeltnis Values (1,10,'C:\Dok\Dienstvertrag1zu10.pdf');
INSERT INTO Dienstverhaeltnis Values (2,11,'C:\Dok\Dienstvertrag2zu11.pdf');

--Dokumente
INSERT INTO dokumente Values ('Zeugnis10',to_date('02 Dec 2013', 'DD Mon YYYY'),10,'C:\Dok\Zeugnis10.pdf',true);
INSERT INTO dokumente Values ('Zeugnis11',to_date('03 Dec 2013', 'DD Mon YYYY'),11,'C:\Dok\Zeugnis11.pdf',true);


--VIEWS------------------------------------------------------------------


--VIEW PersoenlicherAssistent (PersonInfos+Adressinfos+PaInfos)
--DROP VIEW PersoenlicherAssistent CASCADE;
CREATE OR REPLACE VIEW PersoenliecherAssistentView AS
SELECT p.Personid as PersonID, p.Vorname as Vorname, p.Nachname as Nachname, 
       p.email as EMail,p.telnummer as Telefon, p.mobiltelefonnummer as Mobil,
       a.stadt as Stadt, a.PLZ as PLZ,a.Strasse as Strasse, a.hausnummer as Hausnummer,
       a.stock as Stock, a.tuer as Tuer, pa.abgabedatumunterlagen as AbgabeUnterlagenBis,pa.aktiv as Aktiv
FROM Person p
INNER JOIN Adresse a ON (p.adressid = a.adressid)
INNER JOIN Persoenlicherassistent pa ON (p.personid = pa.paid);

--Select * From PersoenliecherAssistentView;


--VIEW Auftraggeber (PersonInfos+AdressInfos+AGInfos)
CREATE OR REPLACE VIEW AuftraggeberView AS
SELECT p.Personid as PersonID, p.Vorname as Vorname, p.Nachname as Nachname, 
       p.email as EMail,p.telnummer as Telefon, p.mobiltelefonnummer as Mobil,
       a.stadt as Stadt, a.PLZ as PLZ,a.Strasse as Strasse, a.hausnummer as Hausnummer,
       a.stock as Stock, a.tuer as Tuer, ag.eintrittsdatum as Eintrittsdatum,
       ag.bewilligungsanfang as Bewilligungsanfang, ag.bewilligungsende as Bewilligungsende,
       ag.kontaktperson as Kontaktperson, ag.aktiv as Aktiv
FROM Person p
INNER JOIN Adresse a ON (p.adressid = a.adressid)
INNER JOIN Auftraggeber ag ON (p.personid = ag.agid);

--Select * From AuftraggeberView;