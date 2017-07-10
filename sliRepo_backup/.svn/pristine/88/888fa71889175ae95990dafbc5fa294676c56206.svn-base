using SelvesSoftware.DataContainer;
using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
    public class PurchaserDataBl:IPurchaserDataBl
    {

        private IPurchaserDataDAO _purDao;
        public IPurchaserDataDAO PurDao
        {
            get
            {
                if (_purDao == null)
                {
                    _purDao = new PurchaserDataDAO();
                }
                return _purDao;
            }
            set
            {
                _purDao = value;
            }
        }

        public Purchaser Purchaser;

        public PurchaserDataBl (Purchaser pur){
            Purchaser = pur;
        }

        public PurchaserDataBl()
        {
            Purchaser = new Purchaser();
        }

        public PurchaserData SelectPurchaser(int id)
        {
            PurchaserData p = new PurchaserData(id);
            
            try
            {
                p = PurDao.Select(p);
            }
            catch (ExceptionHandler e)
            {

            }
            p.CareAllowanceMaximum=calculateMaxCareAllowance(p);
            return p;
        }

        public List<PurchaserData> SelectAllPurchaser()
        {
            List<PurchaserData> paList = new List<PurchaserData>();

            paList = PurDao.SelectAll();

            foreach(PurchaserData pd in paList){
                pd.CareAllowanceMaximum = calculateMaxCareAllowance(pd);

            }
           
            //Fehlerbehandlung
            return paList;
        }

        private decimal calculateMaxCareAllowance(PurchaserData pd)
        {
            if (pd.CareAllowanceMaximum == 0)
            {
                return pd.CareAllowance * (decimal)0.8;
            }
            else
            {
                return pd.CareAllowanceMaximum;
            }
        }

        public List<PurchaserData> SelectSpecificPurchaser()
        {
            throw new NotImplementedException();
        }

        public bool CreatePurchaser(PurchaserData p)
        {
                if (p.CareAllowanceMaximum == 0)
                {
                    p.CareAllowanceMaximum = p.CareAllowance * (decimal)0.8;
                }
                
                try
                {
                    PurDao.Insert(p);
                }
                catch (Exception e)
                {
                Console.WriteLine(e.Message);
                }
                return true;

        }

        private bool EntriesValid()
        {
            throw new NotImplementedException();
        }

        public bool UpdatePurchaser(PurchaserData p)
        {
            try {
                
                PurDao.Update(p);
               
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return true;
        }

        public void addPAEmployment(long agId, long paId)
        {
            throw new NotImplementedException();
        }

        public void changeGlobals(decimal stundensatz, decimal stundensatzAusz,  decimal fahrtkostenzusatzKM)
        {        
            PurDao = new PurchaserDataDAO();
            //get all PurchaserDatas
            List<PurchaserData> purchasers= PurDao.SelectAll();

            //insert new PurchaserData in DB with new Globals
            foreach(PurchaserData p in purchasers)
            {
                p.TravellingAllowanceKM = fahrtkostenzusatzKM;
                p.HourlyRate = stundensatz;
                p.HourlyRatePayoff = stundensatzAusz;
                

                if (p.Month == DateTime.Now.Month && p.Year == DateTime.Now.Year)
                {
                    PurDao.UpdatePurData(p);
                }
                else
                {
                    p.Month = DateTime.Now.Month;
                    p.Year = DateTime.Now.Year;
                    PurDao.InsertPurData(p);
                }
            }
        }

        public PurchaserData Select(PurchaserData pur)
        {
           return PurDao.Select(pur);
        }
    }
}
