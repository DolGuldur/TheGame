using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Locations.Buildings
{
    internal class Church : Building
    {
        private const string TypeConst = "Kościół";

        public Church() 
        {
            Type = TypeConst;
        }
    }
}
