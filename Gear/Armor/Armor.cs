using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Gear.Armor
{
    //NOTE: abstract?
    abstract class Armor
    {
        //TODO: Think about diffrent container?
        //maybe think about stick number to each armor type?
        private string _type;
        private int _level;
        private int _durability;
        private int _maxDurability;
        private int _accessibility;
        private int _cost;

        public Armor(int level) 
        {
            Level = level;
        }

        public void ArmorLevelUp() //bool?
        {
            Level++;
            UpdateMaxDurability();
            UpdateCost();
            Durability = GetMaxDurability();
        }

        public void RepairArmor() //Bool?
        {
            Durability = MaxDurability;
        }

        //takes damage to the armor and checks if part of armor will be destroyed
        //returns true if destroyed, false if it stays
        public bool GetDamageAndCheckIfDestroyed(int takenDamage)
        {
            if(takenDamage >= Durability)
            {
                return true;
            }
            else
            {
                Durability -= takenDamage;
                return false;
            }
        }

        //updates maxDurability according to level
        public abstract int UpdateMaxDurability(); //bool?

        //updates Cost according to level
        public abstract int UpdateCost();

        //Returns Type of armor
        public string GetArmorType() { return Type; }

        //returns accessibility - may be useless, because of accesConst being public - access need
        //to be accessible to get without creation of object - static? but will need to be moved to 
        //lower classes - need to reconsider it
        public int GetAccessibility() { return Accessibility; }

        //returns cost of armor according to level
        public int GetCost() { return Cost; }

        //returns MaxDurability according to level
        public int GetMaxDurability() { return MaxDurability; }

        //returns cost of upgreading armor to next level. At the begining, I thought, that this
        //Upgrade should be changed according to CostConst in order to avoid exponencial growth of
        //cost. But it might be better with this need to do balance - might change it later
        public int GetUpgradeCost()
        {
            int updateCost = (int)(Cost * (this.Level / 10)); //balance
            return updateCost;
        }

        public int GetRepairCost()
        {
            // arbitrary mechanics - needed balance
            int multiplier = (int)(Cost * (this.Level / 10));
            int repairCost = (int)((MaxDurability - Durability) * multiplier);
            return repairCost;
        }

        protected string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        protected int Accessibility 
        { 
            get { return _accessibility; } 
            set { _accessibility = value; }
        }

        protected int Cost 
        { 
            get { return _cost; }
            set { _cost = value; }
        }

        protected int MaxDurability
        {
            get { return _maxDurability; }
            set { _maxDurability = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Durability
        {
            get { return _durability; }
            set { _durability = value; }
        }
    }
}