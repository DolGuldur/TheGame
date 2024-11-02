using Main.Character;
using Main.Gear.Weapon;

namespace Main.Character
{
    class MainCharacter : Character
    {
        //NOTE: use enum instead string? Do separate class?
        private string _name;
        private Dictionary<string, int> _bagpack;
        private Weapon _weaponUsed;
        private List<Weapon> _storedWeapons;
        private int _gold;

        public MainCharacter(string name)
        {
            _name = name;
            _bagpack = new Dictionary<string, int>();
            _weaponUsed = new Weapon();
            _storedWeapons = new List<Weapon>();
            _gold = 50;
        }
    }
}