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
        private const string TypeConst = "Warsztat Płatnerza";
        private const string DirectInfoText = "Wszedłeś do warsztatu płatnerza. Wybierz Opcję:";

        List<Armor> _availiableArmorList;

        public static Dictionary<ArmoryOptions, string> ArmoryOptionsText =
            new Dictionary<ArmoryOptions, string>
            {
                { ArmoryOptions.LookAtYourArmor, "Zobacz swoją aktualną zbroję"},
                { ArmoryOptions.RepairYourArmor, "Napraw swoją zbroję"},
                { ArmoryOptions.BuyNewArmor, "Zakup zbroję"},
                { ArmoryOptions.UpgradeArmor, "Ulepsz zbroję"},
                { ArmoryOptions.LeaveArmory, "Wyjdź z warsztatu płatnerza"},
            };

        public Armory(int level) : base(level)
        {
            Type = TypeConst;
            _availiableArmorList = PrepareAvailiableArmorList();
        }

        public override void GetIntoBuilding(MainCharacter mainCharacter)
        {
            Console.WriteLine(InfoText);
            do
            {
                DisplayOptions();
            } while (WaitForAction(mainCharacter));
        }

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

        public override bool WaitForAction(MainCharacter mainCharacter)
        {
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                //Option 1 may be unneccessary? you will se your armor in other options
                case ConsoleKey.D1:
                    Console.WriteLine("Twoja obecna zbroja:");
                    ShowArmor(mainCharacter._usedArmor, true);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D2:
                    RepairArmor(mainCharacter);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D3:
                    BuyNewArmor(mainCharacter);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D4:
                    UpgradeArmor(mainCharacter);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D5:
                    return false;
                default:
                    Console.WriteLine(WrongOptionText);
                    return true;
            }
        }

        private void ShowArmor(List<Armor> armorList, bool full)
        {
            int ArmorIndex = 1;
            if (full)
            {
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
            int armorIndex;
            // check proper entry
            if (!(int.TryParse(Console.ReadLine(), out armorIndex)) ||
                    (armorIndex > mainCharacter._usedArmor.Count))
            {
                Console.WriteLine(WrongOptionText);
                return false;
            }
            if (mainCharacter._usedArmor[armorIndex].Durability ==
                mainCharacter._usedArmor[armorIndex].GetMaxDurability())
            {
                Console.WriteLine("Ta część zbroi jest nienaruszona! Nie wymaga naprawy.");
                return false;
            }
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetRepairCost())
            {
                Console.WriteLine(NotEnoughGoldText);
                return false;
            }
            RepairArmorPiece(mainCharacter, armorIndex);
            Console.WriteLine("Twoja zbroja została naprawiona!");
            return true;
        }

        //Easier to do this in Armor class?
        private void RepairArmorPiece(MainCharacter mainCharacter, int armorIndex)
        {
            mainCharacter.Gold -= mainCharacter._usedArmor[armorIndex].GetRepairCost();
            mainCharacter._usedArmor[armorIndex].Durability = 
                mainCharacter._usedArmor[armorIndex].GetMaxDurability();
        }

        private bool BuyNewArmor(MainCharacter mainCharacter)
        {
            Console.WriteLine("Wybierz część którą chcesz kupić");
            //TODO: reconsider this string
            Console.WriteLine($"Dostępne części zbroi - ich poziom - {Level}:");
            ShowArmor(_availiableArmorList, false);
            int armorIndex;
            //Check if proper entry
            if (!(int.TryParse(Console.ReadLine(), out armorIndex)) 
                || armorIndex > _availiableArmorList.Count)
            {
                Console.WriteLine(WrongOptionText);
                return false;
            }
            //check if hero already have this piece of armor
            //Maybe also can check if hero have damaged or armor of lower level, so he can buy
            //better, might implement later
            if (CheckArmorPiece(mainCharacter._usedArmor, _availiableArmorList[armorIndex]))
            {
                Console.WriteLine("Już posiadasz tą część zbroi!");
                return false;
            }
            //check gold
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetCost())
            {
                Console.WriteLine(NotEnoughGoldText);
                return false;
            }
            Armor boughtArmor = GenerateArmor(_availiableArmorList[armorIndex].GetArmorType());
            if (boughtArmor == null)
            {
                Console.WriteLine("Coś poszło nie tak :(, błąd w generacji zbroi");
                return false;
            }
            mainCharacter.Gold -= boughtArmor.GetCost();
            mainCharacter._usedArmor.Add(boughtArmor);
            Console.WriteLine("Zakupiłeś nową zboję!");
            return true;
        }

        //will upgrade every armor level by 1 and will go to WaitForOption
        //upgrade to armory max level at once? but if somebody wont have enough gold for full
        //upgrade, but can afford singular upgrade, wont be able to upgrade to lover levels
        //might change it later
        private bool UpgradeArmor(MainCharacter mainCharacter) 
        {
            Console.WriteLine("Którą część chcesz ulepszyć?");
            int armorIndex;
            //check entry
            if (!(int.TryParse(Console.ReadLine(),out armorIndex)) 
                || armorIndex > mainCharacter._usedArmor.Count)
            {
                Console.WriteLine(WrongOptionText);
                return false;
            }
            //check gold
            if (mainCharacter.Gold < mainCharacter._usedArmor[armorIndex].GetUpgradeCost())
            {
                Console.WriteLine(NotEnoughGoldText);
                return false;
            }
            //check if armor can be upgraded according to level of armory
            if (mainCharacter._usedArmor[armorIndex].Level >= Level)
            {
                Console.WriteLine("Twoja część tej zbroi nie może być już ulepszana w tym " +
                    $"warsztacie! Poziom warsztatu - {Level}");
                return false;
            }
            //if everythink is good - upgrade it
            mainCharacter.Gold -= mainCharacter._usedArmor[armorIndex].GetUpgradeCost();
            mainCharacter._usedArmor[armorIndex].ArmorLevelUp();
            Console.WriteLine("Zboja pomyślnie ulepszona!");
            return true;
        }

        /*Function to check if you already have a piece of armor
         * return:
         *  - true - if you have this armor
         *  - false - if you dont have it
         *  
         *  reconsider returning number of armor on the list
         */
        private bool CheckArmorPiece(List<Armor> heroArmor, Armor shopArmor)
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

        //we cannot just add to heros armor object from _availiableArmorList, because the reference
        //might generates problems in the long run. Or can we?
        private Armor GenerateArmor(string armorType)
        {
            if (armorType == "Napierśnik") { return new Breastplate(Level); }
            if (armorType == "Buty") { return new Boots(Level); }
            if (armorType == "Kuszka") { return new Codpiece(Level); }
            if (armorType == "Udawice") { return new Cuisses(Level); }
            if (armorType == "Rękawice") { return new Gauntlets(Level); }
            if (armorType == "Nagolenniki") { return new Greaves(Level); }
            if (armorType == "Hełm") { return new Helm(Level); }
            if (armorType == "Naramienniki") { return new Pauldrons(Level); }
            if (armorType == "Karwasze") { return new Vambraces(Level); }
            return null;
        }

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
