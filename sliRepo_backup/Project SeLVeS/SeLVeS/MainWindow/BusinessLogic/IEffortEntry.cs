using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelvesSoftware.BusinessLogic
{
    public interface IEffortEntry{
        void DoEffortEntry(EffortEntry e);
        List<EffortEntry> GetEntriesByPa(PersonalAssistant pa, int month, int year);
        List<EffortEntry> GetEntriesByPurchaser(Purchaser p, int month, int year);
        EffortEntry GetEntry(PersonalAssistant pa, int month, int year, int day);
        void ModifyEntry(EffortEntry e);

    }
}
