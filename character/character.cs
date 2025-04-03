using System.Xml.Linq;
using Main.Character;

namespace Main.Character
{
    abstract class Character
    {
        private string _name;
        private int _level;
        protected CharacterStats _stats;
        protected CharacterStats _currentStats;

        public Character(string name, int level, CharacterStats stats)
        {
            Name = name;
            Level = level;
            _stats = _currentStats = stats;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }
}