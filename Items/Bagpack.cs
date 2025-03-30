using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Items.Potions;

namespace Main.Items
{
    internal class Bagpack
    {
        private List<Potion> _potions;
        private Map _map; //think about loading uncovered parts of map - json?

        public Bagpack(List<Potion> potions) 
        {
            _potions = potions;
        }
    }
}
