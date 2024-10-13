namespace weapon
{
    class Weapon
    {
        private string _name;
        private int _rarity;
        private int _damage;
        private int _level;
        private int _durability;
        private int _currentCondition;
        private bool _twoHanded;

        public static Weapon(string name, int rarity, int damage, int level, int durability, bool twoHanded)
        {
            _name = name;
            _rarity = rarity;
            _damage = damage;
            _level = level;
            _durability = durability;
            _currentCondition = durability;
            _twoHanded = twoHanded;
        }
    }
}