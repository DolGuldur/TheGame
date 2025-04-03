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
        //I know public const is allocated to private Accessibility, but is needed for armory
        //Might change later
        public const int AccessConst = 2;
        private const int DurabilityConst = 100; //arbitrary TODO: balance
        private const int CostConst = 200; //TODO: balance

        public Breastplate(int level) : base(level) 
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
