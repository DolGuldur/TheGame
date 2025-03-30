using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.character
{
    internal class WeaponMasteryStats
    {
        private int _axeMastery;
        private int _spearMastery;
        private int _swordMastery;

        public WeaponMasteryStats(int axeMastery, int spearMastery, int swordMastery) 
        {
            AxeMastery = axeMastery;
            SpearMastery = spearMastery;
            SwordMastery = swordMastery;
        }

        public int AxeMastery
        {
            get { return _axeMastery; }
            set { _axeMastery = value; }
        }

        public int SpearMastery
        {
            get { return _spearMastery; }
            set { _spearMastery = value; }
        }

        public int SwordMastery
        {
            get { return _swordMastery; }
            set { _swordMastery = value; }
        }
    }
}
