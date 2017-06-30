using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Trove_Stats.model
{
    class EmpGem : IEquatable<EmpGem>
    {

        public EmpGem() {
            First = new Stat();
            Second = new Stat();
            Third = new Stat();
        }
        public EmpGem(string gemType, string gemAbility, int pr, Stat first, Stat second, Stat third)
        {
            GemType = gemType;
            GemAbility = gemAbility;
            PR = pr;
            First = first;
            Second = second;
            Third = third;
        }

        public String GemType { get; set; }

        public String GemAbility { get; set; }

        public int Lvl { get; set; }

        public int PR { get; set; }

        public Stat First { get; set; }
        public Stat Second {get; set; }
        public Stat Third { get; set; }

        public bool Equals(EmpGem other)
        {
            return First == other.First && Second == other.Second && ;
        }
    }
}
