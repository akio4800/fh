using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware.GUI.Personenverwaltung
{
    public class Track 
    {
        private String _fn;
        private String _ln;
        private long _id;

        public String FirstName
        {
            get { return _fn; }
            set { _fn = value; }
        }
        public String LastName
        {
            get { return _ln; }
            set { _ln = value; }
        }
        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public String ToString()
        {
            return Name;
        }

        public Track(long id, String firstname, String lastname)
        {
            FirstName = firstname;
            LastName = lastname;
            Id = id;
        }

        public Track()
        {
        }
    }
}
