using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DataContainer;

namespace SelvesSoftware.DbTestDummies
{
    public class PaDummyDao:IPersonalAssistantDAO
    {
        public PersonalAssistant Select(PersonalAssistant pa)
        {
            if (pa.Id == 54321)
            {
                pa.Active = true;
                pa.ClosingDateDocuments = new DateTime(2015, 10, 10);
                pa.EMail = "test@test.at";
                pa.EmploymentTimes = new List<Employment>();
                Employment first = new Employment();
                first.EmplBegin = new DateTime(2000, 01, 01);
                first.EmplEnd = new DateTime(2001, 01, 01);
                first.EmplId = 12345;
                Employment sec = new Employment();
                sec.EmplBegin = new DateTime(2014, 01, 01);
                sec.EmplId = 12333;
                pa.EmploymentTimes = new List<Employment>();
                pa.EmploymentTimes.Add(first);
                pa.EmploymentTimes.Add(sec);
                pa.FirstName = "Muster";
                pa.LastName = "Franz";
                pa.MobilePhone = "07327773546";
                EmploymentStatus es = new EmploymentStatus();
                es.Assistant = pa;
                Person p1 = new Person();
                p1.Id = 88888;
                p1.LastName = "Huber";
                p1.FirstName = "Hansl";
                es.Purchaser = new Purchaser(p1);
            
                pa.PhoneNumber = "18923742774";
                pa.Documents = new List<Document>();
                pa.Documents.Add(new Document());
                Adress ad = new Adress();
                ad.AdressId = 98765;
                ad.City = "Hagenberg";
                ad.Country = "Österreich";
                ad.DoorNumber = 2;
                ad.HouseNumber = 12;
                ad.Street = "Teststreet";
                ad.ZipCode = 4209;
                pa.HomeAdress = ad;

                return pa;
            }
            else
            {
                return null;
            }
        }

        public PersonalAssistant Insert(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public PersonalAssistant Update(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public PersonalAssistant select(PersonalAssistant pa)
        {
            if (pa.Id == 54321)
            {
                pa.Active = true;
                pa.ClosingDateDocuments = new DateTime(2015, 10, 10);
                pa.EMail = "test@test.at";
                pa.EmploymentTimes = new List<Employment>();
                Employment first = new Employment();
                first.EmplBegin = new DateTime(2000, 01, 01);
                first.EmplEnd = new DateTime(2001, 01, 01);
                first.EmplId = 12345;
                Employment sec = new Employment();
                sec.EmplBegin = new DateTime(2014, 01, 01);
                sec.EmplId = 12333;
                pa.EmploymentTimes = new List<Employment>();
                pa.EmploymentTimes.Add(first);
                pa.EmploymentTimes.Add(sec);
                pa.FirstName = "Muster";
                pa.LastName = "Franz";
                pa.MobilePhone = "07327773546";
                EmploymentStatus es = new EmploymentStatus();
                es.Assistant = pa;
                Person p1 = new Person();
                p1.Id = 88888;
                p1.LastName = "Huber";
                p1.FirstName = "Hansl";
                es.Purchaser = new Purchaser(p1);
                pa.Purchasers = new List<Purchaser>();
                pa.Purchasers.Add(new Purchaser(p1));
                
                pa.PhoneNumber = "18923742774";
                pa.Documents = new List<Document>();
                pa.Documents.Add(new Document());
                Adress ad = new Adress();
                ad.AdressId = 98765;
                ad.City = "Hagenberg";
                ad.Country = "Österreich";
                ad.DoorNumber = 2;
                ad.HouseNumber = 12;
                ad.Street = "Teststreet";
                ad.ZipCode = 4209;
                pa.HomeAdress = ad;
            }

            return pa;

        }

        public PersonalAssistant insert(PersonalAssistant Pa)
        {
            throw new NotImplementedException();
        }

        public PersonalAssistant update(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }


        public List<PersonalAssistant> SelectAll()
        {
            throw new NotImplementedException();
        }

        public List<PersonalAssistant> SelectSpecific(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void insertDocument(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void updateDocument(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void insertEmploymentStatus(Purchaser pur, PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void deleteEmploymentStatus(Purchaser pur, PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void selectPurchaserList(PersonalAssistant pa)
        {
            throw new NotImplementedException();
        }

        public void insertEmployment(Employment e)
        {
            throw new NotImplementedException();
        }

        public void deleteEmployment(Employment e)
        {
            throw new NotImplementedException();
        }
    }
}
