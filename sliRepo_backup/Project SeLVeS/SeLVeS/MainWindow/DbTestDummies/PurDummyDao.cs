using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.BusinessLogic;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware.DbTestDummies
{
    public class PurDummyDao : IPurchaserDataDAO
    {
        public void changeGlobals(decimal stundensatz, decimal stundensatzAusz, decimal fahrtkostenzusatzKM)
        {
            throw new NotImplementedException();
        }

        public PurchaserData Insert(PurchaserData purData)
        {
            throw new NotImplementedException();
        }

        public PurchaserData InsertPurData(PurchaserData purData)
        {
            throw new NotImplementedException();
        }

        public PurchaserData Select(PurchaserData pur)
        {
            if (pur.Purchaser.Id == 54321)
            {
                pur.Purchaser.Active = true;
                pur.Purchaser.FirstName = "Hugo";
                pur.Purchaser.LastName = "Huber";
                pur.Purchaser.MobilePhone = "982374283745";
                pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
                pur.Purchaser.EMail = "test@test.at";
                pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
                pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
                pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
                pur.Purchaser.Employees = new List<EmploymentStatus>();
                EmploymentStatus e =
                    new EmploymentStatus(
                        new PersonalAssistant(new Person("Brigitte", "Fritte",
                            new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))),pur.Purchaser);
                EmploymentStatus e1 =
                    new EmploymentStatus(
                        new PersonalAssistant(new Person("Max", "Moritz",
                            new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), pur.Purchaser);

                pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
                pur.Purchaser.PhoneNumber = "28374937453987";

                pur.Income = 1500;
                pur.InputIncome = 150;
                pur.Month = 6;
                pur.Year = 2015;
            
                pur.TravellingAllowanceKM = (decimal) 0.22;
                pur.HourlyRate = 20;
                pur.HourlyRatePayoff = (decimal)20.5;
                pur.CareAllowanceMaximum = 300;
                pur.CareAllowance = 2;
                pur.AssistenceDemand = 60;

                return pur;
 
            }
            else
            {
                return null;
            }
        }

        public List<PurchaserData> SelectAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(PurchaserData p)
        {
            throw new NotImplementedException();
        }

        public void UpdateGlobals()
        {
            throw new NotImplementedException();
        }

        public bool UpdatePurData(PurchaserData purData)
        {
            throw new NotImplementedException();
        }
    }
}
