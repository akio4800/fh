using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SelvesSoftware.BusinessLogic
{
    public class PersonalAssistentBl : IPersonalAssistentBl
    {
        private IPersonalAssistantDAO _paDao;
        public IPersonalAssistantDAO PaDao
        {
            get
            {
                if (_paDao == null)
                {
                    _paDao = new PersonalAssistantDAO();
                }
                return _paDao;
            }
            set
            {
                _paDao = value;
            }
        }
        public PersonalAssistant SelectPa(int id)
        {
            PersonalAssistant p = new PersonalAssistant(id);
            try
            {
                p = PaDao.select(p);
            }
            catch (Exception e)
            {

                Console.WriteLine("Select hat nix gefunden");
            }
            return p;
        }

        private PersonalAssistant checkWeiterbidlung(PersonalAssistant p)
        {
            if (p.deadLineHours==null||p.deadLineHours.Value.Year == 1 )
            {
                p.deadLineHours = DateTime.Now;
            }
            if (p.consumedHours >= 16)
            {               
                p.deadLineHours = p.deadLineHours.Value.AddYears(2);
                p.consumedHours = 0;
                p = PaDao.update(p);
            }
            return p;
        }

        public List<PersonalAssistant> SelectAllPa()
        {
            List<PersonalAssistant> paList = new List<PersonalAssistant>();
            paList = PaDao.SelectAll();
            //Fehlerbehandlung
            return paList;
        }

        public void deleteEmployment(Employment e)
        {
            PaDao.deleteEmployment(e);
        }

        public void insertEmployment(Employment e)
        {
            PaDao.insertEmployment(e);

        }


        public List<PersonalAssistant> SelectSpecificPa(PersonalAssistant pa)
        {
            List<PersonalAssistant> paList = new List<PersonalAssistant>();
            paList = PaDao.SelectSpecific(pa);
            //Fehlerbehandlung
            return paList;
        }

        public bool CreatePa(PersonalAssistant pa)
        {
            if (EntriesValid())
            {
                _paDao = new PersonalAssistantDAO();
                try
                {
                    pa = checkEmployee(pa);
                    pa = checkWeiterbidlung(pa);                
                    PaDao.insert(pa);

                    // adding employment status
                    if (pa.Purchasers != null)
                    {
                       foreach(Purchaser p in pa.Purchasers)
                        {
                            PaDao.insertEmploymentStatus(p, pa);
                        }
                    }
                }
                catch (ExceptionHandler e)
                {
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private PersonalAssistant checkEmployee(PersonalAssistant pa)
        {
            if (pa.EmploymentTimes == null)
            {
                pa.EmploymentTimes = new List<Employment>();
                Employment e = new Employment();
                DateTime d = DateTime.Now;
                e.EmplBegin = d;
                pa.EmploymentTimes.Add(e);
            }else if (pa.EmploymentTimes.Count == 0)
            {
                Employment e = new Employment();
                DateTime d = DateTime.Now;
                e.EmplBegin = d;
                pa.EmploymentTimes.Add(e);

            }

            if (pa.EmploymentTimes.Last().EmplBegin.Year == 1)
                pa.EmploymentTimes.Last().EmplBegin = DateTime.Now;
            PaDao.update(pa);
            return pa;
        }

        private bool EntriesValid()
        {
            return true;
        }
        public bool UpdatePa(PersonalAssistant pa)
        {
            pa = checkEmployee(pa);
            pa=checkWeiterbidlung(pa);
            PaDao.update(pa);
            //Fehlerbehandlung
            return true;
        }
    }
}
