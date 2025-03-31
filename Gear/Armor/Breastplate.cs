using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Breastplate : Armor
    {
        private const string TypeConst = "Napierśnik";
        private const int AccessConst = 2;
        private const int DurabilityConst = 100; //arbitrary TODO: balance
        private const int CostConst = 200; //TODO: balance

        public Breastplate(int level) : base(level) 
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
