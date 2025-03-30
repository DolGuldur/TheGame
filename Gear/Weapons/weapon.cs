namespace Main.Gear.Weapon
{
    abstract class Weapon
    {
        private string _name;
        private string _rarity;
        private int _damage;
        private int _level;
        private int _durability;
        private int _currentCondition;
        private bool _twoHanded;
        private int _excellence;
        private int agilityBoost;

        public static readonly Dictionary<ExcellenceTypes, string> ExcellenceTypesDict = new Dictionary<ExcellenceTypes, string>
        {
            { ExcellenceTypes.Tragic, "Beznadziejny" },
            { ExcellenceTypes.Bad, "Z³y" },
            { ExcellenceTypes.Poor, "Kiepski" },
            { ExcellenceTypes.Average, "Przeciêtny" },
            { ExcellenceTypes.Solid, "Solidny" },
            { ExcellenceTypes.Good, "Dobry" },
            { ExcellenceTypes.Perfect, "Wyœmienity" },
        };

        public Weapon(string name, string rarity, int damage, int level, int durability, bool twoHanded)
        {
            _name = name;
            _rarity = rarity;
            _damage = damage;
            _level = level;
            _durability = durability;
            _currentCondition = durability;
            _twoHanded = twoHanded;
            _excellence = DrawExcellence();
        }

        public int DrawExcellence()
        {
            Random random = new Random();
            return (int)(random.Next(-3, 4));
        }

        public enum ExcellenceTypes
        {
            Tragic = -3,
            Bad,
            Poor,
            Average,
            Solid,
            Good,
            Perfect
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Rarity
        {
            get { return _rarity; }
            set { _rarity = value; }
        }

        public int Damage
        {
            get { return _damage; }
            set { _damage = value; }
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