using Main.Character;
using System.Security.Cryptography;

namespace Main.Engine
{
    internal class StartNewGame
    {
        private MainCharacter _mainCharacter;
        private const string ChooseNameText = "Wybierz imię dla swojej postaci: ";
        //TODO: invent some heroic story and opening
        private const string NewGameContext = "Some heroic text...";

        public StartNewGame() {; }

        public bool RunNewGame() 
        {
            if (!NameSelector()) 
            {
                return false;
            }
            DisplayNewGameContext();
            return true;
        }

        public bool NameSelector()
        {
            string characterName;

            Console.WriteLine(ChooseNameText);
            characterName = Console.ReadLine();
            _mainCharacter = new MainCharacter(characterName);

        }
        
        public void DisplayNewGameContext() 
        {
            //NOTE: it may looks to simple for separate funtion, but 
            //opening text will be long, so it may require some mechanics
            Console.WriteLine(NewGameContext);
        }
    }
}
