using Main.Engine;
        
namespace Main
{
    class TheGame
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tu kiedys bedzie gra");
            StartTheGame();
        }

        static void StartTheGame(bool test = false)
        {
            //TODO: UI prepare game window
            MenuContext menuContext = new MenuContext();
            menuContext.RunMenu();
        }
    }
}