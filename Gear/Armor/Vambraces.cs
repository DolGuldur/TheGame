using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Vambraces : Armor
    {
        private const string TypeConst = "Karwasze";
        private const int AccessConst = 6;
        private const int DurabilityConst = 45; //arbitrary TODO: balance
        private const int CostConst = 90; //TODO: balance

        public Vambraces(int level) : base(level)
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
