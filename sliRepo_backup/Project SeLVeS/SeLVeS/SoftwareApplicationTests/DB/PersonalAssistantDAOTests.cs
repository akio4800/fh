using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelvesSoftware.DataContainer;
using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.DB.Tests
{
    [TestClass()]
    public class PersonalAssistantDAOTests
    {
        private static PersonalAssistant pa;
        private long idToDelete;

       [ClassInitialize()]
       public static void setUp(TestContext testContext)
        {
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

        }
        //test insert PersonalAssistant WITHOUT EmploymentTimes, Documents, Purchaser
        [TestMethod()]
        public void insertAndSelectTestPersonalAssistant()
        {
            //given (see setUp)
            PersonalAssistant p = pa;
            PersonalAssistantDAO pDao = new PersonalAssistantDAO();
            
            //when
            p=pDao.insert(p);
            idToDelete = p.Id;
            p = pDao.select(p);

            //then
            Assert.AreEqual(p.FirstName, "persöhnlicher");
            Assert.AreEqual(p.LastName, "assistent");
            Assert.AreEqual(p.PhoneNumber, "9999");
            Assert.AreEqual(p.MobilePhone, "99999");
            Assert.AreEqual(p.EMail, "hans@gibts.net");
            Assert.AreEqual(p.HomeAdress.Street, "Am Hügel");
            Assert.AreEqual(p.HomeAdress.HouseNumber, 12);
            Assert.AreEqual(p.HomeAdress.City, "LINZ");
            Assert.AreEqual(p.HomeAdress.Country, "schland");
            Assert.AreEqual(p.HomeAdress.DoorNumber, 11115);
            Assert.AreEqual(p.HomeAdress.StairNumber, 2345);
            Assert.AreEqual(p.HomeAdress.Etage, 2);
            Assert.AreEqual(p.HomeAdress.ZipCode, 5040);
            Assert.AreEqual(p.IBAN, "AT1254859888898");
            Assert.AreEqual(p.BIC, "SPPR2586XX");
            Assert.AreEqual(p.AccountHolder, "Helmut Günther");
            Assert.AreEqual(p.SVN, 1225020496);
            Assert.AreEqual(p.nationality, "Auenland");
            Assert.AreEqual(p.InfoField, "Infotext");

            //pa
            Assert.AreEqual(p.Active, true);
            Assert.AreEqual(p.ClosingDateDocuments, new DateTime(2016, 02, 03));
            Assert.AreEqual(p.SV, true);
            Assert.AreEqual(p.Dienstvertrag, true);
            Assert.AreEqual(p.BestBH, true);
            Assert.AreEqual(p.Grundkurs, true);
            Assert.AreEqual(p.consumedHours, 2.4M);
            Assert.AreEqual(p.deadLineHours, new DateTime(2017, 02, 04));


        }

        [TestMethod()]
        public void SelectAllTestPersonalAss()
        {
            //given (see setUp)
            PersonalAssistant p = pa;
            PersonalAssistantDAO pDao = new PersonalAssistantDAO();

            //when
            p = pDao.insert(p);
            idToDelete = p.Id;
            List<PersonalAssistant> pas = pDao.SelectAll();

            //then
            foreach(PersonalAssistant pa in pas) {
                if (pa.Id == p.Id)
                {
                    //person
                    Assert.AreEqual(pa.FirstName, "persöhnlicher");
                    Assert.AreEqual(pa.LastName, "assistent");
                    Assert.AreEqual(pa.PhoneNumber, "9999");
                    Assert.AreEqual(pa.MobilePhone, "99999");
                    Assert.AreEqual(pa.EMail, "hans@gibts.net");
                    Assert.AreEqual(pa.HomeAdress.Street, "Am Hügel");
                    Assert.AreEqual(pa.HomeAdress.HouseNumber, 12);
                    Assert.AreEqual(pa.HomeAdress.City, "LINZ");
                    Assert.AreEqual(pa.HomeAdress.Country, "schland");
                    Assert.AreEqual(pa.HomeAdress.DoorNumber, 11115);
                    Assert.AreEqual(pa.HomeAdress.StairNumber, 2345);
                    Assert.AreEqual(pa.HomeAdress.Etage, 2);
                    Assert.AreEqual(pa.HomeAdress.ZipCode, 5040);
                    Assert.AreEqual(pa.IBAN, "AT1254859888898");
                    Assert.AreEqual(pa.BIC, "SPPR2586XX");
                    Assert.AreEqual(pa.AccountHolder, "Helmut Günther");
                    Assert.AreEqual(pa.SVN, 1225020496);
                    Assert.AreEqual(pa.nationality, "Auenland");
                    Assert.AreEqual(pa.InfoField, "Infotext");

                    //pa
                    Assert.AreEqual(pa.Active, true);
                    Assert.AreEqual(pa.ClosingDateDocuments, new DateTime(2016, 02, 03));
                    Assert.AreEqual(pa.SV, true);
                    Assert.AreEqual(pa.Dienstvertrag, true);
                    Assert.AreEqual(pa.BestBH, true);
                    Assert.AreEqual(pa.Grundkurs, true);
                    Assert.AreEqual(pa.consumedHours, 2.4M);
                    Assert.AreEqual(pa.deadLineHours, new DateTime(2017, 02, 04));
                }

            }
        }
        [TestMethod()]
        public void employingPurchasersTest()
        {
            PersonalAssistantDAO paDao = new PersonalAssistantDAO();
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PersonalAssistant originalPa = pa;
            originalPa= paDao.insert(originalPa);
            idToDelete = originalPa.Id;
            List<PurchaserData> purs = pdDao.SelectAll();
            if(purs==null || purs.Count == 0)
            {
                Assert.Fail("Can not add purchaser to personal assistants purchasersList");
            }
            Purchaser p = purs[0].Purchaser;
            paDao.insertEmploymentStatus(p, originalPa);

            //when
            paDao.selectPurchaserList(originalPa);

            //then
            Assert.IsNotNull(originalPa.Purchasers);
            Assert.AreEqual(originalPa.Purchasers.Count, 1);
            Assert.AreEqual(originalPa.Purchasers[0].Id, p.Id);
        }

        [TestMethod()]
        public void UpdatePaTest()
        {
            PersonalAssistantDAO paDao = new PersonalAssistantDAO();
            PurchaserDataDAO pdDao = new PurchaserDataDAO();
            PersonalAssistant originalPa = pa;
            originalPa = paDao.insert(originalPa);
            idToDelete = originalPa.Id;
            PersonalAssistant updatePa = new PersonalAssistant();
            //Person Insert
            updatePa.FirstName = "update";
            updatePa.LastName = "update";
            updatePa.PhoneNumber = "1111";
            updatePa.MobilePhone = "52451111";
            updatePa.EMail = "hans@gibts.ja";
            updatePa.HomeAdress.Street = "Am Tal";
            updatePa.HomeAdress.HouseNumber = 14;
            updatePa.HomeAdress.City = "LINZKeks";
            updatePa.HomeAdress.Country = "schiland";
            updatePa.HomeAdress.DoorNumber = 111515;
            updatePa.HomeAdress.StairNumber = 45;
            updatePa.HomeAdress.Etage = 0;
            updatePa.HomeAdress.ZipCode = 50040;
            updatePa.IBAN = "AT1554859888898";
            updatePa.BIC = "SPPR1586XX";
            updatePa.AccountHolder = "Herman Günther";
            updatePa.SVN = 2225025496;
            updatePa.nationality = "Bauernland";
            updatePa.InfoField = "Infotext erweitert";

            //pa
            updatePa.Active = false;
            updatePa.ClosingDateDocuments = new DateTime(2016, 04, 05);
            updatePa.SV = false;
            updatePa.Dienstvertrag = false;
            updatePa.BestBH = false;
            updatePa.Grundkurs = false;
            updatePa.consumedHours = 2.1M;
            updatePa.deadLineHours = new DateTime(2016, 01, 09);

            updatePa=paDao.update(updatePa);

            Assert.AreEqual(updatePa.FirstName, "update");
            Assert.AreEqual(updatePa.LastName, "update");
            Assert.AreEqual(updatePa.PhoneNumber, "1111");
            Assert.AreEqual(updatePa.MobilePhone, "52451111");
            Assert.AreEqual(updatePa.EMail, "hans@gibts.ja");
            Assert.AreEqual(updatePa.HomeAdress.Street, "Am Tal");
            Assert.AreEqual(updatePa.HomeAdress.HouseNumber, 14);
            Assert.AreEqual(updatePa.HomeAdress.City, "LINZKeks");
            Assert.AreEqual(updatePa.HomeAdress.Country, "schiland");
            Assert.AreEqual(updatePa.HomeAdress.DoorNumber, 111515);
            Assert.AreEqual(updatePa.HomeAdress.StairNumber, 45);
            Assert.AreEqual(updatePa.HomeAdress.Etage, 0);
            Assert.AreEqual(updatePa.HomeAdress.ZipCode, 50040);
            Assert.AreEqual(updatePa.IBAN, "AT1554859888898");
            Assert.AreEqual(updatePa.BIC, "SPPR1586XX");
            Assert.AreEqual(updatePa.AccountHolder, "Herman Günther");
            Assert.AreEqual(updatePa.SVN, 2225025496);
            Assert.AreEqual(updatePa.nationality, "Bauernland");
            Assert.AreEqual(updatePa.InfoField, "Infotext erweitert");

            //pa
            Assert.AreEqual(updatePa.Active, false);
            Assert.AreEqual(updatePa.ClosingDateDocuments, new DateTime(2016, 04, 05));
            Assert.AreEqual(updatePa.SV, false);
            Assert.AreEqual(updatePa.Dienstvertrag, false);
            Assert.AreEqual(updatePa.BestBH, false);
            Assert.AreEqual(updatePa.Grundkurs, false);
            Assert.AreEqual(updatePa.consumedHours, 2.1M);
            Assert.AreEqual(updatePa.deadLineHours, new DateTime(2016, 01, 09));



        }
        [TestCleanup()]
        public void tearDown()
        {

            PersonalAssistantDAO pdDao = new PersonalAssistantDAO();
            pdDao.DeletePersonalAssistantRecursive(idToDelete);
        }
    }
}