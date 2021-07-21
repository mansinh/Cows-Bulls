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

    public static class Write
    {

        public static void Center(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
        }

        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            EmptyLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            EmptyLine();
            Console.SetCursorPosition(0, Console.CursorTop-1);
        }

        public static void ClearAll()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.ResetColor();
        }

        public static void Highlight()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void EmptyLine()
        {
            Console.WriteLine(Padding(Console.WindowWidth));
        }

        public static string Padding(int length)
        {
            return Repeat(" ", length);
        }

        public static void HorizontalDivider()
        {
            Console.WriteLine(Underline(Console.WindowWidth));
        }

        public static string Underline(int length)
        {
            return Repeat("_", length);
        }

        public static string Repeat(string r, int length) 
        {
            string s = "";
            for (int i = 0; i < length; i++)
            {
                s = s + r;
            }
            return s;
        }

        public static string SetLength(string original, int length)
        {
            string s = "";
            int paddingLength = (length - original.Length) / 2;
            if (paddingLength >= 0)
            {
                s = original + Padding(paddingLength);
                s = Padding(length - s.Length) + s;
            }
            else
            {
                s = original.Substring(-paddingLength, length);
            }


            return s;
        }

        const int CELL_SIZE = 16;
        public static string Cell(string original)
        {
            return SetLength(original, CELL_SIZE);
        }
        public static void TableRow(string[] rowData, int rowLength)
        {
            ConsoleColor[] colors = { ConsoleColor.DarkGray,
                                      ConsoleColor.Yellow,
                                      ConsoleColor.Red,
                                      ConsoleColor.Cyan};
            Console.Write(Padding((Console.WindowWidth - rowLength * CELL_SIZE) / 2));

            for (int i = 0; i < rowLength; i++)
            {
                Console.ForegroundColor = colors[i % colors.Length];
                if (i < rowData.Length)
                {
                    Console.Write(Cell(rowData[i]));
                }
                else
                {
                    Console.Write(Cell(""));
                }
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        public static int GuessPosition(int codeLength) {
            return (Console.WindowWidth - 4 * CELL_SIZE) / 2+CELL_SIZE + (CELL_SIZE-((CELL_SIZE - codeLength) /2+codeLength));
        }
    }


    static class ASCII
    {
        public static string[] bulls = new string[]
            {"####&   ##  ##  ##   ##    #### ",
             "##  ##  ##  ##  ##   ##   ##  ##",
             "##  ##  ##  ##  ##   ##   ##    ",
             "####Y   ##  ##  ##   ##    Y##& ",
             "##  #&  ##  ##  ##   ##       ##",
             "##  ##  ##  ##  ##   ##   ##  ##",
             "#####Y  ######  #### ####  #### "};

        public static string[] and = new string[]
            {
                "          ",
                "  ####    ",
                " ##  ##   ",
                "  ## #    ",
                " &##### ##",
                "##   Y##Y ",
                "Y####Y ###"};

        public static string[] cows = new string[]
            {
                "A####A  ####  ##  ##  ##  #### ",
                "##  ## ##  ## ##  ##  ## ##  ##",
                "##     ##  ## ##  ##  ## ##    ",
                "##     ##  ## ##  ##  ##  Y##& ",
                "##     ##  ## ##  ##  ##     ##",
                "##  ## ##  ## ##  ##  ## ##  ##",
                "Y####Y  ####   ###YY###   #### "
            };

        public static string[] youWin = new string[]
            {
                "##  ##  ####  ##  ##   ## ## ## ## ##  ##   ##",
                "##  ## ##  ## ##  ##   ## ## ## ## ##! ##   ##",
                "##  ## ##  ## ##  ##   ## ## ## ## ### ##   ##",
                " Y##Y  ##  ## ##  ##   ## ## ## ## ######   ##",
                "  ##   ##  ## ##  ##   ## ## ## ## ## ###     ",
                "  ##    ####  ######    ##YY##  ## ##  ##   ##"
            };

        public static string[] gameOver = new string[]
            {
                " ####   ####  ##    ## #####    ####  ##  ## ##### #####",
                "##  ## ##  ## ###  ### ##      ##  ## ##  ## ##    ##  ##"   ,
                "##     ##  ## ######## ##      ##  ## ##  ## ##    ##  ##",
                "## ### ###### ## ## ## ####    ##  ## ##  ## ####  ####",
                "##  ## ##  ## ## ## ## ##      ##  ##  ####  ##    ## ##",
                " ####  ##  ## ## ## ## #####    ####    ##   ##### ##  ##"
            };

        public static void PrintCenter(string[] word, ConsoleColor color) {
            PrintCenter(new string[][] { word }, color);
        }
        public static void PrintCenter(string[][] words, ConsoleColor color)
        {
            PrintCenter(words, new ConsoleColor[] { color });
        }

        //change colour every (period) non space character if (period)>0
        //if period > 0, change colour every string

        public static void PrintCenter(string[][] words, ConsoleColor[] colors)
        { 
            int padding = 0;
            for (int i = 0; i < words.Length;i++) 
            {
                padding += words[i][0].Length;
                padding += (words.Length - 2) * 3;
            }
            padding = (int)Math.Round((Console.WindowWidth - padding) / 2.0)+1;
            //padding = (Console.WindowWidth - padding) / 2 + 1;

            for (int i = 0; i < words[0].Length; i++)
            {
                //per line
                Console.Write(Write.Padding(padding));
                
                for (int j = 0; j < words.Length; j++)
                { 
                    //per word
                    Console.ForegroundColor = colors[j% colors.Length];
                    Console.Write(words[j][i]);
                    if (j < words.Length-1)
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

    }

}

