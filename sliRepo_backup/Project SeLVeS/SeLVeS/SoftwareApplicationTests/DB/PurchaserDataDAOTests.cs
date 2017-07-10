using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelvesSoftware.DataContainer;
using System;
using System.Collections.Generic;


namespace SelvesSoftware.DB.Tests
{
    
    /// <summary>
    /// Test for PurchaserDataDao
    /// 
    /// </summary>
    [TestClass()]
    public class PurchaserDataDAOTests
    {
        private static PurchaserData d;
        private long idToDelete;

        [ClassInitialize()]
        public static void setUp(TestContext testContext)
        {
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
            d.Year = 2015;
        }
        /// <summary>
        /// Tests Insert PurchaserDataDAO WITHOUT ContactPerson and Employees
        /// </summary>
        [TestMethod()]
        public void InsertAndSelectTestPurchaserData()
        {
           //given (see setUp)
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PurchaserData originalPur = d;
            PurchaserData pd = new PurchaserData();
            
            //when
            long id = pdDao.Insert(originalPur).Purchaser.Id;
            idToDelete = id;
            pd.Purchaser.Id = id;
            pd =pdDao.Select(pd);

            //then
            Assert.AreEqual(pd.Purchaser.FirstName, "testpersonDao");
            Assert.AreEqual(pd.Purchaser.LastName, "testpersonDao");
            Assert.AreEqual(pd.Purchaser.PhoneNumber, "9999");
            Assert.AreEqual(pd.Purchaser.MobilePhone, "99999");
            Assert.AreEqual(pd.Purchaser.EMail, "hans@gibts.net");
            Assert.AreEqual(pd.Purchaser.HomeAdress.Street, "Am Hügel");
            Assert.AreEqual(pd.Purchaser.HomeAdress.HouseNumber, 12);
            Assert.AreEqual(pd.Purchaser.HomeAdress.City, "LINZ");
            Assert.AreEqual(pd.Purchaser.HomeAdress.Country, "schland");
            Assert.AreEqual(pd.Purchaser.HomeAdress.DoorNumber, 11115);
            Assert.AreEqual(pd.Purchaser.HomeAdress.StairNumber, 2345);
            Assert.AreEqual(pd.Purchaser.HomeAdress.Etage, 2);
            Assert.AreEqual(pd.Purchaser.HomeAdress.ZipCode, 5040);
            Assert.AreEqual(pd.Purchaser.IBAN, "AT1254859888898");
            Assert.AreEqual(pd.Purchaser.BIC, "SPPR2586XX");
            Assert.AreEqual(pd.Purchaser.AccountHolder, "Helmut Günther");
            Assert.AreEqual(pd.Purchaser.SVN, 1225020496);
            Assert.AreEqual(pd.Purchaser.nationality, "Auenland");
            Assert.AreEqual(pd.Purchaser.InfoField, "Infotext");

            //purchaser
            Assert.AreEqual(pd.Purchaser.Active, true);
            Assert.AreEqual(pd.Purchaser.ApprovalBegin, new DateTime(2015, 05, 02));
            Assert.AreEqual(pd.Purchaser.ApprovalEnd, new DateTime(2017, 05, 02));
            Assert.AreEqual(pd.Purchaser.EntryDate, new DateTime(2015, 05, 02));
            Assert.AreEqual(pd.Purchaser.hasContract, true);
            Assert.AreEqual(pd.Purchaser.hasIntroCourse, true);
            Assert.AreEqual(pd.Purchaser.DistrictCommision, "Perg");

            //purchaserData
            Assert.AreEqual(pd.AssistenceDemand, 12);
            Assert.AreEqual(pd.CareAllowance, 2);
            Assert.AreEqual(pd.HourlyRate, 30);
            Assert.AreEqual(pd.HourlyRatePayoff, 20);
            Assert.AreEqual(pd.Income, 29);
            Assert.AreEqual(pd.InputIncome, 100);
            Assert.AreEqual(pd.Month, 11);
            Assert.AreEqual(pd.TravellingAllowance, 12);
            Assert.AreEqual(pd.TravellingAllowanceKM, 11);
            //Assert.AreEqual(pd.CareAllowanceMaximum, 16);
            Assert.AreEqual(pd.Year, 2015);

        }

        [TestMethod()]
        public void SelectAllTestPurchaserData()
        {
            //given (see setUp)
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PurchaserData originalPur = d;
            long id = pdDao.Insert(originalPur).Purchaser.Id;
            idToDelete = id;
            //when
            List<PurchaserData> pds = pdDao.SelectAll();

            //then
            foreach (PurchaserData pd in pds)
            {
                if (pd.Purchaser.Id == id)
                {

                    Assert.AreEqual(pd.Purchaser.FirstName, "testpersonDao");
                    Assert.AreEqual(pd.Purchaser.LastName, "testpersonDao");
                    Assert.AreEqual(pd.Purchaser.PhoneNumber, "9999");
                    Assert.AreEqual(pd.Purchaser.MobilePhone, "99999");
                    Assert.AreEqual(pd.Purchaser.EMail, "hans@gibts.net");
                    Assert.AreEqual(pd.Purchaser.HomeAdress.Street, "Am Hügel");
                    Assert.AreEqual(pd.Purchaser.HomeAdress.HouseNumber, 12);
                    Assert.AreEqual(pd.Purchaser.HomeAdress.City, "LINZ");
                    Assert.AreEqual(pd.Purchaser.HomeAdress.Country, "schland");
                    Assert.AreEqual(pd.Purchaser.HomeAdress.DoorNumber, 11115);
                    Assert.AreEqual(pd.Purchaser.HomeAdress.StairNumber, 2345);
                    Assert.AreEqual(pd.Purchaser.HomeAdress.Etage, 2);
                    Assert.AreEqual(pd.Purchaser.HomeAdress.ZipCode, 5040);
                    Assert.AreEqual(pd.Purchaser.IBAN, "AT1254859888898");
                    Assert.AreEqual(pd.Purchaser.BIC, "SPPR2586XX");
                    Assert.AreEqual(pd.Purchaser.AccountHolder, "Helmut Günther");
                    Assert.AreEqual(pd.Purchaser.SVN, 1225020496);
                    Assert.AreEqual(pd.Purchaser.nationality, "Auenland");
                    Assert.AreEqual(pd.Purchaser.InfoField, "Infotext");

                    //purchaser
                    Assert.AreEqual(pd.Purchaser.Active, true);
                    Assert.AreEqual(pd.Purchaser.ApprovalBegin, new DateTime(2015, 05, 02));
                    Assert.AreEqual(pd.Purchaser.ApprovalEnd, new DateTime(2017, 05, 02));
                    Assert.AreEqual(pd.Purchaser.EntryDate, new DateTime(2015, 05, 02));
                    Assert.AreEqual(pd.Purchaser.hasContract, true);
                    Assert.AreEqual(pd.Purchaser.hasIntroCourse, true);
                    Assert.AreEqual(pd.Purchaser.DistrictCommision, "Perg");

                    //purchaserData
                    Assert.AreEqual(pd.AssistenceDemand, 12);
                    Assert.AreEqual(pd.CareAllowance, 2);
                    Assert.AreEqual(pd.HourlyRate, 30);
                    Assert.AreEqual(pd.HourlyRatePayoff, 20);
                    Assert.AreEqual(pd.Income, 29);
                    Assert.AreEqual(pd.InputIncome, 100);
                    Assert.AreEqual(pd.Month, 11);
                    Assert.AreEqual(pd.TravellingAllowance, 12);
                    Assert.AreEqual(pd.TravellingAllowanceKM, 11);
                    //Assert.AreEqual(pd.CareAllowanceMaximum, 16);
                    Assert.AreEqual(pd.Year, 2015);
                }
            }

        }
        [TestMethod()]
        public void employementTimesTest()
        {
            PersonalAssistantDAO paDao = new PersonalAssistantDAO();
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PurchaserData originalPd = d;
            originalPd=pdDao.Insert(originalPd);
            idToDelete = originalPd.Purchaser.Id;
            List<PersonalAssistant> pas = paDao.SelectAll();
            if (pas == null || pas.Count == 0)
            {
                Assert.Fail("Can not find personal assistants to add to purchasers employmentTimes");
            }
            PersonalAssistant pa = pas[0];
            Purchaser p = originalPd.Purchaser;
            paDao.insertEmploymentStatus(p, pa);

            //when
            PurchaserDAO purDao = new PurchaserDAO();
            List<EmploymentStatus> empList = purDao.SelectEmploymentStatusList(p);

            //then
            Assert.IsNotNull(empList);
            Assert.AreEqual(empList.Count, 1);
            Assert.AreEqual(empList[0].Purchaser.Id, p.Id);
            Assert.AreEqual(empList[0].Assistant.Id, pa.Id);
        }

        [TestMethod()]
        public void UpdatePurchaserDataTest()
        {
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PurchaserData originalPur = d;
            PurchaserData pd = new PurchaserData();

            PurchaserData p = pdDao.Insert(originalPur);
            idToDelete = p.Purchaser.Id;

            //Person Insert
            p.Purchaser.FirstName = "Hermann";
            p.Purchaser.LastName = "Mayr";
            p.Purchaser.PhoneNumber = "1111";
            p.Purchaser.MobilePhone = "1111";
            p.Purchaser.EMail = "hans@gibts.ja";
            p.Purchaser.HomeAdress.Street = "Am Tal";
            p.Purchaser.HomeAdress.HouseNumber = 2;
            p.Purchaser.HomeAdress.City = "LINZKeks";
            p.Purchaser.HomeAdress.Country = "schiachland";
            p.Purchaser.HomeAdress.DoorNumber = 2222;
            p.Purchaser.HomeAdress.StairNumber = 1;
            p.Purchaser.HomeAdress.Etage = 1;
            p.Purchaser.HomeAdress.ZipCode = 54;
            p.Purchaser.IBAN = "DE2554859888898";
            p.Purchaser.BIC = "RAPR2586XX";
            p.Purchaser.AccountHolder = "Herman Günther";
            p.Purchaser.SVN = 3525020496;
            p.Purchaser.nationality = "BAuenland";
            p.Purchaser.InfoField = "Infotext erweitert";

            //purchaser
            p.Purchaser.Active = false;
            p.Purchaser.ApprovalBegin = new DateTime(2016, 06, 08);
            p.Purchaser.ApprovalEnd = new DateTime(2018, 08, 09);
            p.Purchaser.EntryDate = new DateTime(2018, 04, 07);
            p.Purchaser.hasContract = false;
            p.Purchaser.hasIntroCourse = false;
            p.Purchaser.DistrictCommision = "Freistadt";

            //purchaserData
            p.AssistenceDemand = 225;
            p.CareAllowance = 84;
            p.HourlyRate = 26;
            p.HourlyRatePayoff = 12;
            p.Income = 2900;
            p.InputIncome = 1000;
            p.Month = 11;
            p.TravellingAllowance = 13;
            p.TravellingAllowanceKM = 25;
            p.CareAllowanceMaximum = 18;
            p.Year = 2015;

            pdDao.Update(p);
            p = new PurchaserData();
            p.Purchaser = new Purchaser();
            p.Purchaser.Id = idToDelete;
            pdDao.Select(p);


            //Person Insert
            Assert.AreEqual(p.Purchaser.FirstName, "Hermann");
            Assert.AreEqual(p.Purchaser.LastName, "Mayr");
            Assert.AreEqual(p.Purchaser.PhoneNumber, "1111");
            Assert.AreEqual(p.Purchaser.MobilePhone, "1111");
            Assert.AreEqual(p.Purchaser.EMail, "hans@gibts.ja");
            Assert.AreEqual(p.Purchaser.HomeAdress.Street, "Am Tal");
            Assert.AreEqual(p.Purchaser.HomeAdress.HouseNumber, 2);
            Assert.AreEqual(p.Purchaser.HomeAdress.City, "LINZKeks");
            Assert.AreEqual(p.Purchaser.HomeAdress.Country, "schiachland");
            Assert.AreEqual(p.Purchaser.HomeAdress.DoorNumber, 2222);
            Assert.AreEqual(p.Purchaser.HomeAdress.StairNumber, 1);
            Assert.AreEqual(p.Purchaser.HomeAdress.Etage, 1);
            Assert.AreEqual(p.Purchaser.HomeAdress.ZipCode, 54);
            Assert.AreEqual(p.Purchaser.IBAN, "DE2554859888898");
            Assert.AreEqual(p.Purchaser.BIC, "RAPR2586XX");
            Assert.AreEqual(p.Purchaser.AccountHolder, "Herman Günther");
            Assert.AreEqual(p.Purchaser.SVN, 3525020496);
            Assert.AreEqual(p.Purchaser.nationality, "BAuenland");
            Assert.AreEqual(p.Purchaser.InfoField, "Infotext erweitert");

            //purchaser
            Assert.AreEqual(p.Purchaser.Active, false);
            Assert.AreEqual(p.Purchaser.ApprovalBegin, new DateTime(2016, 06, 08));
            Assert.AreEqual(p.Purchaser.ApprovalEnd, new DateTime(2018, 08, 09));
            Assert.AreEqual(p.Purchaser.EntryDate, new DateTime(2018, 04, 07));
            Assert.AreEqual(p.Purchaser.hasContract, false);
            Assert.AreEqual(p.Purchaser.hasIntroCourse, false);
            Assert.AreEqual(p.Purchaser.DistrictCommision, "Freistadt");

            //purchaserData
            Assert.AreEqual(p.AssistenceDemand, 225);
            Assert.AreEqual(p.CareAllowance, 84);
            Assert.AreEqual(p.HourlyRate, 26);
            Assert.AreEqual(p.HourlyRatePayoff, 12);
            Assert.AreEqual(p.Income, 2900);
            Assert.AreEqual(p.InputIncome, 1000);
            Assert.AreEqual(p.Month, 11);
            Assert.AreEqual(p.TravellingAllowance, 13);
            Assert.AreEqual(p.TravellingAllowanceKM, 25);
            Assert.AreEqual(p.Year, 2015);


        }

        [TestCleanup()]
        public void tearDown()
        {

            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            pdDao.DeletePurchaserDataRecursive(idToDelete);
        }
    }
}