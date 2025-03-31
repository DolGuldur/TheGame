using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Pauldrons : Armor
    {
        private const string TypeConst = "Naramienniki";
        private const int AccessConst = 4;
        private const int DurabilityConst = 55; //arbitrary TODO: balance
        private const int CostConst = 110; //TODO: balance

        public Pauldrons(int level) : base(level)
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
