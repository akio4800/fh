using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.DB
{
   public interface IEffortEntryDAO
    {

        void UpdateEntry(EffortEntry ee);       
        
       /// <summary>
       /// Hier müssen zumindest Jahr,Monat,Ag mit id und PA mit id übergeben werden.
       /// </summary>
       /// <param name="e"></param>
       /// <returns></returns>
        EffortEntry GetEntry(EffortEntry ee);
        void InsertEntry(EffortEntry ee);
        void deleteEntry(EffortEntry ee);
        
    }
}
