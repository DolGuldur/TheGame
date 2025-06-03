using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Main.Character;
using Main.Gear.Armor;

namespace Main.Locations.Buildings
{
    internal class Armory : Building
    {
        private List<Armor> _availiableArmorList;
        
        public Armory(int level) : base(level)
        {
            Type = TypeConst;
            _availiableArmorList = PrepareAvailiableArmorList();
        }

        /*******************************************************************************************
         * Main method for entering the building menu
         */
        //NOTE: consider returning bool
        public override void GetIntoBuilding(MainCharacter mainCharacter)
        {
            Console.WriteLine(DirectInfoText);
            do
            {
                Console.WriteLine(InfoText);
                DisplayOptions();
            } while (WaitForAction(mainCharacter));
        }

        /*******************************************************************************************
         * Method to read client option
         *  1 - show armor
         *  2 - repair armor
         *  3 - buy new armor
         *  4 - upgrade armor
         *  5 - leave
         */
        public override bool WaitForAction(MainCharacter mainCharacter)
        {
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                //Option 1 may be unneccessary? you will se your armor in other options
                case ConsoleKey.D1:
                    ShowArmor(mainCharacter._usedArmor, true);
                    return true;
                case ConsoleKey.D2:
                    RepairArmor(mainCharacter);
                    return true;
                case ConsoleKey.D3:
                    BuyNewArmor(mainCharacter);
                    return true;
                case ConsoleKey.D4:
                    UpgradeArmor(mainCharacter);
                    return true;
                case ConsoleKey.D5:
                    return false;
                default:
                    Console.WriteLine(WrongOptionText);
                    return true;
            }
        }

        /*******************************************************************************************
         * Mechanic for Reparing Armor
         * 
         * returns:
         *      1 = wrong key
         *      2 = Armor piece is not damaged
         *      3 = not enough money
         *      0 = proper repair
         */
        private static int RepairArmorMech(MainCharacter mainCharacter)
        {
            if (!(int.TryParse(Console.ReadLine(), out int armorIndex)) ||
                    (armorIndex > mainCharacter._usedArmor.Count))
            {
                return 1;
            }
            if (mainCharacter._usedArmor[armorIndex].Durability ==
                mainCharacter._usedArmor[armorIndex].GetMaxDurability())
            {
                return 2;
            }
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetRepairCost())
            {
                return 3;
            }
            else
            {
                mainCharacter.Gold -= mainCharacter._usedArmor[armorIndex].GetRepairCost();
                mainCharacter._usedArmor[armorIndex].RepairArmor();
                return 0;
            }
        }

        /*******************************************************************************************
         * Mechanic for buying armor
         * 
         * returns:
         *      0 - proper usage
         *      1 - wrong option
         *      2 - hero already have this piece of armor
         *          Maybe also can check if hero have damaged or armor of lower level, so he can buy
         *          better, might implement later
         *      3 - not enough gold
         *      4 - generating armor error
         */
        private int BuyNewArmorMech(MainCharacter mainCharacter)
        {
            if (!(int.TryParse(Console.ReadLine(), out int armorIndex))
                || armorIndex > _availiableArmorList.Count)
            {
                return 1;
            }
            if (CheckArmorPiece(mainCharacter._usedArmor, _availiableArmorList[armorIndex]))
            {
                return 2;
            }
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetCost())
            {
                
                return 3;
            }
            else
            {
                Armor? boughtArmor = GenerateArmor(_availiableArmorList[armorIndex].GetArmorType());
                if (boughtArmor == null)
                {
                    return 4;
                }
                mainCharacter.Gold -= boughtArmor.GetCost();
                mainCharacter._usedArmor.Add(boughtArmor);
                return 0;
            }
        }

        /*******************************************************************************************
         * Mechanic for upgrading armor
         * 
         * returns:
         *      0 - proper upgrade
         *      1 - wrong option
         *      2 - not enough gold
         *      3 - too weak armory
         *      4 - armor is damaged
         *
         * will upgrade every armor level by 1 and will go to WaitForOption
         * upgrade to armory max level at once? but if somebody wont have enough gold for full
         * upgrade, but can afford singular upgrade, wont be able to upgrade to lover levels
         * might change it later
         * write all possible upgrates and ask for number of upgrades?
         */
        private int UpgradeArmorMech(MainCharacter mainCharacter)
        {
            if (!(int.TryParse(Console.ReadLine(), out int armorIndex))
                || armorIndex > mainCharacter._usedArmor.Count)
            {
                return 1;
            }
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetUpgradeCost())
            {
                return 2;
            }
            if (mainCharacter._usedArmor[armorIndex].Level >= Level)
            {
                return 3;
            }
            if (mainCharacter._usedArmor[armorIndex].Durability !=
                mainCharacter._usedArmor[armorIndex].GetMaxDurability())
            {
                return 4;
            }
            else
            {
                mainCharacter.Gold -= mainCharacter._usedArmor[armorIndex].GetUpgradeCost();
                mainCharacter._usedArmor[armorIndex].ArmorLevelUp();
                return 0;
            }
        }

        /*******************************************************************************************
         * method to check if you already have a piece of armor
         * 
         * return:
         *  - true - if you have this armor
         *  - false - if you dont have it
         *  
         *  reconsider returning number of armor on the list
         */
        private static bool CheckArmorPiece(List<Armor> heroArmor, Armor shopArmor)
        {
            foreach (Armor armorPiece in heroArmor)
            {
                if (shopArmor.GetArmorType() == armorPiece.GetArmorType())
                {
                    return true;
                }
            }
            return false;
        }

        /*******************************************************************************************
         * Generate new armor type object
         * return:
         *  new armor type object due to provided string
         *  null if wrong string
         *  
         * we cannot just add to heros armor object from _availiableArmorList, because the reference
         * might generates problems in the long run. Or can we?
         */
        private Armor? GenerateArmor(string armorType) =>
            armorType switch
            {
                "Napierśnik" => new Breastplate(Level),
                "Buty" => new Boots(Level),
                "Kuszka" => new Codpiece(Level),
                "Udawice" => new Cuisses(Level),
                "Rękawice" => new Gauntlets(Level),
                "Nagolenniki" => new Greaves(Level),
                "Hełm" => new Helm(Level),
                "Naramienniki" => new Pauldrons(Level),
                "Karwasze" => new Vambraces(Level),
                _ => null
            };

        //Full List? 
        private List<Armor> PrepareAvailiableArmorList()
        {
            List<Armor> armorList = new List<Armor>();
            if (Level >= Breastplate.AccessConst)
            {
                Breastplate breastplate = new Breastplate(Level);
                armorList.Add(breastplate);
            }
            if (Level >= Helm.AccessConst)
            {
                Helm helm = new Helm(Level);
                armorList.Add(helm);
            }
            if (Level >= Codpiece.AccessConst) 
            {
                Codpiece codpiece = new Codpiece(Level);
                armorList.Add(codpiece);
            }
            if (Level >= Boots.AccessConst)
            {
                Boots boots = new Boots(Level);
                armorList.Add(boots);
            }
            if (Level >= Pauldrons.AccessConst) 
            { 
                Pauldrons pauldrons = new Pauldrons(Level);
                armorList.Add(pauldrons);
            }
            if (Level >= Greaves.AccessConst)
            {
                Greaves greaves = new Greaves(Level);
                armorList.Add(greaves);
            }
            if (Level >= Vambraces.AccessConst)
            {
                Vambraces vambraces = new Vambraces(Level);
                armorList.Add(vambraces);
            }
            if (Level >= Gauntlets.AccessConst)
            {
                Gauntlets gauntlets = new Gauntlets(Level);
                armorList.Add(gauntlets);
            }
            return armorList;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        ///Diplay stuff
        /// 

        private const string TypeConst = "Warsztat Płatnerza";
        private const string DirectInfoText = "Wszedłeś do warsztatu płatnerza";
        
        public static Dictionary<ArmoryOptions, string> ArmoryOptionsText =
            new Dictionary<ArmoryOptions, string>
            {
                { ArmoryOptions.LookAtYourArmor, "Zobacz swoją aktualną zbroję"},
                { ArmoryOptions.RepairYourArmor, "Napraw swoją zbroję"},
                { ArmoryOptions.BuyNewArmor, "Zakup zbroję"},
                { ArmoryOptions.UpgradeArmor, "Ulepsz zbroję"},
                { ArmoryOptions.LeaveArmory, "Wyjdź z warsztatu płatnerza"},
            };

        public override void DisplayOptions()
        {
            var values = Enum.GetValues(typeof(ArmoryOptions));

            for (int text = 0; text < values.Length; ++text)
            {
                ArmoryOptions option = (ArmoryOptions)values.GetValue(text);
                string displayText = ArmoryOptionsText[option];
                Console.WriteLine($"{text + 1}. {displayText}");
            }
        }

        private void ShowArmor(List<Armor> armorList, bool full)
        {
            int ArmorIndex = 1;

            if (full)
            {
                Console.WriteLine("Twoja obecna zbroja:");
                Console.WriteLine("[Lp].[Nazwa] - [Poziom] - [Wytrzymałość Obecna/Maksymalna]");
                foreach (Armor armorPiece in armorList)
                {
                    //think about having fixed intervals 
                    Console.WriteLine($"{ArmorIndex}.{armorPiece.GetArmorType()} - {armorPiece.Level}" +
                        $" - {armorPiece.Durability}/{armorPiece.GetMaxDurability()}");
                }
            }
            else
            {
                Console.WriteLine("[Lp].[Nazwa] - [Poziom]");
                foreach (Armor armorPiece in armorList)
                {
                    //think about having fixed intervals 
                    Console.WriteLine($"{ArmorIndex}.{armorPiece.GetArmorType()} - " +
                        $"{armorPiece.Level}");
                }
            }
        }

        private bool RepairArmor(MainCharacter mainCharacter)
        {
            Console.WriteLine("Co chciałbyś naprawić?");
            ShowArmor(mainCharacter._usedArmor, true);
            int mechResult = RepairArmorMech(mainCharacter);
            switch (mechResult)
            {
                case 0:
                    Console.WriteLine("Twoja zbroja została naprawiona!");
                    return true;
                case 1:
                    Console.WriteLine(WrongOptionText);
                    return false;
                case 2:
                    Console.WriteLine("Ta część zbroi jest nienaruszona! Nie wymaga naprawy.");
                    return false;
                case 3:
                    Console.WriteLine(NotEnoughGoldText);
                    return false;
                default:
                    Console.WriteLine("RepairArmor method error");
                    return false;
            }
        }

        private bool BuyNewArmor(MainCharacter mainCharacter)
        {
            Console.WriteLine("Wybierz część którą chcesz kupić");
            ShowArmor(_availiableArmorList, false);
            int mechResult = BuyNewArmorMech(mainCharacter);
            switch (mechResult)
            {
                case 0:
                    Console.WriteLine("Zakupiłeś nową zboję!");
                    return true;
                case 1:
                    Console.WriteLine(WrongOptionText);
                    return false;
                case 2:
                    Console.WriteLine("Już posiadasz tą część zbroi!");
                    return false;
                case 3:
                    Console.WriteLine(NotEnoughGoldText);
                    return false;
                default:
                    Console.WriteLine("BuyNewArmor method error");
                    return false;
            }
        }

        private bool UpgradeArmor(MainCharacter mainCharacter)
        {
            Console.WriteLine("Którą część chcesz ulepszyć?");
            ShowArmor(mainCharacter._usedArmor, false);
            int result = UpgradeArmorMech(mainCharacter);
            switch (result)
            {
                case 0:
                    Console.WriteLine("Zboja pomyślnie ulepszona!");
                    return true;
                case 1:
                    Console.WriteLine(WrongOptionText);
                    return false;
                case 2:
                    Console.WriteLine(NotEnoughGoldText);
                    return false;
                case 3:
                    Console.WriteLine("Twoja część tej zbroi nie może być już ulepszana w tym " +
                        $"warsztacie! Poziom warsztatu - {Level}");
                    return false;
                case 4:
                    Console.WriteLine("Ta zbroja jest uszkodzona! Napraw ją najpierw!");
                    return false;
                default:
                    Console.WriteLine("Upgrade Armor error");
                    return false;
            }
        }
    }

    public enum ArmoryOptions
    {
        LookAtYourArmor,
        RepairYourArmor,
        BuyNewArmor,
        UpgradeArmor,
        LeaveArmory
    }
}
