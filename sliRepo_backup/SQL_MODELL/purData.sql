Drop Table TestAuftraggeberdaten;
DROP SEQUENCE AGDatenIDGen;
CREATE SEQUENCE AGDatenIDGen INCREMENT BY 1
MINVALUE 1 MAXVALUE 99999
START WITH 1 CYCLE;
CREATE TABLE TestAuftraggeberdaten
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
	AgDatenId	  INTEGER
);

--AG1Daten - Example
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,02,2015,999,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,02,2015,998,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,03,2015,999,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,03,2015,998,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,04,2015,999,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,04,2015,998,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,05,2015,999,0.50,30,2,1400,nextval('AGDatenIdGen'));
INSERT INTO TestAuftraggeberdaten VALUES (0.30,8.50,8.50,200,05,2015,998,0.50,30,2,1400,nextval('AGDatenIdGen'));

Select * from TestAuftraggeberdaten ;



select max(agDatenid) from Auftraggeberdaten group by agid;

select * from dienstverhaeltnis dienst inner join person p on (dienst.paid=p.personid) where dienst.agid=999;
select * from auftraggeberdaten;
select * from auftraggeber;


select * from ((((Person p Inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Inner join Adresse adresse on (adresse.Adressid=p.adressId)) left Outer Join Person kontakt on (kontakt.personid=ag.kontaktperson) ) left outer join Adresse kontaktad on (kontakt.adressid=kontaktad.adressid)) Inner Join TestAuftraggeberdaten agDaten on ( agDaten.agid=ag.agid) where agDaten.agDatenid IN ((select max(agDatenid) from Auftraggeberdaten group by agid));

select * from (((((Person p left outer Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left outer join Adresse adresse on 
                (p.adressId=adresse.Adressid)) left Outer Join Person kontakt on (ag.kontaktperson=kontakt.personid) ) left outer join Adresse 
                kontaktad on (kontakt.adressid=kontaktad.adressid))) left outer Join Auftraggeberdaten agDaten on (ag.agid=agDaten.agid) 
                where agDaten.agDatenid IN ((select max(agDatenid) from Auftraggeberdaten group by agid));

select * from ((Person p left outer Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left outer join Adresse adresse on 
                (p.adressId=adresse.Adressid));

select * from (((((Person p inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left outer join Adresse adresse on 
                (p.adressId=adresse.Adressid)) left Outer Join Person kontakt on (ag.kontaktperson=kontakt.personid) ) left outer join Adresse 
                kontaktad on (kontakt.adressid=kontaktad.adressid))) left outer Join Auftraggeberdaten agDaten on (ag.agid=agDaten.agid) 
                where ag.agid=998 and agDaten.agDatenid IN ((select max(agDatenid) from Auftraggeberdaten group by agid));

                