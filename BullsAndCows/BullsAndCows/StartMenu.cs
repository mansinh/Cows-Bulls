using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    class StartMenu
    {
        const int NEWGAME = 0;

        const int CODE_LENGTH = 1;
        const int MIN_CODE_LENGTH = 3;
        const int MAX_CODE_LENGTH = 10;

        const int GUESSES = 2;
        const int MIN_GUESSES = 1;
        

        int position = 0;
        int codeLength = 4;
        int guesses = 20;
        ConsoleKey key;

        public StartMenu() {
            

            while (!(key == ConsoleKey.Enter && position == 0)) {
                Draw();
                ManageInput();
            }
            NewGame();

        }

        void Draw() 
        {
            Write.EmptyLine();
            Write.EmptyLine();
            string[][] TitleASCII = new string[][] { ASCII.bulls, ASCII.and, ASCII.cows };
            ConsoleColor[] TitleColors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.White, ConsoleColor.Cyan };
            ASCII.PrintCenter(TitleASCII, TitleColors);
            Write.EmptyLine();
            Write.EmptyLine();

            DrawOption("Start", NEWGAME);
            Write.EmptyLine();

            DrawOption("Code Length", CODE_LENGTH);
            DrawOption("<" + Write.SetLength(""+codeLength,5) + ">", CODE_LENGTH);
            Write.EmptyLine();
            
            DrawOption("Guesses", GUESSES);
            DrawOption("<" + Write.SetLength(""+guesses,5) + ">", GUESSES);
            Write.EmptyLine();
        }

        void DrawOption(string s, int p) {
            if (position == p)
                Write.Highlight();

            Write.Center(s);
            Console.ResetColor();
        }

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

        void NewGame()
        {
            Game.instance.Start(codeLength,guesses);
        }

        
    }


}
