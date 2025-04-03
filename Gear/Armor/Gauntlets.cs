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
        //I know public const is allocated to private Accessibility, but is needed for armory
        //Might change later
        public const int AccessConst = 7;
        private const int DurabilityConst = 25; //arbitrary TODO: balance
        private const int CostConst = 50; //TODO: balance

        public Gauntlets(int level) : base(level)
        {
            Type = TypeConst;
            Accessibility = AccessConst;
            Cost = UpdateCost();
            MaxDurability = UpdateMaxDurability();
            Durability = MaxDurability;
        }

        public override int UpdateMaxDurability()
        {
            //TODO: balance
            return DurabilityConst + (int)(DurabilityConst * (this.Level / 10));
        }

        public override int UpdateCost()
        {
            return CostConst + (int)(CostConst * (this.Level / 5)); //TODO: balance
        }
    }
}
