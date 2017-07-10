using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.DB
{
   public interface IPurchaserDAO
    {
        Purchaser Select(Purchaser pur);
        List<Purchaser> SelectAll();
        Purchaser Insert(Purchaser person);
        bool Update(Purchaser p);
    }
}
