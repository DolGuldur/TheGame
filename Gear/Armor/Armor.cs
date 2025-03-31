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
                //don't have to update durability because this part of armor will be destroyed
                //anyways
                return true;
            }
            else
            {
                Durability -= takenDamage;
                return false;
            }
        }

        public abstract void UpdateMaxDurability(); //bool?

        public string GetArmorType() { return Type; }

        public int GetAccessibility() { return Accessibility; }

        public int GetCost() { return Cost; }

        public int GetMaxDurability() { return MaxDurability; }

        public int GetUpdateCost()
        {
            int updateCost = (int)(Cost * (this.Level / 10)); //balance
            return updateCost;
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