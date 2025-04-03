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
        private Bagpack _bagpack;
        private WeaponMasteryStats _weaponMasteryStats;
        private Weapon _usedWeapon;
        //array instead of List? may be easier for choosing weapons, and will always have only
        //3 slots
        private List<Weapon> _storedWeapons;
        public List<Armor> _usedArmor; //Private but some functions to get this list?
        //maybe a class, but what with situation, when you dont have this part of armor?
        //maybe private, but with funtions to change it? Property?
        private int _gold;
        private int _experience;

        public MainCharacter(string name, int level, CharacterStats stats, int gold, int experience, 
            Bagpack bagpack, WeaponMasteryStats weaponMasteryStats, Weapon usedWeapon, 
            List<Weapon> storedWeapons, List<Armor> usedArmor) : base(name, level, stats)
        {
            _weaponMasteryStats = weaponMasteryStats;
            _bagpack = bagpack;
            _storedWeapons = storedWeapons;
            _usedArmor = usedArmor;
            UsedWeapon = usedWeapon;
            Gold = gold;
            _experience = experience;
        }

        public bool Heal(int healthAmount)
        {
            if (_currentStats.Health == _stats.Health)
            {
                Console.WriteLine("Twoje ¿ycie jest pe³ne");
                return false;
            }
            else
            {
                _currentStats.Health += healthAmount;
                if (_currentStats.Health > _stats.Health) 
                { 
                    _currentStats.Health = _stats.Health; 
                }
                return true;
            }
        }

        public bool RestoreFullHealth()
        {
            if (_currentStats.Health == _stats.Health)
            {
                Console.WriteLine("Twoje ¿ycie jest ju¿ pe³ne");
                return false;
            }
            else
            {
                _currentStats.Health = _stats.Health;
                return true;
            }
        }

        public bool RestoreStamina(int staminaAmount)
        {
            if (_currentStats.Stamina == _stats.Stamina)
            {
                Console.WriteLine("Twoja wytrzyma³oœæ jest pe³na");
                return false;
            }
            else
            {
                _currentStats.Stamina += staminaAmount;
                if (_currentStats.Stamina > _stats.Stamina)
                {
                    _currentStats.Stamina = _stats.Stamina;
                }
                return true;
            }
        }

        public bool RestoreFullStamina()
        {
            if (_currentStats.Stamina == _stats.Stamina)
            {
                Console.WriteLine("Twoja wytrzyma³oœæ jest ju¿ pe³na");
                return false;
            }
            else
            {
                _currentStats.Stamina = _stats.Stamina;
                return true;
            }
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