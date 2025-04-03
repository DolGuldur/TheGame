using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Main.Character;

namespace Main.Locations.Buildings
{
    internal class Home : Building
    {
        private const string TypeConst = "Dom rodzinny";
        private const string InfoText = "Wszedłeś do domu rodzinnego. Wybierz Opcję:";
        private const string InfoText2 = "Wybierz Opcję:";
        private const string WrongOptionText = "Wybrano niewspieraną opcję, wybierz ponownie";
        private const int HomeLevel = 1;

        public static readonly Dictionary<HomeOptions, string> HomeOptionsText =
            new Dictionary<HomeOptions, string>
            {
                { HomeOptions.EatFood, "Zjedz rodzinny obiad"},
                { HomeOptions.GetSomeSleep, "Prześpij się"},
                { HomeOptions.LeaveHome, "Wyjdź z domu"},
            };

        public Home(int level) : base(level)
        {
            Level = HomeLevel;
        }

        public override void GetIntoBuilding(MainCharacter mainCharacter)
        {
            Console.WriteLine(InfoText);
            do
            {
                DisplayOptions();
            } while (WaitForAction(mainCharacter));
        }
        //This funtion will propably look the same in all Building type classes - consider doing it
        //in the Building class body

        public override void DisplayOptions()
        {
            var values = Enum.GetValues(typeof(HomeOptions));

            for (int text = 0; text < values.Length; ++text)
            {
                HomeOptions option = (HomeOptions)values.GetValue(text);
                string displayText = HomeOptionsText[option];
                Console.WriteLine($"{text + 1}. {displayText}");
            }
        }

        public override bool WaitForAction(MainCharacter mainCharacter)
        {
            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.D1:
                    if (mainCharacter.RestoreFullHealth())
                    {
                        Console.WriteLine($"Nie ma to jak domowy obiad! Twój bohater odzyskał " +
                            $"wszystkie siły!");
                    }
                    Console.WriteLine(InfoText2);
                    return true;
                case ConsoleKey.D2:
                    if (mainCharacter.RestoreFullStamina())
                    {
                        Console.WriteLine("Twój bohater położył się do swojego łóżka. Odzyskał" +
                            "wszystkie siły!");
                    }
                    Console.WriteLine(InfoText2);
                    return true;
                case ConsoleKey.D3:
                    return false;
                default:
                    Console.WriteLine(WrongOptionText);
                    return true;
            }
        }
    }

    public enum HomeOptions
    {
        EatFood,
        GetSomeSleep,
        LeaveHome
    }
}
