namespace BullsAndCows
{
    using System;



    class Game
    {
        public static Game instance;
        bool isRunning;
        bool showCode = false; // Show answer code
        Code code;
        int turn = 0; // Current number of guesses/turns
        int guesses = 10; // Maximum guesses before gameover
        int codeLength = 4; // Length of secret code

        public Game()
        {
            // Singleton
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
            // Apply game settings from start menu
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

            // Write player prompt
            Write.HorizontalDivider();
            Write.Center(" Guess the secret " + codeLength + " digit code within " + guesses + " turns");
            Write.HorizontalDivider();
         
            // Write headings for guesses and result table
            Write.TableRow(new string[] { "#", "GUESS", "BULLS", "COWS" }, 4);
            Write.EmptyLine();

            Update();
        }

        // Draw panel that displays controls and hidden answer
        void DrawTopPanel() 
        {
            Console.SetCursorPosition(0, 1);

            // Press esc to return to start menu
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("   Back (esc)    ");

            // Press space to reset the game with new code
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Reset (space)    ");

            // Press tab to reveal hidden code
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
            // if there are still guesses left, await player input, otherwise game over
            if (turn < guesses)
            {
                // read player input
                string guess = ManageInput();

                // compare player guess with code
                int[] result = code.Check(guess);
                Write.ClearLine();
                // next line
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                turn++;

                // write the result of the guess
                Write.TableRow(new string[] { "" + turn, guess, "" + result[0], "" + result[1] }, 4);

                // if correctly guessed, tell player and end game
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

        // React to player input
        string ManageInput()
        {
            // Set cursor to guess input
            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop);
            char[] input = Write.Repeat("#",codeLength).ToCharArray();
            Console.Write(input);
            int position = 0;
            
            // Read player input 
            Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop);
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            while (key != ConsoleKey.Enter)
            {

                switch (key)
                {
                    // Toggle show answer when tab pressed
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
                    // Return to start menu when esc pressed
                    case ConsoleKey.Escape:
                        {
                            Write.ClearAll();
                            isRunning = false;
                            new StartMenu();
                            break;
                        }
                    // Reset game with new code when space pressed
                    case ConsoleKey.Spacebar:
                        {
                            Write.ClearAll();
                            Start(codeLength, guesses);
                            break;
                        }
                    // Move cursor left 1 space and delete guess number 
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
                     // Delete the current guess number
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

                    // Move cursor left 1 space
                    case ConsoleKey.LeftArrow:
                        { 
                            position = Math.Max(0, position - 1);
                            Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }

                    // Move cursor right 1 space
                    case ConsoleKey.RightArrow:
                        { 
                            position = Math.Min(codeLength-1, position + 1);
                            Console.SetCursorPosition(Write.GuessPosition(codeLength) + position, Console.CursorTop);
                            keyInfo = Console.ReadKey(true);
                            key = keyInfo.Key;
                            break;
                        }

                    // Input number and ignore all other inputs
                    default:
                        {
                            // if key is a number
                            bool isNumber = key >= ConsoleKey.D0 && key <= ConsoleKey.D9;
                            // if key is a number from number pad
                            bool isNumPad = key >= ConsoleKey.NumPad0 && key <= ConsoleKey.NumPad9;

                           
                            if (isNumber || isNumPad)
                            {
                                // convert key to number character and write onto console
                                Write.ClearLine();
                                Console.SetCursorPosition(Write.GuessPosition(codeLength), Console.CursorTop-1);
                                char c = Char.ConvertFromUtf32(keyInfo.KeyChar)[0];
                                input[position] = c;
                                Console.Write(input);

                                // move cursor right while keeping cursor inside guess input area
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

        // Display You Win and end game
        void Win()
        {
            isRunning = false;
            
            Write.EmptyLine();
            ASCII.PrintCenter(ASCII.youWin, ConsoleColor.Cyan);
            Write.EmptyLine();
            EndGame();
            
        }

        
        // Display Game Over and end game
        void Lose()
        {
            isRunning = false;
            Write.EmptyLine();
            ASCII.PrintCenter(ASCII.gameOver, ConsoleColor.Red);
            Write.EmptyLine();
            EndGame();
        }

        // End game and read whether the player wants to return to start menu,
        // show the hidden answer or reset and start a new game
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


