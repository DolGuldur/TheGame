using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Main.Character;

namespace Main.Locations.Buildings
{
    internal abstract class Building
    {
        protected const string InfoText = "Wybierz Opcję:";
        protected const string WrongOptionText = "Wybrano niewspieraną opcję, wybierz ponownie";
        protected const string NotEnoughGoldText = "Nie masz tyle złota biedaku";
        private string _type;
        private int _level;
        private int _accessibility;

        public Building(int level) 
        {
            Level = level;
        }

        //Is taking a copy of MainCharacter and return changed character a good option?
        public abstract void GetIntoBuilding(MainCharacter mainCharacter); //bool?

        public abstract void DisplayOptions();

        public abstract bool WaitForAction(MainCharacter mainCharacter);

        public int GetAccessibility() { return Accessibility; }

        public string GetBuildingType() { return Type; }

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

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }
}
