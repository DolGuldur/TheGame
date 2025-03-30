using Main.Character;
using System.Security.Cryptography;
using Main.Locations;
using Main.Gear.Weapon;
using Main.Gear.WeaponSerializer;
using Main.Items;
using System.Text.Json;
using Main.character;
using Main.Items.Potions;

namespace Main.Engine
{
    internal class StartNewGame
    {
        private MainCharacter _mainCharacter;
        private CharacterStats _characterStats;
        private const string ChooseNameText = "Wybierz imię dla swojej postaci: ";
        private const string WrongNameError = "Błędne imię postaci! Spróbuj ponownie";
        //TODO: invent some heroic story and opening, and will propably wont be here
        private const string NewGameContext = "Some heroic text...";
        private const int StartingLevel = 1;
        private const int StartingExp = 0;
        private string _characterName;
        private const int StartingGold = 50;
        private Weapon _startingWeapon;
        private List<Weapon> _storedWeapons;
        private Bagpack _startingBagpack;
        private WeaponMasteryStats _startWeaponMasteryStats;
        private static readonly string jsonFilePath = "../Gear/Weapon/weaponData.json";

        private static readonly Dictionary<string, int> StartingStats = new Dictionary<string, int>
        {
        //TODO: balance
            { "Health", 100 },
            { "Attack", 100 },
            { "Stamina", 100 },
            { "Agility", 3 },
            { "Defence", 20 },
            { "SwordMastery", 1 },
            { "AxeMastery", 1 },
            { "SpearMastery", 1 }
        };

        public StartNewGame() {; }

        public bool RunNewGame() 
        {
            if (!NameSelector()) 
            {
                System.Console.WriteLine(WrongNameError);
                return false;
            }
            if (!CreateNewMainCharackter())
            { 
                return false; 
            }
            DisplayNewGameContext();
            StartLocation.EnterTheLocation();
            return true;
        }

        public bool NameSelector()
        {
            Console.WriteLine(ChooseNameText);
            _characterName = Console.ReadLine();
            return !string.IsNullOrWhiteSpace(_characterName);
        }

        public bool CreateNewMainCharackter()
        {
            _startingWeapon = PrepareStartingWeapon();
            _storedWeapons = new List<Weapon>();
            _characterStats = PrepareStats();
            List<Potion> potions = new List<Potion>();
            _startingBagpack = new Bagpack(potions);
            _startWeaponMasteryStats = PrepareMasteryStats();
            _mainCharacter = new MainCharacter(_characterName, StartingLevel, _characterStats, 
                StartingGold, StartingExp, _startingBagpack, _startWeaponMasteryStats, _startingWeapon,
                _storedWeapons);
            return true;
        }

        private CharacterStats PrepareStats()
        {
            CharacterStats mainCharStats = new CharacterStats(StartingStats["Health"], 
                StartingStats["Attack"], StartingStats["Stamina"], StartingStats["Agility"],
                StartingStats["Defence"]);
            return mainCharStats;
        }

        private Weapon PrepareStartingWeapon()
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            WeaponDataSerializer weaponData = JsonSerializer.Deserialize<WeaponDataSerializer>(jsonData);
            var firstWeapon = weaponData.StartingWeapon[0];
            Weapon startingWeapon = new Sword(firstWeapon.Name, firstWeapon.Rarity, firstWeapon.Damage,
                1, firstWeapon.Durability, false);
            return startingWeapon;
        }

        private WeaponMasteryStats PrepareMasteryStats()
        {
            WeaponMasteryStats weapMastStats = new WeaponMasteryStats(StartingStats["AxeMastery"],
                StartingStats["SpearMastery"], StartingStats["SwordMastery"]);
            return weapMastStats;
        }

        public void DisplayNewGameContext() 
        {
            //NOTE: it may looks to simple for separate funtion, but 
            //opening text will be long, so it may require some mechanics
            Console.WriteLine(NewGameContext);
        }
    }
}
