using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{

   
    public class EffortEntryBl:IEffortEntryBL
    {

        private IEffortEntryDAO _eeDao;
        public IEffortEntryDAO EeDao
        {
            get
            {
                if (_eeDao == null)
                {
                    _eeDao = new EffortEntryDAO();
                }
                return _eeDao;
            }
            set
            {
                _eeDao = value;
            }

        }
        public void DoEffortEntry(EffortEntry e)
        {
           EffortEntryDAO eDao = new EffortEntryDAO();
            eDao.InsertEntry(e);
        }

        public void deleteEntry(EffortEntry e)
        {

            EffortEntryDAO eDAO = new EffortEntryDAO();
            eDAO.deleteEntry(e);

          


        }

        public List<EffortEntry> GetEntriesByPa(PersonalAssistant pa, int month, int year)
        {
            EffortEntryDAO eDao = new EffortEntryDAO();
            List<EffortEntry> entry = eDao.GetEntriesByPa(pa,month,year);
            return null;
        }

        public List<EffortEntry> GetEntriesByPurchaser(Purchaser p, int month, int year)
        {
            EffortEntryDAO eDao = new EffortEntryDAO();
            List<EffortEntry> entry = eDao.GetEntriesByPurchaser(p,month,year);
            return null;
        }

        public EffortEntry GetEntry(PersonalAssistant pa, int month, int year, int day)
        {
            EffortEntryDAO eDao = new EffortEntryDAO();
            EffortEntry entry = eDao.GetEntry(pa,month,year,day);
            return null;
        }

        public void ModifyEntry(EffortEntry e)
        {
            EffortEntryDAO eDao = new EffortEntryDAO();
            eDao.UpdateEntry(e);
        } 
        
        
        public double getcalculableHours(EffortEntry e)
        {
            //abgerechenbare stunden (aktivitäten) zurückgeben
            return 1.09; //zum testen weilß null kann auch kein wert

        }

        public List<EffortEntry> GetEntries(PersonalAssistant pa, Purchaser pur, int month, int year)
        {
            EffortEntryDAO eDao = new EffortEntryDAO();
            return eDao.GetEntries(pa.Id, pur.Id, month, year);
        }
    }
}
