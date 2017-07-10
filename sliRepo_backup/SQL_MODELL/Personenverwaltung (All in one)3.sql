

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
DROP TABLE dienst;
DROP TABLE Users;
DROP TABLE Reha;

CREATE TABLE Users
(
	username	VARCHAR(30) NOT NULL,
	passwort	VARCHAR(30) NOT NULL
);


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
	Kontaktperson INTEGER NULL,
	HatEinfuehrungskurs	BOOLEAN NOT NULL,
	HatKontrakt		BOOLEAN NOT NULL,
	BezirksHauptmannschaft VARCHAR(30) NULL
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
	EinkommenMonat	  NUMERIC(7,2)  NOT NULL,
	agdatenid	  INTEGER NOT NULL
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

CREATE TABLE Reha
(
	AgID	 	INTEGER  NOT NULL ,
	monat		Numeric(2)  NOT NULL,
	jahr		Numeric(4)  NOT NULL,
	tage 		INTEGER NOT NULL
);



ALTER TABLE Reha
	ADD CONSTRAINT  PKReha PRIMARY KEY (AgID, monat, jahr);

ALTER TABLE Reha
	ADD CONSTRAINT  FK_Ag_Reha FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);
	
CREATE TABLE Monatsabrechnung
(
	AnzahPrivatlKM	  INTEGER  NOT NULL ,
	Monat		  Numeric(2)  NOT NULL ,
	Jahr		  Numeric(4)  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	AgID	  INTEGER  NOT NULL ,
	StundenAnzahl	  INTEGER  NOT NULL ,
	AnzahlKMabgerechnet  INTEGER  NOT NULL
	
);

ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  PKAg_PgProMonat PRIMARY KEY (Monat,Jahr,PaID,AgID);



CREATE TABLE PersoenlicherAssistent
(
	Aktiv		  BOOLEAN  NULL ,
	PaID	  INTEGER  NOT NULL ,
	AbgabeDatumUnterlagen  DATE  NULL,
	SV		BOOLEAN NULL,
	Dienstvertrag	BOOLEAN NULL,
	BestaetigungBH	BOOLEAN NULL,
	Grundkurs	BOOLEAN NULL,
	DeadLineWeiterbildung DATE NULL,
	WeiterbildungsStunden NUMERIC(7,2)
);



CREATE UNIQUE INDEX XPKPersoenlicherAssistent ON PersoenlicherAssistent
(PaID  ASC);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  PKPersoenlicherAssistent PRIMARY KEY (PaID);



CREATE TABLE Person
(
	PersonID	  INTEGER  NOT NULL ,
	eMail		  VARCHAR(50)  NULL ,
	VorName		  VARCHAR(20)  NOT NULL ,
	NachName	  VARCHAR(20)  NOT NULL ,
	TelNummer	  VARCHAR(20)  NULL ,
	AdressID	  INTEGER  NULL ,
	MobilTelefonnummer  VARCHAR(20)  NULL,
	IBAN		  VARCHAR(20) NULL,
	BIC		  VARCHAR(20) NULL,
	Kontoinhaber	  VARCHAR(50) NULL,
	SVN		  NUMERIC(10) NULL,	
	Staatszugehoerigkeit	VARCHAR(30) NULL,
	Info		  TEXT NULL
	 
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
	ADD CONSTRAINT  FK_Ag_MA FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  is_a FOREIGN KEY (PaID) REFERENCES Person(PersonID) ON DELETE CASCADE;



ALTER TABLE Person
	ADD CONSTRAINT  hat FOREIGN KEY (AdressID) REFERENCES Adresse(AdressID) ON DELETE SET NULL;

CREATE TABLE dienst
(
  begin date NOT NULL,
  paid integer NOT NULL,
  ende date NULL,
  CONSTRAINT pkdienst PRIMARY KEY (begin, paid)
);

ALTER TABLE dienst
	ADD CONSTRAINT  dienstVon FOREIGN KEY (paid) REFERENCES PersoenlicherAssistent(PaID) ON DELETE CASCADE;
	
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
	ADD CONSTRAINT  LeistungseintragFK FOREIGN KEY (Monat,Jahr,PaID, AgID) REFERENCES Monatsabrechnung(Monat,Jahr,PaID, AgID);



ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  FK_Taetigkeit1 FOREIGN KEY (Taetigkeit1) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;

ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  FK_Taetigkeit2 FOREIGN KEY (Taetigkeit2) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;

ALTER TABLE Leistungseintrag
	ADD CONSTRAINT  FK_Taetigkeit3 FOREIGN KEY (Taetigkeit3) REFERENCES Taetigkeit(ID) ON DELETE SET NULL;

--SEQUENCE-----------------------------------------------------------------------------------------
DROP SEQUENCE PersonIDGen;
CREATE SEQUENCE PersonIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;

DROP SEQUENCE AddressIDGen;
CREATE SEQUENCE AddressIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;

DROP SEQUENCE EintragIDGen;
CREATE SEQUENCE EintragIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;

DROP SEQUENCE AGDatenIDGen;
CREATE SEQUENCE AGDatenIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;
	
--INSERTS------------------------------------------------------------------------------------------

--Taetigkeit---
INSERT INTO Taetigkeit (ID,Name) VALUES (1,'Nachtbereitschaft');
INSERT INTO Taetigkeit (ID,Name) VALUES (2,'Unterstützung bei der Grundversorgung');
INSERT INTO Taetigkeit (ID,Name) VALUES (3,'Hauswirtschafltiche Tätigkeiten');
INSERT INTO Taetigkeit (ID,Name) VALUES (4,'Begleitung und Mobilität');
INSERT INTO Taetigkeit (ID,Name) VALUES (5,'Freizeitgestaltung');
INSERT INTO Taetigkeit (ID,Name) VALUES (6,'Unterstützung bei jeder Form der Kommunikation');


--Kontaktperson
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (nextval('AddressIDGen'),'Kontaktstrasse','Linz','Engerwitzdorf',2,NULL,NULL,4209,1);
INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'kontakt@gmx.de','Brigitte','Mutter','+43 699 22 22 22',1,'+7463546384',null,null,null,null,null);


--AG1 - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (nextval('AddressIDGen'),'Hauptweg','Linz','Österreich',1,1,1,4020,1);
INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'AG1@gmail.com','Hans','AuftraggeberEins','06994444',2,'8888888',NULL,NULL,NULL,'2545',NULL,NULL);
INSERT INTO AUFTRAGGEBER VALUES (true,to_date('05 Dec 2013', 'DD Mon YYYY'),to_date('05 Dec 2016', 'DD Mon YYYY'),to_date('05 Dec 2012', 'DD Mon YYYY'),2,1,TRUE,TRUE,'Wien');
--AG2
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (nextval('AddressIDGen'),'Nebenstraße','Mainz','Deutschland',2,2,2,1020,2);
INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'eierlegendewollmilchsau@gmail.com','Fritz','AuftraggeberZwei','+43 699 22 22 22',3,'+43 699 22 22 22','32457348753987','GATFSB5G','Kontoinhaber 1','7453','Deutschland',NULL);
INSERT INTO AUFTRAGGEBER VALUES (true,to_date('06 Dec 2013', 'DD Mon YYYY'),to_date('06 Dec 2016', 'DD Mon YYYY'),to_date('06 Dec 2012', 'DD Mon YYYY'),3,NULL,TRUE,TRUE,'Linz');




--AG1Daten - Example
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,02,2015,3,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,02,2015,2,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,03,2015,3,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,03,2015,2,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,04,2015,3,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,04,2015,2,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,05,2015,3,0.50,30,2,1400,nextval('AGDatenIDGen'));
INSERT INTO AUFTRAGGEBERDATEN VALUES (0.30,8.50,8.50,200,05,2015,2,0.50,30,2,1400,nextval('AGDatenIDGen'));

--PA - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (nextval('AddressIDGen'),'Hauptstraße','Linz','Österreich',2,2,2,4022,2);
INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'PA10@gmail.com','Susi','Dumhart','069922222',4,NULL,NULL,NULL,NULL,'2545080789',NULL,NULL);
INSERT INTO persoenlicherassistent VALUES (true,4,to_date('01 Dec 2016', 'DD Mon YYYY'),false,true,false,true,to_date('12 Mar 2017', 'DD Mon YYYY'),6);
INSERT INTO dienst VALUES (to_date('12 Mar 2015', 'DD Mon YYYY'),4,to_date('12 Mar 2016', 'DD Mon YYYY'));
INSERT INTO dienst VALUES (to_date('12 Oct 2016', 'DD Mon YYYY'),4,null);

--PA - Example
INSERT INTO ADRESSE (AdressID,Strasse,Stadt,Land,HausNummer,Stiege,Stock,PLZ,Tuer) VALUES (nextval('AddressIDGen'),'Mausweg','Paris','Frankreich',11,11,11,7022,11);
INSERT INTO PERSON VALUES (nextval('PersonIDGen'),'PA11@gmail.com','Peter','Außerwöger','069977777',5,'0664 22 03 2374','0984570923475','JSIZG123','Kontoinhaber 2','2545080789','Mittelerde',NULL);
INSERT INTO persoenlicherassistent VALUES (true,5,to_date('02 Dec 2016', 'DD Mon YYYY'),true,true,true,true,to_date('12 Mar 2016', 'DD Mon YYYY'),10);

--Monatsabrechnung - Example

INSERT INTO Monatsabrechnung Values (50,12,2014,4,2,20,40);
INSERT INTO Monatsabrechnung Values (50,12,2014,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,01,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (50,01,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,02,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (50,02,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,03,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,03,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,04,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,04,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,05,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,05,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,06,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (50,06,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,07,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,07,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,08,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,08,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,09,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,09,2015,5,3,20,40);
INSERT INTO Monatsabrechnung Values (50,10,2015,4,2,20,40);
INSERT INTO Monatsabrechnung Values (30,10,2015,5,3,20,40);

INSERT INTO Reha Values (2,3,2015,20);
INSERT INTO Reha Values (2,12,2015,10);

--Dienstverhaeltnis
INSERT INTO Dienstverhaeltnis Values (2,4,'C:\Dok\Dienstvertrag1zu10.pdf');
INSERT INTO Dienstverhaeltnis Values (3,5,'C:\Dok\Dienstvertrag2zu11.pdf');

--Dokumente
INSERT INTO dokumente Values ('Zeugnis10',to_date('02 Dec 2013', 'DD Mon YYYY'),4,'C:\Dok\Zeugnis10.pdf',true);
INSERT INTO dokumente Values ('Zeugnis11',to_date('03 Dec 2013', 'DD Mon YYYY'),5,'C:\Dok\Zeugnis11.pdf',true);

--Users
INSERT INTO users Values('Karin','sli');
INSERT INTO users Values('Michael','sli');
INSERT INTO users Values('Admin','sli');


-- LA für PA 4 und AG 2 für Monatsabrechnungen von Feb-Mai 2015
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'10:00','14:00',3,10,3,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'08:00','12:00',4,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'09:00','13:00',5,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,4,2,'10:00','14:00',6,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,4,2,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,4,2,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,4,2,'10:00','14:00',3,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'10:00','14:00',3,10,3,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'08:00','12:00',4,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'09:00','13:00',5,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,4,2,'10:00','14:00',6,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,4,2,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,4,2,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,4,2,'10:00','14:00',3,10,3,null,null);

-- LA für PA 5 und AG 3 für Monatsabrechnungen von Feb-Mai 2015
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'10:00','14:00',3,10,3,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'08:00','12:00',4,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'09:00','13:00',5,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),2,2015,5,3,'10:00','14:00',6,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,5,3,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,5,3,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),3,2015,5,3,'10:00','14:00',3,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'10:00','14:00',3,10,3,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'08:00','12:00',4,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'09:00','13:00',5,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),4,2015,5,3,'10:00','14:00',6,10,3,null,null);

Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,5,3,'08:00','12:00',1,10,1,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,5,3,'09:00','13:00',2,10,2,null,null);
Insert INTO leistungseintrag Values(nextval('eintragIdgen'),5,2015,5,3,'10:00','14:00',3,10,3,null,null);



--VIEWS------------------------------------------------------------------


--VIEW PersoenlicherAssistent (PersonInfos+Adressinfos+PaInfos)
--DROP VIEW PersoenlicherAssistent CASCADE;
CREATE OR REPLACE VIEW PersoenliecherAssistentView AS
SELECT p.Personid as PersonID, p.Vorname as Vorname, p.Nachname as Nachname, 
       p.email as EMail,p.telnummer as Telefon, p.mobiltelefonnummer as Mobil,
	   p.iban as IBAN, p.bic as BIC, p.kontoinhaber as Kontoinhaber,  p.svn as SVN, p.Staatszugehoerigkeit as Staatszugehoerigkeit,
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
       p.iban as IBAN, p.bic as BIC, p.kontoinhaber as Kontoinhaber, p.svn as SVN, p.Staatszugehoerigkeit as Staatszugehoerigkeit,
       a.stadt as Stadt, a.PLZ as PLZ,a.Strasse as Strasse, a.hausnummer as Hausnummer,
       a.stock as Stock, a.tuer as Tuer, ag.eintrittsdatum as Eintrittsdatum,
       ag.bewilligungsanfang as Bewilligungsanfang, ag.bewilligungsende as Bewilligungsende,
       ag.kontaktperson as Kontaktperson, ag.aktiv as Aktiv, ag.HatEinfuehrungskurs as HatEinfuehrungskurs,
	   ag.HatKontrakt as HatKontrakt 
FROM Person p
INNER JOIN Adresse a ON (p.adressid = a.adressid)
INNER JOIN Auftraggeber ag ON (p.personid = ag.agid);

--Select * From AuftraggeberView;