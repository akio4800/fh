using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
   public interface IPurchaserBL
    {
        /// <summary>
        /// Abfrage eines AG mit einer bestimmten id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>alle Daten des AG</returns>
         Purchaser selectPurchaser(Purchaser pur);

        /// <summary>
        /// Abrage aller AG
        /// </summary>
        /// <returns>Liste von AGs</returns>
         List<Purchaser> selectAllPurchaser();

        /// <summary>
        /// Wählt einen AG mit bestimmten Parametern aus (für die Suche)
        /// </summary>
        /// <returns>Liste der AG</returns>
         List<Purchaser> selectSpecificPurchaser();

        /// <summary>
        /// legt einen AG an
        /// </summary>
        /// <param name="p"></param>
        /// <returns>true wenn es funktioniert hat sonst false</returns>
         bool createPurchaser(Purchaser p);

        /// <summary>
        /// Ruft ein update mit den neuen Daten des AG auf
        /// </summary>
        /// <param name="p"></param>
        /// <returns>true oder false falls es fehlschlägt</returns>
         bool updatePurchaser(Purchaser p);
    }
}
