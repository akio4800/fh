select * from (((((Person p inner Join Auftraggeber ag On (p.PersonId=ag.Agid)) Left outer join Adresse adresse on 
                (p.adressId = adresse.Adressid)) left Outer Join Person kontakt on (ag.kontaktperson = kontakt.personid) ) left outer join Adresse
                kontaktad on (kontakt.adressid = kontaktad.adressid))) left outer Join Auftraggeberdaten agDaten on (ag.agid = agDaten.agid)
                where agDaten.agDatenid IN((select max(agDatenid) from Auftraggeberdaten group by agid));

                 Select * from person;
                 Select * from adresse;
                 Delete from person where personid=30;

                 Select * from auftraggeberdaten;

                 select

                 UPDATE auftraggeberdaten SET stundensatzauszahlung=3, 
            Stundensatz=2,BeitragEinkommen=200,FahrtkostenZusatz=1,BetreuungsbedarfH=20, 
            EinkommenMonat=20, PflegegeldStufe=2, 
            FahrtkostenzusatzKM=0.1, agdatenid=nextval('AGDatenIDGen') WHERE agid=3,monat=4,jahr=2015;

            (select max(agDatenid) from Auftraggeberdaten group by agid);

            select * from reha;

            select * from monatsabrechnung;

            select * from dienstverhaeltnis;
            

            select m.paid from persoenlicherassistent pa LEFT OUTER JOIN monatsabrechnung m on(pa.paid=m.paid) where m.monat=2 AND m.jahr=2015 AND agid=2;