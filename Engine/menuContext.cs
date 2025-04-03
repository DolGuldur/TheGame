using Main.Engine;

namespace Main.Engine
{
    internal class MenuContext
    {
        //NOTE: rethink?
        private const string InfoText = "Wybierz Opcję:";
        private const string WrongOptionText = "Wybrano niewspieraną opcję, wybierz ponownie";
        private const string NotIplementedError = "Opcja nie została zaiplementowana";

        public static readonly Dictionary<MenuText, string> MenuTextDict = new Dictionary<MenuText, string>()
        {
            { MenuText.StartGame, "Wystartuj Nową Grę" },
            { MenuText.LoadGame, "Załaduj Grę" },
            { MenuText.Options, "Opcje" },
            { MenuText.CloseGame, "Wyłącz Grę"},
        };

        //NOTE: is constructor needed?
        public MenuContext() { ;}

        public bool RunMenu()
        {
            do
            {
                DisplayMenu();
            } while (MenuAction());
            return true;
        }

        public void DisplayMenu()
        {
            var values = Enum.GetValues(typeof(MenuText));

            Console.WriteLine(InfoText);
            for (int text = 0; text < values.Length; ++text)
            {
                MenuText menuOption = (MenuText)values.GetValue(text);
                string displayText = MenuTextDict[menuOption];
                Console.WriteLine($"{text + 1}. {displayText}");
            }
        }

        public bool MenuAction()
        {
            var keyInfo = Console.ReadKey();
            
            switch (keyInfo.Key) 
            {
                case ConsoleKey.D1:
                    StartNewGame newGame = new StartNewGame();
                    bool newGameRet = newGame.RunNewGame();
                    return newGameRet;
                case ConsoleKey.D2:
                    LoadGame loadGame = new LoadGame();
                    bool loadGameRet = loadGame.LoadGameMain();
                    Console.WriteLine(NotIplementedError);
                    return loadGameRet;
                case ConsoleKey.D3:
                    OptionsMenu optionsMenu = new OptionsMenu();
                    bool optionsMenuRet = optionsMenu.RunOptionsMenu();
                    Console.WriteLine(NotIplementedError);
                    return optionsMenuRet;
                case ConsoleKey.D4:
                    return true;
                default:
                    Console.WriteLine(WrongOptionText);
                    return false;
            }
        }
    }

    public enum MenuText
    {
        StartGame,
        LoadGame,
        Options,
        CloseGame
    }
}
