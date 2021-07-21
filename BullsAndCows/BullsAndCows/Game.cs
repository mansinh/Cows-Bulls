using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    class Game
    {
        public static Game instance;
        bool isRunning;
        bool showCode = false;
        Code code;
        int turn = 0;
        int guesses = 10;
        int codeLength = 4;

        public Game()
        {
            if (instance == null)
            {
                instance = this;

            }
            else
            {
                Console.WriteLine("An instance of Game already exists");
            }
        }

        public void Start(int codeLength, int guesses)
        {
            this.codeLength = codeLength;
            this.guesses = guesses;


            code = new Code();
            NewGame();

        }

        void NewGame()
        {
            Write.ClearAll();
            isRunning = true;
            code.GenerateCode(codeLength);

            turn = 0;

            DrawTopPanel();

            Write.HorizontalDivider();
            Write.Center(" Guess the secret " + codeLength + " digit code within " + guesses + " turns");
            Write.HorizontalDivider();
         
            Write.TableRow(new string[] { "#", "GUESS", "BULLS", "COWS" }, 4);
            Write.EmptyLine();

            Update();
        }

        void DrawTopPanel() 
        {
            Console.SetCursorPosition(0, 1);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   Back (esc)    ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Reset (space)    ");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Code: ");
            if (showCode)
            {
                Console.Write("" + code.GetCode());
            }
            else
            {
                Console.Write("" + Write.Repeat("#", codeLength));
            }
            Console.WriteLine(" (tab)");
            Console.ResetColor();
        }

        void Update()
        {


            if (turn < guesses)
            {
                string guess = ManageInput();

                int[] result = code.Check(guess);
                Write.ClearLine();
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                turn++;

                Write.TableRow(new string[] { "" + turn, guess, "" + result[0], "" + result[1] }, 4);

                if (result[0] == codeLength)
                {
                    Win();
                }

            }
            else
            {
                Lose();
            }

            if (isRunning)
            {
                Update();
            }
        }

        string ManageInput()
        {
            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop);
            char[] input = Write.Repeat("#",codeLength).ToCharArray();
            Console.Write(input);
            int position = 0;
            
            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            while (key != ConsoleKey.Enter)
            {

                switch (key)
                {
                    case ConsoleKey.Tab: 
                        {
                            int left = Console.CursorLeft;
                            int top = Console.CursorTop;
                            showCode = !showCode;
                            DrawTopPanel();
                            Console.SetCursorPosition(left,top);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Write.ClearAll();
                            isRunning = false;
                            new StartMenu();
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            Write.ClearAll();
                            Start(codeLength, guesses);
                            break;
                        }
                    case ConsoleKey.Backspace:
                        {
                            Write.ClearLine();
                            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop - 1);
                            int back = position - 1;
                            if (back >= 0)
                            {
                                input[back] = '#';
                            }
                            Console.Write(input);
                            position = Math.Max(0, back);
                            Console.SetCursorPosition(Write.GuessPosition(codeLength)+position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                    case ConsoleKey.Delete:
                        {
                            Write.ClearLine();
                            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop - 1);
                          
                                input[position] = '#';
                            
                            Console.Write(input);
       
                            Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        { 
                            position = Math.Max(0, position - 1);
                            Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        { 
                            position = Math.Min(codeLength-1, position + 1);
                            Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                    default:
                        {

                            if (key >= ConsoleKey.D0 && key <= ConsoleKey.D9)
                            {
                                Write.ClearLine();
                                Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop-1);
                                char c = Char.ConvertFromUtf32(keyInfo.KeyChar)[0];
                                input[position] = c;
                                Console.Write(input);
                                position = Math.Min(codeLength-1, position + 1);
                                Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            }

                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }
                }
            }
            return new string(input);
        }
        void Win()
        {
            isRunning = false;
            
            Write.EmptyLine();
            ASCII.PrintCenter(ASCII.youWin, ConsoleColor.Cyan);
            Write.EmptyLine();
            EndGame();
            
        }

        

        void Lose()
        {
            isRunning = false;
            Write.EmptyLine();
            ASCII.PrintCenter(ASCII.gameOver, ConsoleColor.Red);
            Write.EmptyLine();
            EndGame();
        }

        void EndGame()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.Tab:
                    {
                        int left = Console.CursorLeft;
                        int top = Console.CursorTop;
                        showCode = !showCode;
                        DrawTopPanel();
                        Console.SetCursorPosition(left, top);
                        EndGame();
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        Write.ClearAll();
                        isRunning = false;
                        new StartMenu();
                        break;
                    }
                case ConsoleKey.Spacebar:
                    {
                        Write.ClearAll();
                        Start(codeLength, guesses);
                        break;
                    }
                default: 
                    {
                        EndGame();
                        break;
                    }
            }
        }
    }
}


