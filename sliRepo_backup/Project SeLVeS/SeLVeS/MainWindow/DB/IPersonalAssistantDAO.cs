using System;
using System.Collections.Generic;
namespace SelvesSoftware.DB
{
    /// <summary>
    /// author: TS
    /// </summary>
    public interface IPersonalAssistantDAO
    {
        PersonalAssistant select(PersonalAssistant pa);
        PersonalAssistant insert(PersonalAssistant Pa);
        PersonalAssistant update(PersonalAssistant pa);
        List<PersonalAssistant> SelectAll();
        void insertEmployment(Employment e);
        void deleteEmployment(Employment e);
        void insertDocument(PersonalAssistant pa);
        void updateDocument(PersonalAssistant pa);
        void insertEmploymentStatus(Purchaser pur, PersonalAssistant pa);
        void deleteEmploymentStatus(Purchaser pur, PersonalAssistant pa);
        void selectPurchaserList(PersonalAssistant pa);
        List<PersonalAssistant> SelectSpecific(PersonalAssistant pa);

    }
}
