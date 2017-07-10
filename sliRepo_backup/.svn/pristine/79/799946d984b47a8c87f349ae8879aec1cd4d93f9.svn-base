using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelvesSoftware;
using SelvesSoftware.DataContainer;
using SelvesSoftware.DB;

namespace SoftwareApplicationTests.DB
{
    /// <summary>
    /// Summary description for MonthlyBillingDAOTests
    /// </summary>
    [TestClass]
    public class MonthlyBillingDAOTests
    {
        private static PurchaserData d;
        private static PersonalAssistant pa;
        private static MonthlyBillingPerPa mb;
        private static MonthlyBillingDAO mbDao;
        private static PurchaserDataDAO pdDao;
        private static PersonalAssistantDAO paDao;


        #region Additional test attributes
        [ClassInitialize()]
        public static void setUp(TestContext testContext)
        {
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
            
            
            mb = new MonthlyBillingPerPa();
            mb.PrivateKm = 21;
            mb.Month = 5;
            mb.Year = 1998;
            mb.Pa = pa;
            mb.Pur = d;
            mb.WorkingHours = 15;
            mb.BillableKm = 10;
        }
        #endregion


        [TestMethod]
        public void InsertMBEntry()
        {
            //given
            pa = paDao.insert(pa);
            d = pdDao.Insert(d);

            //when
            mbDao.InsertMonthlyBilling(mb);

            //then
            mb = mbDao.selectMBEntry(mb);
            Assert.AreEqual(mb.Pur.Purchaser.FirstName, "testpersonDao");
            Assert.AreEqual(mb.Pur.Purchaser.LastName, "testpersonDao");
            Assert.AreEqual(mb.Pur.Purchaser.PhoneNumber, "9999");
            Assert.AreEqual(mb.Pur.Purchaser.MobilePhone, "99999");
            Assert.AreEqual(mb.Pur.Purchaser.EMail, "hans@gibts.net");
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.Street, "Am Hügel");
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.HouseNumber, 12);
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.City, "LINZ");
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.Country, "schland");
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.DoorNumber, 11115);
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.StairNumber, 2345);
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.Etage, 2);
            Assert.AreEqual(mb.Pur.Purchaser.HomeAdress.ZipCode, 5040);
            Assert.AreEqual(mb.Pur.Purchaser.IBAN, "AT1254859888898");
            Assert.AreEqual(mb.Pur.Purchaser.BIC, "SPPR2586XX");
            Assert.AreEqual(mb.Pur.Purchaser.AccountHolder, "Helmut Günther");
            Assert.AreEqual(mb.Pur.Purchaser.SVN, 1225020496);
            Assert.AreEqual(mb.Pur.Purchaser.nationality, "Auenland");
            Assert.AreEqual(mb.Pur.Purchaser.InfoField, "Infotext");

            //purchaser
            Assert.AreEqual(mb.Pur.Purchaser.Active, true);
            Assert.AreEqual(mb.Pur.Purchaser.ApprovalBegin, new DateTime(2015, 05, 02));
            Assert.AreEqual(mb.Pur.Purchaser.ApprovalEnd, new DateTime(2017, 05, 02));
            Assert.AreEqual(mb.Pur.Purchaser.EntryDate, new DateTime(2015, 05, 02));
            Assert.AreEqual(mb.Pur.Purchaser.hasContract, true);
            Assert.AreEqual(mb.Pur.Purchaser.hasIntroCourse, true);
            Assert.AreEqual(mb.Pur.Purchaser.DistrictCommision, "Perg");

            //purchaserData
            Assert.AreEqual(mb.Pur.AssistenceDemand, 12);
            Assert.AreEqual(mb.Pur.CareAllowance, 2);
            Assert.AreEqual(mb.Pur.HourlyRate, 30);
            Assert.AreEqual(mb.Pur.HourlyRatePayoff, 20);
            Assert.AreEqual(mb.Pur.Income, 29);
            Assert.AreEqual(mb.Pur.InputIncome, 100);
            Assert.AreEqual(mb.Pur.Month, 11);
            Assert.AreEqual(mb.Pur.TravellingAllowance, 12);
            Assert.AreEqual(mb.Pur.TravellingAllowanceKM, 11);
            Assert.AreEqual(mb.Pur.CareAllowanceMaximum, 16);
            Assert.AreEqual(mb.Pur.Year, 1998);

            //Person Insert
            Assert.AreEqual(mb.Pa.FirstName, "persöhnlicher");
            Assert.AreEqual(mb.Pa.LastName, "assistent");
            Assert.AreEqual(mb.Pa.PhoneNumber, "9999");
            Assert.AreEqual(mb.Pa.MobilePhone, "99999");
            Assert.AreEqual(mb.Pa.EMail, "hans@gibts.net");
            Assert.AreEqual(mb.Pa.HomeAdress.Street, "Am Hügel");
            Assert.AreEqual(mb.Pa.HomeAdress.HouseNumber, 12);
            Assert.AreEqual(mb.Pa.HomeAdress.City, "LINZ");
            Assert.AreEqual(mb.Pa.HomeAdress.Country, "schland");
            Assert.AreEqual(mb.Pa.HomeAdress.DoorNumber, 11115);
            Assert.AreEqual(mb.Pa.HomeAdress.StairNumber, 2345);
            Assert.AreEqual(mb.Pa.HomeAdress.Etage, 2);
            Assert.AreEqual(mb.Pa.HomeAdress.ZipCode, 5040);
            Assert.AreEqual(mb.Pa.IBAN, "AT1254859888898");
            Assert.AreEqual(mb.Pa.BIC, "SPPR2586XX");
            Assert.AreEqual(mb.Pa.AccountHolder, "Helmut Günther");
            Assert.AreEqual(mb.Pa.SVN, 1225020496);
            Assert.AreEqual(mb.Pa.nationality, "Auenland");
            Assert.AreEqual(mb.Pa.InfoField, "Infotext");

            //pa
            Assert.AreEqual(mb.Pa.Active, true);
            Assert.AreEqual(mb.Pa.ClosingDateDocuments, new DateTime(2016, 02, 03));
            Assert.AreEqual(mb.Pa.SV, true);
            Assert.AreEqual(mb.Pa.Dienstvertrag, true);
            Assert.AreEqual(mb.Pa.BestBH, true);
            Assert.AreEqual(mb.Pa.Grundkurs, true);
            Assert.AreEqual(mb.Pa.consumedHours, 2.4M);
            Assert.AreEqual(mb.Pa.deadLineHours, new DateTime(2017, 02, 04));

            Assert.AreEqual(mb.PrivateKm, 21);
            Assert.AreEqual(mb.Month, 5);
            Assert.AreEqual(mb.Year, 1998);
            Assert.AreEqual(mb.Pa, pa);
            Assert.AreEqual(mb.Pur, d);
            Assert.AreEqual(mb.WorkingHours, 15);
            Assert.AreEqual(mb.BillableKm, 10);

        }

        [TestMethod]
        public void UpdateMBEntry()
        {
            //given
            pa = paDao.insert(pa);
            d = pdDao.Insert(d);
            mbDao.InsertMonthlyBilling(mb);

            //when
            mb.BillableKm = 12;
            mb.PrivateKm = 15;
            mb.WorkingHours = 14;
            mbDao.UpdateMonthlyBillingEntry(mb);

            //then
            mb = mbDao.selectMBEntry(mb);
            Assert.AreEqual(mb.BillableKm, 12);
            Assert.AreEqual(mb.PrivateKm, 15);
            Assert.AreEqual(mb.WorkingHours, 14);
        }

   

        [TestCleanup()]
        public void tearDown()
        {
            
            mbDao.DeleteMonthlyBillingEntry(mb.Pur.Purchaser.Id, mb.Pa.Id, mb.Month, mb.Year);

            pdDao.DeletePurchaserDataRecursive(mb.Pur.Purchaser.Id);

            paDao.DeletePersonalAssistantRecursive(mb.Pa.Id);
        
        }
    }
}
