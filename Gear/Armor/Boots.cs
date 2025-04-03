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
        //I know public const is allocated to private Accessibility, but is needed for armory
        //Might change later
        public const int AccessConst = 4;
        private const int DurabilityConst = 30; //arbitrary TODO: balanceprivate const int CostConst = 200; //TODO: balance
        private const int CostConst = 60;

        public Boots(int level) : base(level)
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
