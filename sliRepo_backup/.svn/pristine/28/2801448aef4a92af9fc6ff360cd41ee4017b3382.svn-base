using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelvesSoftware.DB;
using SelvesSoftware;
using System.Collections.Generic;
using SelvesSoftware.DataContainer;

namespace SoftwareApplicationTests.DB
{
    [TestClass]
    public class EffortEntryDAOTests
    {
        private static EffortEntryDAO eeDao;
        private static List<EffortEntry> effortEntries;
        private static PurchaserData d;
        private static PersonalAssistant pa;
        private static MonthlyBillingPerPa mb;
        private static MonthlyBillingDAO mbDao;
        private static PurchaserDataDAO pdDao;
        private static PersonalAssistantDAO paDao;

        [ClassInitialize()]
        public static void setUp(TestContext testContext)
        {
            eeDao = new EffortEntryDAO();
            mbDao = new MonthlyBillingDAO();
            pdDao = new PurchaserDataDAO();
            paDao = new PersonalAssistantDAO();
            Purchaser p = new Purchaser();
            //Person Insert
            p.FirstName = "testpersonDao";
            p.LastName = "testpersonDao";
            p.PhoneNumber = "9999";
            p.MobilePhone = "99999";
            p.EMail = "hans@gibts.net";
            p.HomeAdress.Street = "Am Hügel";
            p.HomeAdress.HouseNumber = 12;
            p.HomeAdress.City = "LINZ";
            p.HomeAdress.Country = "schland";
            p.HomeAdress.DoorNumber = 11115;
            p.HomeAdress.StairNumber = 2345;
            p.HomeAdress.Etage = 2;
            p.HomeAdress.ZipCode = 5040;
            p.IBAN = "AT1254859888898";
            p.BIC = "SPPR2586XX";
            p.AccountHolder = "Helmut Günther";
            p.SVN = 1225020496;
            p.nationality = "Auenland";
            p.InfoField = "Infotext";

            //purchaser
            p.Active = true;
            p.ApprovalBegin = new DateTime(2015, 05, 02);
            p.ApprovalEnd = new DateTime(2017, 05, 02);
            p.EntryDate = new DateTime(2015, 05, 02);
            p.hasContract = true;
            p.hasIntroCourse = true;
            p.DistrictCommision = "Perg";

            //purchaserData
            d = new PurchaserData();
            d.Purchaser = p;
            d.AssistenceDemand = 12;
            d.CareAllowance = 2;
            d.HourlyRate = 30;
            d.HourlyRatePayoff = 20;
            d.Income = 29;
            d.InputIncome = 100;
            d.Month = 11;
            d.TravellingAllowance = 12;
            d.TravellingAllowanceKM = 11;
            d.CareAllowanceMaximum = 16;
            d.Year = 1998;

            pa = new PersonalAssistant();
            //Person Insert
            pa.FirstName = "persöhnlicher";
            pa.LastName = "assistent";
            pa.PhoneNumber = "9999";
            pa.MobilePhone = "99999";
            pa.EMail = "hans@gibts.net";
            pa.HomeAdress.Street = "Am Hügel";
            pa.HomeAdress.HouseNumber = 12;
            pa.HomeAdress.City = "LINZ";
            pa.HomeAdress.Country = "schland";
            pa.HomeAdress.DoorNumber = 11115;
            pa.HomeAdress.StairNumber = 2345;
            pa.HomeAdress.Etage = 2;
            pa.HomeAdress.ZipCode = 5040;
            pa.IBAN = "AT1254859888898";
            pa.BIC = "SPPR2586XX";
            pa.AccountHolder = "Helmut Günther";
            pa.SVN = 1225020496;
            pa.nationality = "Auenland";
            pa.InfoField = "Infotext";

            //pa
            pa.Active = true;
            pa.ClosingDateDocuments = new DateTime(2016, 02, 03);
            pa.SV = true;
            pa.Dienstvertrag = true;
            pa.BestBH = true;
            pa.Grundkurs = true;
            pa.consumedHours = 2.4M;
            pa.deadLineHours = new DateTime(2017, 02, 04);

            pa = paDao.insert(pa);
            d = pdDao.Insert(d);
            mb = new MonthlyBillingPerPa();
            mb.PrivateKm = 21;
            mb.Month = 7;
            mb.Year = 1998;
            mb.Pa = pa;
            mb.Pur = d;
            mb.WorkingHours = 15;
            mb.BillableKm = 10;


            effortEntries = new List<EffortEntry>();
            EffortEntry a = new EffortEntry();
            a.Month = mb.Month;
            a.Year = mb.Year;
            a.Pa = mb.Pa;
            a.Purchaser = mb.Pur.Purchaser;
            a.Day = 1;
            a.From = new DateTime(a.Year, a.Month, a.Day, 12, 30, 0);
            a.To = new DateTime(a.Year, a.Month, a.Day, 18, 45, 0);
            a.Km = 10;
            a.A1 = new Activity(Activity.State.Begleitung);
            a.A2 = new Activity(Activity.State.Hauswirtschaft);
            EffortEntry b = new EffortEntry();
            b.Month = mb.Month;
            b.Year = mb.Year;
            b.Pa = mb.Pa;
            b.Purchaser = mb.Pur.Purchaser;
            b.Day = 2;
            b.From = new DateTime(b.Year, b.Month, b.Day, 12, 30, 0);
            b.To = new DateTime(b.Year, b.Month, b.Day, 18, 45, 0);
            b.Km = 10;
            b.A1 = new Activity(Activity.State.Begleitung);
            b.A2 = new Activity(Activity.State.Hauswirtschaft);

            effortEntries.Add(a);
            effortEntries.Add(b);


        }

        [TestMethod]
        public void InsertEffortEntry()
        {
            //given
            pa = paDao.insert(pa);
            d = pdDao.Insert(d);
            mbDao.InsertMonthlyBilling(mb);

            //when
            eeDao.InsertEntry(effortEntries[0]);
            eeDao.InsertEntry(effortEntries[1]);

            //then
            List<EffortEntry> ees = eeDao.GetEntries(mb.Pa.Id, mb.Pur.Purchaser.Id, mb.Month, mb.Year);
            Assert.AreEqual(ees.Count, 2);

            Assert.AreEqual(ees[0].A1.Name, Activity.State.Begleitung);
            Assert.AreEqual(ees[0].A2.Name, Activity.State.Hauswirtschaft);
            Assert.AreEqual(ees[0].Km, 10);
            Assert.AreEqual(ees[0].From.Hour, 12);
            Assert.AreEqual(ees[0].From.Minute, 30);
            Assert.AreEqual(ees[0].To.Hour, 18);
            Assert.AreEqual(ees[0].To.Minute, 45);

        }

        [TestMethod]
        public void UpdateEffortEntry()
        {
            //given
            pa = paDao.insert(pa);
            d = pdDao.Insert(d);
            mbDao.InsertMonthlyBilling(mb);
            eeDao.InsertEntry(effortEntries[0]);
            effortEntries[0].A1.Name = Activity.State.Grundversorgung;
            effortEntries[0].A2.Name = Activity.State.Kommunikation;
            effortEntries[0].From = new DateTime(mb.Year, mb.Month, effortEntries[0].Day, 13, 31, 0);
            effortEntries[0].To = new DateTime(mb.Year, mb.Month, effortEntries[0].Day, 20, 47, 0);
            effortEntries[0].Km = 0;



            //when
            eeDao.UpdateEntry(effortEntries[0]);
            //then
            List<EffortEntry> ees = eeDao.GetEntries(mb.Pa.Id, mb.Pur.Purchaser.Id, mb.Month, mb.Year);
            Assert.AreEqual(ees.Count, 1);
            if (ees[0].Day == 1)
            {
                Assert.AreEqual(ees[0].A1.Name, Activity.State.Grundversorgung);
                Assert.AreEqual(ees[0].A2.Name, Activity.State.Kommunikation);
                Assert.AreEqual(ees[0].Km, 0);
                Assert.AreEqual(ees[0].From.Hour, 13);
                Assert.AreEqual(ees[0].From.Minute, 31);
                Assert.AreEqual(ees[0].To.Hour, 20);
                Assert.AreEqual(ees[0].To.Minute, 47);
            } else
            {
                Assert.AreEqual(ees[1].A1.Name, Activity.State.Grundversorgung);
                Assert.AreEqual(ees[1].A2.Name, Activity.State.Kommunikation);
                Assert.AreEqual(ees[1].Km, 0);
                Assert.AreEqual(ees[1].From.Hour, 13);
                Assert.AreEqual(ees[1].From.Minute, 31);
                Assert.AreEqual(ees[1].To.Hour, 20);
                Assert.AreEqual(ees[1].To.Minute, 47);
            }

        }

        [TestCleanup()]
        public void tearDown()
        {
            eeDao.DeleteEffortEntry(effortEntries[0].Purchaser.Id, effortEntries[0].Pa.Id, effortEntries[0].Day, effortEntries[0].Month, effortEntries[0].Year);
            eeDao.DeleteEffortEntry(effortEntries[1].Purchaser.Id, effortEntries[1].Pa.Id, effortEntries[1].Day, effortEntries[1].Month, effortEntries[1].Year);

            mbDao.DeleteMonthlyBillingEntry(mb.Pur.Purchaser.Id, mb.Pa.Id, mb.Month, mb.Year);

            pdDao.DeletePurchaserDataRecursive(mb.Pur.Purchaser.Id);

            paDao.DeletePersonalAssistantRecursive(mb.Pa.Id);

        }

    }
}
