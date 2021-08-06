using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    class StartMenu
    {
        
        const int NEWGAME = 0;// Key for new game option

        
        const int CODE_LENGTH = 1;// Key for change code length option
        const int MIN_CODE_LENGTH = 3;
        const int MAX_CODE_LENGTH = 10;


        const int GUESSES = 2;// Key for change number of guesses option
        const int MIN_GUESSES = 1;
        

        int position = 0;// Key for current option/cursor position
        int codeLength = 4;// current code length
        int guesses = 20;// current maximum guesses until game over

        ConsoleKey key;// The last key pressed

        public StartMenu() {

            // Redraw menu after each input
            // Start new game if input is enter key when new game is selected
            while (!(key == ConsoleKey.Enter && position == NEWGAME)) {
                Draw();
                ManageInput();
            }
            NewGame();

        }

        void Draw() 
        {
            // Draw Title
            Write.EmptyLine();
            Write.EmptyLine();
            string[][] TitleASCII = new string[][] { ASCII.bulls, ASCII.and, ASCII.cows };
            ConsoleColor[] TitleColors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Cyan };
            ASCII.PrintCenter(TitleASCII, TitleColors);
            Write.EmptyLine();
            Write.EmptyLine();

            // Draw new game option
            DrawOption("Start", NEWGAME);
            Write.EmptyLine();

            // Draw change code length option
            DrawOption("Code Length", CODE_LENGTH);
            DrawOption("<" + Write.SetLength(""+codeLength,5) + ">", CODE_LENGTH);
            Write.EmptyLine();
            
            // Draw change maximum guesses option
            DrawOption("Guesses", GUESSES);
            DrawOption("<" + Write.SetLength(""+guesses,5) + ">", GUESSES);
            Write.EmptyLine();
        }

        // Draw option with center alignment and highlight current option selected
        void DrawOption(string s, int p) {
            if (position == p)
                Write.Highlight();

            Write.Center(s);
            Console.ResetColor();
        }

        // React to player direction input 
        void ManageInput() 
        {
            key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    {
                        Up();
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {
                        Down();
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        Left();
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        Right();
                        break;
                    }

            }
            Console.SetCursorPosition(0, 0);
        }

        // Change option selected up or down. Wrap around if at top or bottom
        void Up() 
        {
            position--;
            if (position < 0)
                position = 2;
        }
        void Down() 
        {
            position++;
            if (position >2)
                position = 0;
        }

        // If at either change code length or change maximum guesses, 
        // increase the value for right arrow pressed and 
        // decrease the value for left arrow pressed
        void Left()
        {
            switch (position)
            {
                case CODE_LENGTH: 
                    {
                        codeLength--;
                        if (codeLength < MIN_CODE_LENGTH)
                            codeLength = 10;
                        
                        break;
                    }
                case GUESSES:
                    {
                        guesses--;
                        guesses = Math.Max(guesses, MIN_GUESSES);
                        break;
                    }
            }
        }
        void Right()
        {
            switch (position)
            {
                case CODE_LENGTH:
                    {
                        codeLength++;
                        if (codeLength > MAX_CODE_LENGTH)
                            codeLength = 3;
                        
                        break;
                    }
                case GUESSES:
                    {
                        guesses++;
                        break;
                    }
            }
        }

        // Start new game with options selected
        void NewGame()
        {
            Game.instance.Start(codeLength,guesses);
        }     
    }
}
