using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Codpiece : Armor
    {
        private const string TypeConst = "Kuszka";
        private const int AccessConst = 3;
        private const int DurabilityConst = 40; //arbitrary TODO: balance
        private const int CostConst = 80; //TODO: balance

        public Codpiece(int level) : base(level)
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
