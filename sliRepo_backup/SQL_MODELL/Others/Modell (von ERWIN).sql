
DROP TABLE Dokumente CASCADE CONSTRAINTS PURGE;
DROP TABLE Monatsabrechnung CASCADE CONSTRAINTS PURGE;
DROP TABLE Auftragsgeberdaten CASCADE CONSTRAINTS PURGE;
DROP TABLE DienstVerhältnis CASCADE CONSTRAINTS PURGE;
DROP TABLE PersoenlicherAssistent CASCADE CONSTRAINTS PURGE;
DROP TABLE Auftraggeber CASCADE CONSTRAINTS PURGE;
DROP TABLE Person CASCADE CONSTRAINTS PURGE;
DROP TABLE Adresse CASCADE CONSTRAINTS PURGE;



CREATE TABLE Adresse
(
	AdressID	  INTEGER  NOT NULL ,
	Strasse		  VARCHAR2(30)  NULL ,
	Stadt		  VARCHAR2(20)  NULL ,
	Land		  VARCHAR2(20)  NULL ,
	HausNummer	  INTEGER  NULL ,
	Stiege		  INTEGER  NULL ,
	Stock		  INTEGER  NULL ,
	PLZ		  INTEGER  NULL ,
	Tuer		  INTEGER  NULL 
);



CREATE UNIQUE INDEX XPKAdresse ON Adresse
(AdressID  ASC);



ALTER TABLE Adresse
	ADD CONSTRAINT  XPKAdresse PRIMARY KEY (AdressID);



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
	ADD CONSTRAINT  XPKAuftraggeber PRIMARY KEY (AgID);



CREATE TABLE Auftragsgeberdaten
(
	FahrtkostenzusatzKM  INTEGER  NOT NULL ,
	StundensatzAuszahlung  INTEGER  NOT NULL ,
	StundenSatz	  INTEGER  NOT NULL ,
	BeitragEinkommen  INTEGER  NOT NULL ,
	Monat		  DATE  NOT NULL ,
	Jahr		  DATE  NOT NULL ,
	AgID	  INTEGER  NOT NULL ,
	FahrtkostenZusatz  INTEGER  NOT NULL ,
	BetreuungsbedarfH  INTEGER  NOT NULL ,
	PflegegeldStufe	  INTEGER  NOT NULL ,
	EinkommenMonat	  INTEGER  NOT NULL 
);



CREATE UNIQUE INDEX XPKDatenAg_Monat ON Auftragsgeberdaten
(Monat  ASC,Jahr  ASC,PersonID  ASC);



ALTER TABLE Auftragsgeberdaten
	ADD CONSTRAINT  XPKDatenAg_Monat PRIMARY KEY (Monat,Jahr,AgID);



CREATE TABLE DienstVerhältnis
(
	AgID	  INTEGER  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	Dienstvertrag	  VARCHAR2(50)  NULL 
);



CREATE UNIQUE INDEX XPKDienstVerhältnis ON DienstVerhältnis
(PaID  ASC, AgID  ASC);



ALTER TABLE DienstVerhältnis
	ADD CONSTRAINT  XPKDienstVerhältnis PRIMARY KEY (PaID, AgID);



CREATE TABLE Dokumente
(
	Name		  VARCHAR2(20)  NOT NULL ,
	Datum		  DATE  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	File		  VARCHAR2(50)  NULL ,
	erforderlich	  BOOLEAN  NULL 
);



CREATE UNIQUE INDEX XPKDokumente ON Dokumente
(Name  ASC,PaID  ASC,Datum  ASC);



ALTER TABLE Dokumente
	ADD CONSTRAINT  XPKDokumente PRIMARY KEY (Name,PaID,Datum);



CREATE TABLE Monatsabrechnung
(
	AnzahPrivatlKM	  INTEGER  NOT NULL ,
	Monat		  DATE  NOT NULL ,
	Jahr		  DATE  NOT NULL ,
	PaID	  INTEGER  NOT NULL ,
	AgID	  INTEGER  NOT NULL ,
	StundenAnzahl	  INTEGER  NOT NULL ,
	AnzahlKMabgerechnet  INTEGER  NOT NULL 
);



CREATE UNIQUE INDEX XPKAg_PgProMonat ON Monatsabrechnung
(Monat  ASC,Jahr  ASC,PaID  ASC,AgID  ASC);



ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  XPKAg_PgProMonat PRIMARY KEY (Monat,Jahr,PaID,AgID);



CREATE TABLE PersoenlicherAssistent
(
	Aktiv		  BOOLEAN  NULL ,
	PaID	  INTEGER  NOT NULL ,
	AbgabeDatumUnterlagen  DATE  NULL 
);



CREATE UNIQUE INDEX XPKPersoenlicherAssistent ON PersoenlicherAssistent
(PaID  ASC);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  XPKPersoenlicherAssistent PRIMARY KEY (PaID);



CREATE TABLE Person
(
	PersonID	  INTEGER  NOT NULL ,
	eMail		  VARCHAR2(30)  NULL ,
	VorName		  VARCHAR2(20)  NOT NULL ,
	NachName	  VARCHAR2(20)  NOT NULL ,
	TelNummer	  NUMERIC(20)  NULL ,
	AdressID	  INTEGER  NULL ,
	MobilTelefonnummer  NUMERIC(20)  NULL 
);



CREATE UNIQUE INDEX XPKPerson ON Person
(PersonID  ASC);



ALTER TABLE Person
	ADD CONSTRAINT  XPKPerson PRIMARY KEY (PersonID);



ALTER TABLE Auftraggeber
	ADD CONSTRAINT  is_a FOREIGN KEY (AgID) REFERENCES Person(PersonID) ON DELETE CASCADE;



ALTER TABLE Auftraggeber
	ADD CONSTRAINT  hat_Kontaktperson FOREIGN KEY (Kontaktperson) REFERENCES Person(PersonID) ON DELETE SET NULL;



ALTER TABLE Auftragsgeberdaten
	ADD CONSTRAINT  FK_Ag_Daten FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);



ALTER TABLE DienstVerhältnis
	ADD CONSTRAINT  FK_Ag_DV FOREIGN KEY (AgID) REFERENCES Auftraggeber(AgID);



ALTER TABLE DienstVerhältnis
	ADD CONSTRAINT  FK_Pa_DV FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Dokumente
	ADD CONSTRAINT  FK_Pa_Dok FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  FK_Pa_MA FOREIGN KEY (PaID) REFERENCES PersoenlicherAssistent(PaID);



ALTER TABLE Monatsabrechnung
	ADD CONSTRAINT  FK_DatenAg_MA FOREIGN KEY (Monat,Jahr,AgID) REFERENCES Auftragsgeberdaten(Monat,Jahr,AgID);



ALTER TABLE PersoenlicherAssistent
	ADD CONSTRAINT  is_a FOREIGN KEY (PaID) REFERENCES Person(PersonID) ON DELETE CASCADE;



ALTER TABLE Person
	ADD CONSTRAINT  hat FOREIGN KEY (AdressID) REFERENCES Adresse(AdressID) ON DELETE SET NULL;



CREATE  TRIGGER tD_Adresse AFTER DELETE ON Adresse for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- DELETE trigger on Adresse 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Adresse R/13 Person on parent delete set null */
    /* ERWIN_RELATION:CHECKSUM="0000ba96", PARENT_OWNER="", PARENT_TABLE="Adresse"
    CHILD_OWNER="", CHILD_TABLE="Person"
    P2C_VERB_PHRASE="R/13", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_13", FK_COLUMNS="AdressID" */
    UPDATE Person
      SET
        /* %SetFK(Person,NULL) */
        Person.AdressID = NULL
      WHERE
        /* %JoinFKPK(Person,:%Old," = "," AND") */
        Person.AdressID = :old.AdressID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Adresse AFTER UPDATE ON Adresse for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Adresse 
DECLARE NUMROWS INTEGER;
BEGIN
  /* Adresse R/13 Person on parent update set null */
  /* ERWIN_RELATION:CHECKSUM="0000deeb", PARENT_OWNER="", PARENT_TABLE="Adresse"
    CHILD_OWNER="", CHILD_TABLE="Person"
    P2C_VERB_PHRASE="R/13", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_13", FK_COLUMNS="AdressID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Adresse.AdressID <> Adresse.AdressID
  THEN
    UPDATE Person
      SET
        /* %SetFK(Person,NULL) */
        Person.AdressID = NULL
      WHERE
        /* %JoinFKPK(Person,:%Old," = ",",") */
        Person.AdressID = :old.AdressID;
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_Auftraggeber BEFORE INSERT ON Auftraggeber for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on Auftraggeber 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person is a Auftraggeber on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="0002235a", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Person
      WHERE
        /* %JoinFKPK(:%New,Person," = "," AND") */
        :new.PersonID = Person.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert Auftraggeber because Person does not exist.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person hat Kontaktperson Auftraggeber on child insert set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="hat Kontaktperson", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="hat_Kontaktperson", FK_COLUMNS="PersonID" */
    UPDATE Auftraggeber
      SET
        /* %SetFK(Auftraggeber,NULL) */
        Auftraggeber.PersonID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM Person
            WHERE
              /* %JoinFKPK(:%New,Person," = "," AND") */
              :new.PersonID = Person.PersonID
        ) 
        /* %JoinPKPK(Auftraggeber,:%New," = "," AND") */
         and Auftraggeber.PersonID = Auftraggeber.PersonID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tD_Auftraggeber AFTER DELETE ON Auftraggeber for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- DELETE trigger on Auftraggeber 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftraggeber R/16 Auftragsgeberdaten on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="00022737", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="Auftragsgeberdaten"
    P2C_VERB_PHRASE="R/16", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_16", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Auftragsgeberdaten
      WHERE
        /*  %JoinFKPK(Auftragsgeberdaten,:%Old," = "," AND") */
        Auftragsgeberdaten.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete Auftraggeber because Auftragsgeberdaten exists.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftraggeber R/18 DienstVerhältnis on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/18", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_18", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM DienstVerhältnis
      WHERE
        /*  %JoinFKPK(DienstVerhältnis,:%Old," = "," AND") */
        DienstVerhältnis.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete Auftraggeber because DienstVerhältnis exists.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Auftraggeber AFTER UPDATE ON Auftraggeber for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Auftraggeber 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftraggeber R/16 Auftragsgeberdaten on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="0004a781", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="Auftragsgeberdaten"
    P2C_VERB_PHRASE="R/16", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_16", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Auftraggeber.PersonID <> Auftraggeber.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM Auftragsgeberdaten
      WHERE
        /*  %JoinFKPK(Auftragsgeberdaten,:%Old," = "," AND") */
        Auftragsgeberdaten.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update Auftraggeber because Auftragsgeberdaten exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftraggeber R/18 DienstVerhältnis on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/18", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_18", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Auftraggeber.PersonID <> Auftraggeber.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM DienstVerhältnis
      WHERE
        /*  %JoinFKPK(DienstVerhältnis,:%Old," = "," AND") */
        DienstVerhältnis.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update Auftraggeber because DienstVerhältnis exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Person is a Auftraggeber on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM Person
    WHERE
      /* %JoinFKPK(:%New,Person," = "," AND") */
      :new.PersonID = Person.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update Auftraggeber because Person does not exist.'
    );
  END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person hat Kontaktperson Auftraggeber on child update set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="hat Kontaktperson", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="hat_Kontaktperson", FK_COLUMNS="PersonID" */
    UPDATE Auftraggeber
      SET
        /* %SetFK(Auftraggeber,NULL) */
        Auftraggeber.PersonID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM Person
            WHERE
              /* %JoinFKPK(:%New,Person," = "," AND") */
              :new.PersonID = Person.PersonID
        ) 
        /* %JoinPKPK(Auftraggeber,:%New," = "," AND") */
         and Auftraggeber.PersonID = Auftraggeber.PersonID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_Auftragsgeberdaten BEFORE INSERT ON Auftragsgeberdaten for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on Auftragsgeberdaten 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftraggeber R/16 Auftragsgeberdaten on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00010df0", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="Auftragsgeberdaten"
    P2C_VERB_PHRASE="R/16", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_16", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Auftraggeber
      WHERE
        /* %JoinFKPK(:%New,Auftraggeber," = "," AND") */
        :new.PersonID = Auftraggeber.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert Auftragsgeberdaten because Auftraggeber does not exist.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tD_Auftragsgeberdaten AFTER DELETE ON Auftragsgeberdaten for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- DELETE trigger on Auftragsgeberdaten 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftragsgeberdaten R/17 Monatsabrechnung on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="00012bc4", PARENT_OWNER="", PARENT_TABLE="Auftragsgeberdaten"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/17", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_17", FK_COLUMNS="Monat""Jahr""PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Monatsabrechnung
      WHERE
        /*  %JoinFKPK(Monatsabrechnung,:%Old," = "," AND") */
        Monatsabrechnung.Monat = :old.Monat AND
        Monatsabrechnung.Jahr = :old.Jahr AND
        Monatsabrechnung.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete Auftragsgeberdaten because Monatsabrechnung exists.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Auftragsgeberdaten AFTER UPDATE ON Auftragsgeberdaten for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Auftragsgeberdaten 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftragsgeberdaten R/17 Monatsabrechnung on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="0002b6ab", PARENT_OWNER="", PARENT_TABLE="Auftragsgeberdaten"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/17", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_17", FK_COLUMNS="Monat""Jahr""PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Auftragsgeberdaten.Monat <> Auftragsgeberdaten.Monat OR 
    Auftragsgeberdaten.Jahr <> Auftragsgeberdaten.Jahr OR 
    Auftragsgeberdaten.PersonID <> Auftragsgeberdaten.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM Monatsabrechnung
      WHERE
        /*  %JoinFKPK(Monatsabrechnung,:%Old," = "," AND") */
        Monatsabrechnung.Monat = :old.Monat AND
        Monatsabrechnung.Jahr = :old.Jahr AND
        Monatsabrechnung.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update Auftragsgeberdaten because Monatsabrechnung exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftraggeber R/16 Auftragsgeberdaten on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="Auftragsgeberdaten"
    P2C_VERB_PHRASE="R/16", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_16", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM Auftraggeber
    WHERE
      /* %JoinFKPK(:%New,Auftraggeber," = "," AND") */
      :new.PersonID = Auftraggeber.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update Auftragsgeberdaten because Auftraggeber does not exist.'
    );
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_DienstVerhältnis BEFORE INSERT ON DienstVerhältnis for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on DienstVerhältnis 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftraggeber R/18 DienstVerhältnis on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00024685", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/18", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_18", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Auftraggeber
      WHERE
        /* %JoinFKPK(:%New,Auftraggeber," = "," AND") */
        :new.PersonID = Auftraggeber.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert DienstVerhältnis because Auftraggeber does not exist.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/19 DienstVerhältnis on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/19", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_19", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM PersoenlicherAssistent
      WHERE
        /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
        :new.PersonID = PersoenlicherAssistent.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert DienstVerhältnis because PersoenlicherAssistent does not exist.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_DienstVerhältnis AFTER UPDATE ON DienstVerhältnis for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on DienstVerhältnis 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftraggeber R/18 DienstVerhältnis on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="000255ae", PARENT_OWNER="", PARENT_TABLE="Auftraggeber"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/18", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_18", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM Auftraggeber
    WHERE
      /* %JoinFKPK(:%New,Auftraggeber," = "," AND") */
      :new.PersonID = Auftraggeber.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update DienstVerhältnis because Auftraggeber does not exist.'
    );
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/19 DienstVerhältnis on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/19", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_19", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM PersoenlicherAssistent
    WHERE
      /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
      :new.PersonID = PersoenlicherAssistent.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update DienstVerhältnis because PersoenlicherAssistent does not exist.'
    );
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_Dokumente BEFORE INSERT ON Dokumente for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on Dokumente 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/9 Dokumente on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00010e2e", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Dokumente"
    P2C_VERB_PHRASE="R/9", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_9", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM PersoenlicherAssistent
      WHERE
        /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
        :new.PersonID = PersoenlicherAssistent.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert Dokumente because PersoenlicherAssistent does not exist.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Dokumente AFTER UPDATE ON Dokumente for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Dokumente 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/9 Dokumente on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00010cdc", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Dokumente"
    P2C_VERB_PHRASE="R/9", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_9", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM PersoenlicherAssistent
    WHERE
      /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
      :new.PersonID = PersoenlicherAssistent.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update Dokumente because PersoenlicherAssistent does not exist.'
    );
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_Monatsabrechnung BEFORE INSERT ON Monatsabrechnung for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on Monatsabrechnung 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/11 Monatsabrechnung on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="000280f5", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/11", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_11", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM PersoenlicherAssistent
      WHERE
        /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
        :new.PersonID = PersoenlicherAssistent.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert Monatsabrechnung because PersoenlicherAssistent does not exist.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Auftragsgeberdaten R/17 Monatsabrechnung on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Auftragsgeberdaten"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/17", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_17", FK_COLUMNS="Monat""Jahr""PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Auftragsgeberdaten
      WHERE
        /* %JoinFKPK(:%New,Auftragsgeberdaten," = "," AND") */
        :new.Monat = Auftragsgeberdaten.Monat AND
        :new.Jahr = Auftragsgeberdaten.Jahr AND
        :new.PersonID = Auftragsgeberdaten.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert Monatsabrechnung because Auftragsgeberdaten does not exist.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Monatsabrechnung AFTER UPDATE ON Monatsabrechnung for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Monatsabrechnung 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/11 Monatsabrechnung on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="000275d1", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/11", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_11", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM PersoenlicherAssistent
    WHERE
      /* %JoinFKPK(:%New,PersoenlicherAssistent," = "," AND") */
      :new.PersonID = PersoenlicherAssistent.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update Monatsabrechnung because PersoenlicherAssistent does not exist.'
    );
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Auftragsgeberdaten R/17 Monatsabrechnung on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Auftragsgeberdaten"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/17", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_17", FK_COLUMNS="Monat""Jahr""PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM Auftragsgeberdaten
    WHERE
      /* %JoinFKPK(:%New,Auftragsgeberdaten," = "," AND") */
      :new.Monat = Auftragsgeberdaten.Monat AND
      :new.Jahr = Auftragsgeberdaten.Jahr AND
      :new.PersonID = Auftragsgeberdaten.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update Monatsabrechnung because Auftragsgeberdaten does not exist.'
    );
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_PersoenlicherAssistent BEFORE INSERT ON PersoenlicherAssistent for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on PersoenlicherAssistent 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person is a PersoenlicherAssistent on child insert restrict */
    /* ERWIN_RELATION:CHECKSUM="0001091c", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="PersoenlicherAssistent"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Person
      WHERE
        /* %JoinFKPK(:%New,Person," = "," AND") */
        :new.PersonID = Person.PersonID;
    IF (
      /* %NotnullFK(:%New," IS NOT NULL AND") */
      
      NUMROWS = 0
    )
    THEN
      raise_application_error(
        -20002,
        'Cannot insert PersoenlicherAssistent because Person does not exist.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tD_PersoenlicherAssistent AFTER DELETE ON PersoenlicherAssistent for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- DELETE trigger on PersoenlicherAssistent 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/9 Dokumente on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="000346d9", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Dokumente"
    P2C_VERB_PHRASE="R/9", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_9", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Dokumente
      WHERE
        /*  %JoinFKPK(Dokumente,:%Old," = "," AND") */
        Dokumente.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete PersoenlicherAssistent because Dokumente exists.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/11 Monatsabrechnung on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/11", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_11", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM Monatsabrechnung
      WHERE
        /*  %JoinFKPK(Monatsabrechnung,:%Old," = "," AND") */
        Monatsabrechnung.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete PersoenlicherAssistent because Monatsabrechnung exists.'
      );
    END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* PersoenlicherAssistent R/19 DienstVerhältnis on parent delete restrict */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/19", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_19", FK_COLUMNS="PersonID" */
    SELECT count(*) INTO NUMROWS
      FROM DienstVerhältnis
      WHERE
        /*  %JoinFKPK(DienstVerhältnis,:%Old," = "," AND") */
        DienstVerhältnis.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN
      raise_application_error(
        -20001,
        'Cannot delete PersoenlicherAssistent because DienstVerhältnis exists.'
      );
    END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_PersoenlicherAssistent AFTER UPDATE ON PersoenlicherAssistent for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on PersoenlicherAssistent 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/9 Dokumente on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="000504ec", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Dokumente"
    P2C_VERB_PHRASE="R/9", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_9", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    PersoenlicherAssistent.PersonID <> PersoenlicherAssistent.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM Dokumente
      WHERE
        /*  %JoinFKPK(Dokumente,:%Old," = "," AND") */
        Dokumente.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update PersoenlicherAssistent because Dokumente exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/11 Monatsabrechnung on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="Monatsabrechnung"
    P2C_VERB_PHRASE="R/11", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_11", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    PersoenlicherAssistent.PersonID <> PersoenlicherAssistent.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM Monatsabrechnung
      WHERE
        /*  %JoinFKPK(Monatsabrechnung,:%Old," = "," AND") */
        Monatsabrechnung.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update PersoenlicherAssistent because Monatsabrechnung exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* PersoenlicherAssistent R/19 DienstVerhältnis on parent update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="PersoenlicherAssistent"
    CHILD_OWNER="", CHILD_TABLE="DienstVerhältnis"
    P2C_VERB_PHRASE="R/19", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_19", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    PersoenlicherAssistent.PersonID <> PersoenlicherAssistent.PersonID
  THEN
    SELECT count(*) INTO NUMROWS
      FROM DienstVerhältnis
      WHERE
        /*  %JoinFKPK(DienstVerhältnis,:%Old," = "," AND") */
        DienstVerhältnis.PersonID = :old.PersonID;
    IF (NUMROWS > 0)
    THEN 
      raise_application_error(
        -20005,
        'Cannot update PersoenlicherAssistent because DienstVerhältnis exists.'
      );
    END IF;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Person is a PersoenlicherAssistent on child update restrict */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="PersoenlicherAssistent"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
  SELECT count(*) INTO NUMROWS
    FROM Person
    WHERE
      /* %JoinFKPK(:%New,Person," = "," AND") */
      :new.PersonID = Person.PersonID;
  IF (
    /* %NotnullFK(:%New," IS NOT NULL AND") */
    
    NUMROWS = 0
  )
  THEN
    raise_application_error(
      -20007,
      'Cannot update PersoenlicherAssistent because Person does not exist.'
    );
  END IF;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/


CREATE  TRIGGER tI_Person BEFORE INSERT ON Person for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- INSERT trigger on Person 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Adresse R/13 Person on child insert set null */
    /* ERWIN_RELATION:CHECKSUM="0000f2a5", PARENT_OWNER="", PARENT_TABLE="Adresse"
    CHILD_OWNER="", CHILD_TABLE="Person"
    P2C_VERB_PHRASE="R/13", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_13", FK_COLUMNS="AdressID" */
    UPDATE Person
      SET
        /* %SetFK(Person,NULL) */
        Person.AdressID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM Adresse
            WHERE
              /* %JoinFKPK(:%New,Adresse," = "," AND") */
              :new.AdressID = Adresse.AdressID
        ) 
        /* %JoinPKPK(Person,:%New," = "," AND") */
         and Person.PersonID = Person.PersonID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tD_Person AFTER DELETE ON Person for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- DELETE trigger on Person 
DECLARE NUMROWS INTEGER;
BEGIN
    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person is a Auftraggeber on parent delete cascade */
    /* ERWIN_RELATION:CHECKSUM="00028c99", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
    DELETE FROM Auftraggeber
      WHERE
        /*  %JoinFKPK(Auftraggeber,:%Old," = "," AND") */
        Auftraggeber.PersonID = :old.PersonID;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person is a PersoenlicherAssistent on parent delete cascade */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="PersoenlicherAssistent"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
    DELETE FROM PersoenlicherAssistent
      WHERE
        /*  %JoinFKPK(PersoenlicherAssistent,:%Old," = "," AND") */
        PersoenlicherAssistent.PersonID = :old.PersonID;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Person hat Kontaktperson Auftraggeber on parent delete set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="hat Kontaktperson", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="hat_Kontaktperson", FK_COLUMNS="PersonID" */
    UPDATE Auftraggeber
      SET
        /* %SetFK(Auftraggeber,NULL) */
        Auftraggeber.PersonID = NULL
      WHERE
        /* %JoinFKPK(Auftraggeber,:%Old," = "," AND") */
        Auftraggeber.PersonID = :old.PersonID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

CREATE  TRIGGER tU_Person AFTER UPDATE ON Person for each row
-- ERwin Builtin Montag, 13. April 2015 10:41:24
-- UPDATE trigger on Person 
DECLARE NUMROWS INTEGER;
BEGIN
  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Person is a Auftraggeber on parent update cascade */
  /* ERWIN_RELATION:CHECKSUM="00043221", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Person.PersonID <> Person.PersonID
  THEN
    UPDATE Auftraggeber
      SET
        /*  %JoinFKPK(Auftraggeber,:%New," = ",",") */
        Auftraggeber.PersonID = :new.PersonID
      WHERE
        /*  %JoinFKPK(Auftraggeber,:%Old," = "," AND") */
        Auftraggeber.PersonID = :old.PersonID;
  END IF;

  /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
  /* Person is a PersoenlicherAssistent on parent update cascade */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="PersoenlicherAssistent"
    P2C_VERB_PHRASE="is a", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="is_a", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Person.PersonID <> Person.PersonID
  THEN
    UPDATE PersoenlicherAssistent
      SET
        /*  %JoinFKPK(PersoenlicherAssistent,:%New," = ",",") */
        PersoenlicherAssistent.PersonID = :new.PersonID
      WHERE
        /*  %JoinFKPK(PersoenlicherAssistent,:%Old," = "," AND") */
        PersoenlicherAssistent.PersonID = :old.PersonID;
  END IF;

  /* Person hat Kontaktperson Auftraggeber on parent update set null */
  /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Person"
    CHILD_OWNER="", CHILD_TABLE="Auftraggeber"
    P2C_VERB_PHRASE="hat Kontaktperson", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="hat_Kontaktperson", FK_COLUMNS="PersonID" */
  IF
    /* %JoinPKPK(:%Old,:%New," <> "," OR ") */
    Person.PersonID <> Person.PersonID
  THEN
    UPDATE Auftraggeber
      SET
        /* %SetFK(Auftraggeber,NULL) */
        Auftraggeber.PersonID = NULL
      WHERE
        /* %JoinFKPK(Auftraggeber,:%Old," = ",",") */
        Auftraggeber.PersonID = :old.PersonID;
  END IF;

    /* ERwin Builtin Montag, 13. April 2015 10:41:24 */
    /* Adresse R/13 Person on child update set null */
    /* ERWIN_RELATION:CHECKSUM="00000000", PARENT_OWNER="", PARENT_TABLE="Adresse"
    CHILD_OWNER="", CHILD_TABLE="Person"
    P2C_VERB_PHRASE="R/13", C2P_VERB_PHRASE="", 
    FK_CONSTRAINT="R_13", FK_COLUMNS="AdressID" */
    UPDATE Person
      SET
        /* %SetFK(Person,NULL) */
        Person.AdressID = NULL
      WHERE
        NOT EXISTS (
          SELECT * FROM Adresse
            WHERE
              /* %JoinFKPK(:%New,Adresse," = "," AND") */
              :new.AdressID = Adresse.AdressID
        ) 
        /* %JoinPKPK(Person,:%New," = "," AND") */
         and Person.PersonID = Person.PersonID;


-- ERwin Builtin Montag, 13. April 2015 10:41:24
END;
/

