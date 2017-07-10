using SelvesSoftware.DataContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SelvesSoftware.BusinessLogic
{
    public interface IPurchaserDataDAO
    {
        PurchaserData Insert(PurchaserData purData);
        PurchaserData Select(PurchaserData pur);
        List<PurchaserData> SelectAll();
        bool Update(PurchaserData p);
        void changeGlobals(decimal stundensatz, decimal stundensatzAusz,  decimal fahrtkostenzusatzKM);
        void UpdateGlobals();


        /// <summary>
        /// insert only table AuftraggeberDaen
        /// </summary>
        /// <param name="purData"></param>
        /// <returns></returns>
        PurchaserData InsertPurData(PurchaserData purData);

        /// <summary>
        /// update only table AuftraggeberDaen
        /// </summary>
        /// <param name="purData"></param>
        /// <returns></returns>
        bool UpdatePurData(PurchaserData purData);
    }
}
