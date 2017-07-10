using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware.DBDummies
{
    public class MbDummyDao:IMonthlyBillingDAO
    {
        public List<MonthlyBilling> selectAllMB()
        {
            MonthlyBilling mb1 = new MonthlyBilling();
            MonthlyBilling mb2 = new MonthlyBilling();
            MonthlyBillingPerPa mbppa1 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa2 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa3 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa4 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa5 = new MonthlyBillingPerPa();
            mb1.Month = 9;
            mb1.Year = 2015;

            mb2.Month = 8;
            mb2.Year = 2015;


           

            EffortEntry e1 = new EffortEntry(2011, 06, 12,
                new Purchaser(new Person("Hansi", "Müller", new Adress("Teststrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Elisabeth", "Schütz", new Adress("Hausstrasse", 12, 3445, "Linz"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e2 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Lisa", "Bauer", new Adress("Hochstrasse", 34, 7654, "Town"))), new PersonalAssistant(new Person("Hans", "Kunz", new Adress("testtest", 87, 6354, "Enns"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);


            EffortEntry e3 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Thomas", "Mann", new Adress("Klammstrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Uwe", "Lamm", new Adress("Hausstrasse", 12, 1234, "Steyr"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e4 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Tom", "Bauer", new Adress("Friedlstrasse", 344, 9876, "Hall"))), new PersonalAssistant(new Person("Julia", "Niedermaier", new Adress("Lastenstrasse", 56, 6354, "Wels"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);
            e1.A1 = new Activity();
            e1.A2 = new Activity();
            e1.A3 = new Activity();
            e1.A1.Name = new Activity.State();
            e1.A2.Name = new Activity.State();
            e1.A3.Name = new Activity.State();
            e1.A1.Name = Activity.State.Nachtbereitschaft;
            e1.A2.Name = Activity.State.Grundversorgung;
            e1.A3.Name = Activity.State.Begleitung;
            e1.Day = 1;
            e1.Month = 2;
            e1.Year = 2015;
            e1.From = new DateTime();
            
            e1.From.AddHours(11);
            e1.From.AddMinutes(00);
            e1.To = new DateTime();
            e1.To.AddHours(12);
            e1.To.AddMinutes(00);
            e1.Km = 15;

            e2.A1 = new Activity();
            e2.A2 = new Activity();
            e2.A1.Name = new Activity.State();
            e2.A2.Name = new Activity.State();
            e2.A1.Name = Activity.State.Begleitung;
            e2.A2.Name = Activity.State.Freizeitgestaltung;
            e2.Day = 3;
            e2.Month = 7;
            e2.Year = 2015;
            e2.From = new DateTime(2015,7,3,11,22,12);
            e2.To = new DateTime(2015, 7, 3, 15, 00, 12);
    
            e2.Km = 15;

            mb1.Purchaser = new PurchaserData();
            mb2.Purchaser = new PurchaserData();

            mb2.Purchaser.HourlyRatePayoff = 20;
            mb1.Purchaser.HourlyRatePayoff = 20;
            e3.A1 = new Activity();
            e3.A2 = new Activity();
            e3.A1.Name = new Activity.State();
            e3.A2.Name = new Activity.State();
            e3.A1.Name = Activity.State.Begleitung;
            e3.A2.Name = Activity.State.Freizeitgestaltung;
            e3.Day = 3;
            e3.Month = 7;
            e3.Year = 2015;
            e3.From = new DateTime(2015, 7,3 , 22, 22, 12);
            e3.To = new DateTime(2015, 7,4 , 06, 00, 00);
           
            e3.Km = 15;

            e4.A1 = new Activity();
            e4.A2 = new Activity();
            e4.A1.Name = new Activity.State();
            e4.A2.Name = new Activity.State();
            e4.A1.Name = Activity.State.Begleitung;
            e4.A2.Name = Activity.State.Freizeitgestaltung;
            e4.Day = 3;
            e4.Month = 7;
            e4.Year = 2015;
            e4.From = new DateTime();
            e4.From.AddHours(12);
            e4.From.AddMinutes(00);
            e4.To = new DateTime();
            e4.To.AddHours(15);
            e4.To.AddMinutes(00);
            e4.Km = 15;

            mbppa1.EffortList = new List<EffortEntry>();
            mbppa2.EffortList = new List<EffortEntry>();
            mbppa3.EffortList = new List<EffortEntry>();

            mbppa1.EffortList.Add(e2);
            mbppa1.EffortList.Add(e1);
            mbppa1.EffortList.Add(e4);

            mbppa2.EffortList.Add(e3);
            mbppa2.EffortList.Add(e4);

            mbppa1.BillableKm = 22;
            mbppa1.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa1.Pur = new PurchaserData();
            mbppa1.Pur.Purchaser.Active = true;
            mbppa1.Pur.Purchaser.FirstName = "Hugo";
            mbppa1.Pur.Purchaser.LastName = "Huber";
            mbppa1.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa1.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa1.Pur.Purchaser.EMail = "test@test.at";
            mbppa1.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa1.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa1.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa1.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e0 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);
            EmploymentStatus e5 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);

            mbppa1.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa1.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa1.Pur.Income = 1500;
            mbppa1.Pur.InputIncome = 150;
            mbppa1.Pur.Month = 6;
            mbppa1.Pur.Year = 2015;

            mbppa1.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa1.Pur.HourlyRate = 20;
            mbppa1.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa1.Pur.CareAllowanceMaximum = 300;
            mbppa1.Pur.CareAllowance = 2;
            mbppa1.Pur.AssistenceDemand = 60;

            mbppa2.BillableKm = 34;
            mbppa2.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa2.Pur = new PurchaserData();
            mbppa2.Pur.Purchaser.Active = true;
            mbppa2.Pur.Purchaser.FirstName = "Hugo";
            mbppa2.Pur.Purchaser.LastName = "Huber";
            mbppa2.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa2.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa2.Pur.Purchaser.EMail = "test@test.at";
            mbppa2.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa2.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa2.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa2.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e9 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);
            EmploymentStatus e7 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);

            mbppa2.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa2.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa2.Pur.Income = 1500;
            mbppa2.Pur.InputIncome = 150;
            mbppa2.Pur.Month = 6;
            mbppa2.Pur.Year = 2015;

            mbppa2.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa2.Pur.HourlyRate = 20;
            mbppa2.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa2.Pur.CareAllowanceMaximum = 300;
            mbppa2.Pur.CareAllowance = 2;
            mbppa2.Pur.AssistenceDemand = 60;

            mb1.MbPerPaList = new List<MonthlyBillingPerPa>();
            mb2.MbPerPaList = new List<MonthlyBillingPerPa>();

            mb1.MbPerPaList.Add(mbppa1);
            mb1.MbPerPaList.Add(mbppa2);

            mb2.MbPerPaList.Add(mbppa2);
            List<MonthlyBilling> list = new List<MonthlyBilling>();
            list.Add(mb1);
            list.Add(mb2);

            return list;
        }

        public void selectMB(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }

        public MonthlyBillingPerPa selectMBEntry(MonthlyBillingPerPa mb)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> SelectPeriod(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectSpecificMB(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectSpecificMB(Purchaser pur)
        {
            throw new NotImplementedException();
        }

        public List<MonthlyBilling> selectAllMB(MonthlyBilling pur)
        {
            MonthlyBilling mb1 = new MonthlyBilling();
            MonthlyBilling mb2 = new MonthlyBilling();
            MonthlyBillingPerPa mbppa1 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa2 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa3 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa4 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa5 = new MonthlyBillingPerPa();
            mb1.Month = 9;
            mb1.Year = 2015;

            mb2.Month = 8;
            mb2.Year = 2015;

            


            EffortEntry e1 = new EffortEntry(2011, 06, 12,
                new Purchaser(new Person("Hansi", "Müller", new Adress("Teststrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Elisabeth", "Schütz", new Adress("Hausstrasse", 12, 3445, "Linz"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e2 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Lisa", "Bauer", new Adress("Hochstrasse", 34, 7654, "Town"))), new PersonalAssistant(new Person("Hans", "Kunz", new Adress("testtest", 87, 6354, "Enns"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);


            EffortEntry e3 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Thomas", "Mann", new Adress("Klammstrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Uwe", "Lamm", new Adress("Hausstrasse", 12, 1234, "Steyr"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e4 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Tom", "Bauer", new Adress("Friedlstrasse", 344, 9876, "Hall"))), new PersonalAssistant(new Person("Julia", "Niedermaier", new Adress("Lastenstrasse", 56, 6354, "Wels"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);
            e1.A1 = new Activity();
            e1.A2 = new Activity();
            e1.A3 = new Activity();
            e1.A1.Name = new Activity.State();
            e1.A2.Name = new Activity.State();
            e1.A3.Name = new Activity.State();
            e1.A1.Name = Activity.State.Nachtbereitschaft;
            e1.A2.Name = Activity.State.Grundversorgung;
            e1.A3.Name = Activity.State.Begleitung;
            e1.Day = 1;
            e1.Month = 2;
            e1.Year = 2015;
            e1.From = new DateTime();
            e1.From.AddHours(13);
            e1.From.AddMinutes(00);
            e1.To = new DateTime();
            e1.To.AddHours(19);
            e1.To.AddMinutes(00);
            e1.Km = 15;

            e2.A1 = new Activity();
            e2.A2 = new Activity();
            e2.A1.Name = new Activity.State();
            e2.A2.Name = new Activity.State();
            e2.A1.Name = Activity.State.Begleitung;
            e2.A2.Name = Activity.State.Freizeitgestaltung;
            e2.Day = 3;
            e2.Month = 7;
            e2.Year = 2015;
            e2.From = new DateTime();
            e2.From.AddHours(12);
            e2.From.AddMinutes(00);
            e2.To = new DateTime();
            e2.To.AddHours(15);
            e2.To.AddMinutes(00);
            e2.Km = 15;


            e3.A1 = new Activity();
            e3.A2 = new Activity();
            e3.A1.Name = new Activity.State();
            e3.A2.Name = new Activity.State();
            e3.A1.Name = Activity.State.Begleitung;
            e3.A2.Name = Activity.State.Freizeitgestaltung;
            e3.Day = 3;
            e3.Month = 7;
            e3.Year = 2015;
            e3.From = new DateTime();
            e3.From.AddHours(12);
            e3.From.AddMinutes(00);
            e3.To = new DateTime();
            e3.To.AddHours(15);
            e3.To.AddMinutes(00);
            e3.Km = 15;

            e4.A1 = new Activity();
            e4.A2 = new Activity();
            e4.A1.Name = new Activity.State();
            e4.A2.Name = new Activity.State();
            e4.A1.Name = Activity.State.Begleitung;
            e4.A2.Name = Activity.State.Freizeitgestaltung;
            e4.Day = 3;
            e4.Month = 7;
            e4.Year = 2015;
            e4.From = new DateTime();
            e4.From.AddHours(12);
            e4.From.AddMinutes(00);
            e4.To = new DateTime();
            e4.To.AddHours(15);
            e4.To.AddMinutes(00);
            e4.Km = 15;

            mbppa1.EffortList = new List<EffortEntry>();
            mbppa2.EffortList = new List<EffortEntry>();
            mbppa3.EffortList = new List<EffortEntry>();

            mbppa1.EffortList.Add(e2);
            mbppa1.EffortList.Add(e1);
            mbppa1.EffortList.Add(e4);

            mbppa2.EffortList.Add(e3);
            mbppa2.EffortList.Add(e4);

            mbppa1.BillableKm = 22;
            mbppa1.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa1.Pur = new PurchaserData();
            mbppa1.Pur.Purchaser.Active = true;
            mbppa1.Pur.Purchaser.FirstName = "Hugo";
            mbppa1.Pur.Purchaser.LastName = "Huber";
            mbppa1.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa1.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa1.Pur.Purchaser.EMail = "test@test.at";
            mbppa1.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa1.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa1.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa1.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e0 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);
            EmploymentStatus e5 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);

            mbppa1.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa1.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa1.Pur.Income = 1500;
            mbppa1.Pur.InputIncome = 150;
            mbppa1.Pur.Month = 6;
            mbppa1.Pur.Year = 2015;

            mbppa1.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa1.Pur.HourlyRate = 20;
            mbppa1.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa1.Pur.CareAllowanceMaximum = 300;
            mbppa1.Pur.CareAllowance = 2;
            mbppa1.Pur.AssistenceDemand = 60;

            mbppa2.BillableKm = 34;
            mbppa2.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa2.Pur = new PurchaserData();
            mbppa2.Pur.Purchaser.Active = true;
            mbppa2.Pur.Purchaser.FirstName = "Hugo";
            mbppa2.Pur.Purchaser.LastName = "Huber";
            mbppa2.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa2.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa2.Pur.Purchaser.EMail = "test@test.at";
            mbppa2.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa2.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa2.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa2.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e9 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);
            EmploymentStatus e7 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);

            mbppa2.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa2.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa2.Pur.Income = 1500;
            mbppa2.Pur.InputIncome = 150;
            mbppa2.Pur.Month = 6;
            mbppa2.Pur.Year = 2015;

            mbppa2.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa2.Pur.HourlyRate = 20;
            mbppa2.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa2.Pur.CareAllowanceMaximum = 300;
            mbppa2.Pur.CareAllowance = 2;
            mbppa2.Pur.AssistenceDemand = 60;

            mb1.MbPerPaList = new List<MonthlyBillingPerPa>();
            mb2.MbPerPaList = new List<MonthlyBillingPerPa>();

            mb1.MbPerPaList.Add(mbppa1);
            mb1.MbPerPaList.Add(mbppa2);

            mb2.MbPerPaList.Add(mbppa2);
            List<MonthlyBilling> list = new List<MonthlyBilling>();
            list.Add(mb1);
            list.Add(mb2);

            return list;

        }
        public MonthlyBilling InsertMonthlyBilling(MonthlyBilling mb)
        {
            //falsches Insert
            throw new NotImplementedException();
        }
        public MonthlyBillingPerPa InsertMonthlyBilling(MonthlyBillingPerPa mb)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMonthlyBilling(MonthlyBilling mb)
        {
            //falsches update
            throw new NotImplementedException();
        }

        public bool UpdateMonthlyBillingEntry(MonthlyBillingPerPa mb)
        {
            throw new NotImplementedException();
        }
        public List<MonthlyBillingPerPa> SelectMBperPa(List<MonthlyBillingPerPa> mbppa)
        {
            throw new NotImplementedException();
        }


        public List<MonthlyBilling> selectAllFrom(DateTime? nullable)
        {
            List<MonthlyBilling> mb = new List<MonthlyBilling>();
            MonthlyBilling mb1 = new MonthlyBilling();
            MonthlyBilling mb2 = new MonthlyBilling();
            MonthlyBillingPerPa mbppa1 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa2 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa3 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa4 = new MonthlyBillingPerPa();
            MonthlyBillingPerPa mbppa5 = new MonthlyBillingPerPa();
            mb1.Month = 6;
            mb1.Year = 2015;

            mb2.Month = 5;
            mb2.Year = 2015;
            EffortEntry e1 = new EffortEntry(2011, 06, 12,
                new Purchaser(new Person("Hansi", "Müller", new Adress("Teststrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Elisabeth", "Schütz", new Adress("Hausstrasse", 12, 3445, "Linz"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e2 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Lisa", "Bauer", new Adress("Hochstrasse", 34, 7654, "Town"))), new PersonalAssistant(new Person("Hans", "Kunz", new Adress("testtest", 87, 6354, "Enns"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);


            EffortEntry e3 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Thomas", "Mann", new Adress("Klammstrasse", 2, 2323, "City"))), new PersonalAssistant(new Person("Uwe", "Lamm", new Adress("Hausstrasse", 12, 1234, "Steyr"))), new DateTime(2011, 5, 6), new DateTime(2012, 7, 7), 22);

            EffortEntry e4 = new EffortEntry(2011, 06, 12,
               new Purchaser(new Person("Tom", "Bauer", new Adress("Friedlstrasse", 344, 9876, "Hall"))), new PersonalAssistant(new Person("Julia", "Niedermaier", new Adress("Lastenstrasse", 56, 6354, "Wels"))), new DateTime(2013, 5, 6), new DateTime(2015, 7, 7), 242);
            e1.A1 = new Activity();
            e1.A2 = new Activity();
            e1.A3 = new Activity();
            e1.A1.Name = new Activity.State();
            e1.A2.Name = new Activity.State();
            e1.A3.Name = new Activity.State();
            e1.A1.Name = Activity.State.Nachtbereitschaft;
            e1.A2.Name = Activity.State.Grundversorgung;
            e1.A3.Name = Activity.State.Begleitung;
            e1.Day = 1;
            e1.Month = 2;
            e1.Year = 2015;
            e1.From = new DateTime(2015,2,1,10,23,00);
           
            e1.To = new DateTime(2015, 2, 1, 20, 00, 00);
            
            e1.Km = 15;

            e2.A1 = new Activity();
            e2.A2 = new Activity();
            e2.A1.Name = new Activity.State();
            e2.A2.Name = new Activity.State();
            e2.A1.Name = Activity.State.Begleitung;
            e2.A2.Name = Activity.State.Freizeitgestaltung;
            e2.Day = 3;
            e2.Month = 7;
            e2.Year = 2015;
            e2.From = new DateTime(2015,7,3,6,20,00);
            e2.To = new DateTime(2015, 7, 3, 6, 20, 00);

            e2.Km = 15;


            e3.A1 = new Activity();
            e3.A2 = new Activity();
            e3.A1.Name = new Activity.State();
            e3.A2.Name = new Activity.State();
            e3.A1.Name = Activity.State.Begleitung;
            e3.A2.Name = Activity.State.Freizeitgestaltung;
            e3.Day = 3;
            e3.Month = 7;
            e3.Year = 2015;
            e3.From = new DateTime();
            e3.From.AddHours(12);
            e3.From.AddMinutes(00);
            e3.To = new DateTime();
            e3.To.AddHours(15);
            e3.To.AddMinutes(00);
            e3.Km = 15;

            e4.A1 = new Activity();
            e4.A2 = new Activity();
            e4.A1.Name = new Activity.State();
            e4.A2.Name = new Activity.State();
            e4.A1.Name = Activity.State.Begleitung;
            e4.A2.Name = Activity.State.Freizeitgestaltung;
            e4.Day = 3;
            e4.Month = 7;
            e4.Year = 2015;
            e4.From = new DateTime(2015,2,1,07,25,00);

            e4.To = new DateTime(2015, 2, 1, 16, 00, 00);
            
            e4.Km = 15;

            mbppa1.EffortList = new List<EffortEntry>();
            mbppa2.EffortList = new List<EffortEntry>();
            mbppa3.EffortList = new List<EffortEntry>();

            mbppa1.EffortList.Add(e2);
            mbppa1.EffortList.Add(e1);
            mbppa1.EffortList.Add(e4);

            mbppa2.EffortList.Add(e3);
            mbppa2.EffortList.Add(e4);

            mbppa1.BillableKm = 22;
            mbppa1.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa1.Pur = new PurchaserData();
            mbppa1.Pur.Purchaser.Active = true;
            mbppa1.Pur.Purchaser.FirstName = "Hugo";
            mbppa1.Pur.Purchaser.LastName = "Huber";
            mbppa1.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa1.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa1.Pur.Purchaser.EMail = "test@test.at";
            mbppa1.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa1.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa1.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa1.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e0 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);
            EmploymentStatus e5 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa1.Pur.Purchaser);

            mbppa1.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa1.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa1.Pur.Income = 1500;
            mbppa1.Pur.InputIncome = 150;
            mbppa1.Pur.Month = 6;
            mbppa1.Pur.Year = 2015;

            mbppa1.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa1.Pur.HourlyRate = 20;
            mbppa1.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa1.Pur.CareAllowanceMaximum = 300;
            mbppa1.Pur.CareAllowance = 2;
            mbppa1.Pur.AssistenceDemand = 60;

            mbppa2.BillableKm = 34;
            mbppa2.Pa = new PersonalAssistant(new Person("Franz", "Müller", new Adress("Sandl", 2, 2345, "Altstadt")));
            mbppa2.Pur = new PurchaserData();
            mbppa2.Pur.Purchaser.Active = true;
            mbppa2.Pur.Purchaser.FirstName = "Hugo";
            mbppa2.Pur.Purchaser.LastName = "Huber";
            mbppa2.Pur.Purchaser.MobilePhone = "982374283745";
            mbppa2.Pur.Purchaser.EntryDate = new DateTime(2010, 12, 12);
            mbppa2.Pur.Purchaser.EMail = "test@test.at";
            mbppa2.Pur.Purchaser.ApprovalBegin = new DateTime(2015, 01, 01);
            mbppa2.Pur.Purchaser.ApprovalEnd = new DateTime(2015, 12, 31);
            mbppa2.Pur.Purchaser.ContactPerson = new Person("Maria", "Huber", new Adress("Hauptstrasse", 12, 4040, "Linz"));
            mbppa2.Pur.Purchaser.Employees = new List<EmploymentStatus>();
            EmploymentStatus e9 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Brigitte", "Fritte",
                        new Adress("Landstrasse", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);
            EmploymentStatus e7 =
                new EmploymentStatus(
                    new PersonalAssistant(new Person("Max", "Moritz",
                        new Adress("Maienweg", 22, 4209, "Engerwitzdorf"))), mbppa2.Pur.Purchaser);

            mbppa2.Pur.Purchaser.HomeAdress = new Adress("Heimatstrasse", 124, 2345, "Wien");
            mbppa2.Pur.Purchaser.PhoneNumber = "28374937453987";

            mbppa2.Pur.Income = 1500;
            mbppa2.Pur.InputIncome = 150;
            mbppa2.Pur.Month = 6;
            mbppa2.Pur.Year = 2015;

            mbppa2.Pur.TravellingAllowanceKM = (decimal)0.22;
            mbppa2.Pur.HourlyRate = 20;
            mbppa2.Pur.HourlyRatePayoff = (decimal)20.5;
            mbppa2.Pur.CareAllowanceMaximum = 300;
            mbppa2.Pur.CareAllowance = 2;
            mbppa2.Pur.AssistenceDemand = 60;

            mb1.MbPerPaList = new List<MonthlyBillingPerPa>();
            mb2.MbPerPaList = new List<MonthlyBillingPerPa>();

            mb1.MbPerPaList.Add(mbppa1);
            mb1.MbPerPaList.Add(mbppa2);

            mb2.MbPerPaList.Add(mbppa2);
            List<MonthlyBilling> list = new List<MonthlyBilling>();
            list.Add(mb1);
            list.Add(mb2);

            return list;
            

            
        }

        public List<MonthlyBillingPerPa> SelectMBperPa(MonthlyBilling mb)
        {
            throw new NotImplementedException();
        }

        public void DeleteReha(int month, int year, long agid, int days)
        {
            throw new NotImplementedException();
        }

        public void InsertReha(long agid, int month, int year, int days)
        {
            throw new NotImplementedException();
        }

        public int selectReha(long agid, int month, int year)
        {
            throw new NotImplementedException();
        }
    }
}
