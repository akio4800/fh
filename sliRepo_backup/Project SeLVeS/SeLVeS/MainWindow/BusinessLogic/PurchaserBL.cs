using SelvesSoftware.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
    public class PurchaserBL : IPurchaserBL
    {

        private IPurchaserDAO purDao;
        public IPurchaserDAO PurDao
        {
            get
            {
                if (purDao == null)
                {
                    purDao = new PurchaserDAO();
                }
                return purDao;
            }
            set
            {
                purDao = value;
            }
        }
        public Purchaser selectPurchaser(Purchaser pur)
        {
           
            Purchaser p = new Purchaser(pur);
            try
            {
                p = PurDao.select(pur);
                if (p.empty()) {
                    throw new ExceptionHandler("Auftraggeber existiert nicht in der Datenbank!");              
                }
            }
            catch (ExceptionHandler e)
            {

            }

            return p;
        }

        public List<Purchaser> selectAllPurchaser()
        {
            List<Purchaser> paList = new List<Purchaser>();

            paList = PurDao.selectAll();
            
            //Fehlerbehandlung
            return paList;
        }

        public List<Purchaser> selectSpecificPurchaser()
        {
            throw new NotImplementedException();
        }

        public bool createPurchaser(Purchaser p)
        {
            if (entriesValid())
            {
               
                try
                {
                    PurDao.insert(p);
                }
                catch (Exception e)
                {
                    //Fehlerbehandlung
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool entriesValid()
        {
            throw new NotImplementedException();
        }

        public bool updatePurchaser(Purchaser p)
        {

            PurDao.update(p);
            //Fehlerbehandlung
            return true;
        }
    }
}
