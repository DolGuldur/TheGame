using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Gauntlets : Armor
    {
        private const string TypeConst = "Rękawice";
        private const int AccessConst = 7;
        private const int DurabilityConst = 25; //arbitrary TODO: balance
        private const int CostConst = 50; //TODO: balance

        public Gauntlets(int level) : base(level)
        {
            Type = TypeConst;
            Accessibility = AccessConst;
            Cost = CostConst;
            UpdateMaxDurability();
            Durability = MaxDurability;
        }

        public override void UpdateMaxDurability()
        {
            //TODO: balance
            MaxDurability = DurabilityConst + (int)(DurabilityConst * (this.Level / 10));
        }
    }
}
