using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.DB
{
    /// <summary>
    /// author: TS
    /// </summary>
   public  interface IAdressDAO
    {
        Adress Select(Adress a);
        void Update(Adress a);
        Adress Insert(Adress a);
    }
}
