using Main.Engine;

namespace Main
{
    class TheGame
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tu kiedys bedzie gra");
            MainEngine mainEngine = new MainEngine();
            mainEngine.Run();
        }
    }
}