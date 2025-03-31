using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    internal class Cuisses : Armor
    {
        private const string TypeConst = "Udawice";
        private const int AccessConst = 5;
        private const int DurabilityConst = 50; //arbitrary TODO: balance
        private const int CostConst = 100; //TODO: balance

        public Cuisses(int level) : base(level)
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
