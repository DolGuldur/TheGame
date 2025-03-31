using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Boots : Armor
    {
        private const string TypeConst = "Buty";
        private const int AccessConst = 4;
        private const int DurabilityConst = 30; //arbitrary TODO: balanceprivate const int CostConst = 200; //TODO: balance
        private const int CostConst = 60;

        public Boots(int level) : base(level)
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
