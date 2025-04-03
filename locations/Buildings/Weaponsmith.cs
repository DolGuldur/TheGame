using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Locations.Buildings
{
    internal class Weaponsmith : Building
    {
        private const string TypeConst = "Kuźnia Oręża";

        public Weaponsmith()
        {
            Type = TypeConst;
        }
    }
}
