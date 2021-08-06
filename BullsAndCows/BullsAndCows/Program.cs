using System;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight*10);
            Game game = new Game();
            StartMenu startMenu = new StartMenu();         
        }
    }

    


   

}

