using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelvesSoftware.DB;

namespace SelvesSoftware.BusinessLogic
{
   public class PAtoPurchaserBL
    {
        PAtoPurchaserDAO PDao = new PAtoPurchaserDAO();

        public void add(PersonalAssistant pa, Purchaser p)
        {
            PDao.add(pa, p);

        }

        public void delete(PersonalAssistant pa, Purchaser p)
        {
            PDao.delete(pa, p);

        }

    }
}
