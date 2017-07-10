Insert into Konten (KontoNr, Name) Values (4000,'Leistungsentgelt Land OÖ (Sozialabteilung)');
Insert into Konten (KontoNr, Name) Values (4001,'Leistungsentgelt Land OÖ (andere Abteilungen)');
Insert into Konten (KontoNr, Name) Values (4002,'Fahrtkostenersätze für Fahrdienste');
Insert into Konten (KontoNr, Name) Values (4010,'Leistungsentgelt Bund / BSB');
Insert into Konten (KontoNr, Name) Values (4020,'Leistungsentgelt andere Bundesländer');
Insert into Konten (KontoNr, Name) Values (4030,'Leistungsentgelt Sozialhilfeverband / Magistrat');
Insert into Konten (KontoNr, Name) Values (4040,'Leistungsentgelt Europäische Union');
Insert into Konten (KontoNr, Name) Values (4050,'Leistungsentgelt Arbeitsmarktservice');
Insert into Konten (KontoNr, Name) Values (4060,'Leistungsentgelt von Krankenkassen / Sozialversicherungsträgern');
Insert into Konten (KontoNr, Name) Values (4090,'Leistungsentgelt von Privaten ');
Insert into Konten (KontoNr, Name) Values (4221,'Zuschüsse für Personalkosten');
Insert into Konten (KontoNr, Name) Values (4222,'Zuschüsse für Sachkosten');
Insert into Konten (KontoNr, Name) Values (4300,'Verkaufserlöse Werkstätten');
Insert into Konten (KontoNr, Name) Values (4600,'Sonstige betriebliche Erlöse');
Insert into Konten (KontoNr, Name) Values (4900,'Interne Leistungsverrechnung');

Insert into Kostenstelle (KostenstellenID, Name) Values (1,'Persoenliche Assistenz');
Insert into Kostenstelle (KostenstellenID, Name) Values (2,'HiKo Kueche');

Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4000,1234,2014,'Plan');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4001,5000,2014,'Plan');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4002,13000,2014,'Plan');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4000,1234,2014,'Plan');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4001,5000,2014,'Plan');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4002,13000,2014,'Plan');

Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4000,1234,2014,'Ist');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4001,5000,2014,'Ist');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (1,4002,13000,2014,'Ist');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4000,1234,2014,'Ist');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4001,5000,2014,'Ist');
Insert into Erfolgsrechnung (KostenstellenID, KontoNr, Wert, Jahr, Art) Values (2,4002,13000,2014,'Ist');