using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Helm : Armor
    {
        private const string TypeConst = "Hełm";
        private const int AccessConst = 3;
        private const int DurabilityConst = 60; //arbitrary TODO: balance
        private const int CostConst = 120; //TODO: balance

        public Helm(int level) : base(level)
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
