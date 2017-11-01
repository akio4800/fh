using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b1 {
    public class Cell {

        public bool IsAlive { get; set; }

        public Cell( bool isAlive) {
            this.IsAlive = isAlive;
        }

    }
}
