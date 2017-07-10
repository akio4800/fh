using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.BusinessLogic
{
    public interface IPersonalAssistentBl
    {
        /// <summary>
        /// Abfrage eines PA mit einer bestimmten id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>alle Daten des PA</returns>
        
         PersonalAssistant SelectPa(int id);
        /// <summary>
        /// fügt einen dienst hinzu
        /// </summary>
        /// <param name="e"></param>
        void insertEmployment(Employment e);

        /// <summary>
        /// löscht einen dienst 
        /// </summary>
        /// <param name="e"></param>
        void deleteEmployment(Employment e);

        /// <summary>
        /// Abrage aller PA
        /// </summary>
        /// <returns>Liste von PAs</returns>
         List<PersonalAssistant> SelectAllPa();

        /// <summary>
        /// Wählt einen PA mit bestimmten Parametern aus (für die Suche)
        /// </summary>
        /// <returns>Liste der PA</returns>
         List<PersonalAssistant> SelectSpecificPa(PersonalAssistant pa);

        /// <summary>
        /// legt einen PA an
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>true wenn es funktioniert hat sonst false</returns>
         bool CreatePa(PersonalAssistant pa);

        /// <summary>
        /// Ruft ein update mit den neuen Daten des PA auf
        /// </summary>
        /// <param name="pa"></param>
        /// <returns>true oder false falls es fehlschlägt</returns>
         bool UpdatePa(PersonalAssistant pa);

    }
}
