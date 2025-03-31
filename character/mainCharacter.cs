using Main.Character;
using Main.Gear.Weapon;
using Main.Items;
using Main.Gear.Armor;
using System.Text.Json;
using Main.Gear.WeaponSerializer;
using System.Reflection.Metadata.Ecma335;

namespace Main.Character
{
    class MainCharacter : Character
    {
        //NOTE: use enum instead string? Do separate class?
        private Bagpack _bagpack;
        private WeaponMasteryStats _weaponMasteryStats;
        private Weapon _usedWeapon;
        private List<Weapon> _storedWeapons;
        private List<Armor> _usedArmor;
        private int _gold;
        private int _experience;

        public MainCharacter(string name, int level, CharacterStats stats, int gold,
            int experience, Bagpack bagpack, WeaponMasteryStats weaponMasteryStats,
            Weapon usedWeapon, List<Weapon> storedWeapons, List<Armor> usedArmor) 
            : base(name, level, stats)
        {
            _weaponMasteryStats = weaponMasteryStats;
            _bagpack = bagpack;
            _storedWeapons = storedWeapons;
            _usedArmor = usedArmor;
            UsedWeapon = usedWeapon;
            Gold = gold;
            _experience = experience;
        }

        public int Gold
        {
            get{ return _gold; }
            set{ _gold = value; }
        }

        public Weapon UsedWeapon
        {
            get{ return _usedWeapon; }
            set{ _usedWeapon = value; }
        }
    }
}