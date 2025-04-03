using Main.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Locations.Buildings
{
    internal class Tavern : Building
    {
        //TODO: consider seperate text class!
        private const string TypeConst = "Karczma";
        private const string DirectInfoText = "Wszedłeś do Karczmy. Wybierz Opcję:";
        private const int BasicCostOfFood = 20; //TODO: balance
        private const int BasicCostOfSleep = 200; //TODO: balance
        private const int BasicBenefitsOfFood = 100; //TODO: balance
        private const int BasicBenefitsOfSleep = 100; //TODO: balance

        private int _costOfSleep;
        private int _benefitsOfSleep;
        private int _costOfFood;
        private int _benefitOfFood;

        public static readonly Dictionary<TavernOptions, string> TavernOptionsText =
            new Dictionary<TavernOptions, string>
            {
                { TavernOptions.DisplayYourGold, "Wyświetl swoje złoto"},
                { TavernOptions.DisplayCostsAndBenefits, "Wyświetl koszta karczmy"},
                { TavernOptions.EatFood, "Zakup obiad" },
                { TavernOptions.GetSomeSleep, "Idź się przespać"},
                { TavernOptions.LeaveTavern, "Wyjdź z karczmy"},
            };

        public Tavern(int level) : base(level)
        {
            Type = TypeConst;
            CalculateCostsAndBenefits();
        }

        private void CalculateCostsAndBenefits()
        {
            CostOfFood = BasicCostOfFood;
            CostOfFood += (int)(BasicCostOfSleep * (Level / 10)); //TODO: balance
            BenefitsOfFood = BasicBenefitsOfFood;
            BenefitsOfFood += (int)(BasicBenefitsOfFood * (Level / 7)); //TODO: balance
            CostOfSleep = BasicCostOfSleep;
            CostOfSleep += (int)(BasicCostOfSleep * (Level / 10)); //TODO: balance
            BenefitOfSleep = BasicBenefitsOfSleep;
            BenefitOfSleep += (int)(BasicBenefitsOfSleep * (Level / 7)); //TODO: balance
        }

        public override void GetIntoBuilding(MainCharacter mainCharacter)
        {
            Console.WriteLine(DirectInfoText);
            do
            {
                DisplayOptions();
            } while (WaitForAction(mainCharacter));
            //return true; will be better for testing
        }

        //bool?
        private void DisplayCostsAndBenefits()
        {
            List<int> costsAndBenefits = GetCostsAndBenefits();
            Console.WriteLine($"Po posiłku za {costsAndBenefits[0]} złota zregenerujesz " +
                $"{costsAndBenefits[1]} życia");
            Console.WriteLine($"Po wyspaniu się za {costsAndBenefits[2]} złota zregenerujesz " +
                $"{costsAndBenefits[3]} wytrzymałości");
        }

        //bool?
        private void GetSomeFood(MainCharacter mainCharacter)
        {
            int foodAmount;
            Console.WriteLine("Jak dużo chciałbyś zjeść?");
            //check proper entry
            if (int.TryParse(Console.ReadLine(), out foodAmount)) 
            {
                int totalFoodCost = foodAmount * CostOfFood;
                int totalHealthGain = foodAmount * BenefitsOfFood;
                //check if char can afford that
                if(totalFoodCost <= mainCharacter.Gold)
                {
                    //check if char has full health
                    if (mainCharacter.Heal(totalHealthGain))
                    {
                        mainCharacter.Gold -= totalFoodCost;
                    }
                }
                else { Console.WriteLine(NotEnoughGoldText); }
            }
            else { Console.WriteLine("Niepoprawna liczba!"); }
        }

        private void GetSomeSleep(MainCharacter mainCharacter)
        {
            if(mainCharacter.Gold >= CostOfSleep)
            {
                if (mainCharacter.RestoreStamina(BenefitOfSleep))
                {
                    mainCharacter.Gold -= CostOfSleep;
                }
            }
            else { Console.WriteLine(NotEnoughGoldText); }
        }

        public override void DisplayOptions()
        {
            var values = Enum.GetValues(typeof(TavernOptions));

            for (int text = 0; text < values.Length; ++text)
            {
                TavernOptions option = (TavernOptions)values.GetValue(text);
                string displayText = TavernOptionsText[option];
                Console.WriteLine($"{text + 1}. {displayText}");
            }
        }

        public override bool WaitForAction(MainCharacter mainCharacter)
        {
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine($"Masz przy sobie {mainCharacter.Gold} złota");
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D2:
                    DisplayCostsAndBenefits();
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D3:
                    GetSomeFood(mainCharacter);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D4:
                    GetSomeSleep(mainCharacter);
                    Console.WriteLine(InfoText);
                    return true;
                case ConsoleKey.D5:
                    return false;
                default:
                    Console.WriteLine(WrongOptionText);
                    return true;
            }
        }

        /* order:
         * - cost of Food
         * - benefit of Food
         * - cost of Sleep
         * - benefit of Sleep
         * 
         * made to return List in order to save yourself 4 diffrent functions
         * will propably used to write down all at once, so it will be easier
         */
        public List<int> GetCostsAndBenefits()
        {
            List<int> costsAndBenefits = new List<int>();
            costsAndBenefits.Add(CostOfFood);
            costsAndBenefits.Add(BenefitsOfFood);
            costsAndBenefits.Add(CostOfSleep);
            costsAndBenefits.Add(BenefitOfSleep);
            return costsAndBenefits;
        }

        private int CostOfSleep
        {
            get { return _costOfSleep; }
            set { _costOfSleep = value; }
        }

        private int BenefitOfSleep
        {
            get { return _benefitsOfSleep; }
            set { _benefitsOfSleep = value; }
        }

        private int CostOfFood
        {
            get { return _costOfFood; }
            set { _costOfFood = value; }
        }

        private int BenefitsOfFood
        {
            get { return _benefitOfFood; }
            set { _benefitOfFood = value; }
        }
    }

    public enum TavernOptions
    {
        DisplayYourGold,
        DisplayCostsAndBenefits,
        EatFood,
        GetSomeSleep,
        LeaveTavern
    }
}
