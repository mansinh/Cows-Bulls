using System;

namespace BullsAndCows
{

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

        public static void PrintCenter(string[] word, ConsoleColor color)
        {
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
            // Calculate total length
            for (int i = 0; i < words.Length; i++)
            {
                padding += words[i][0].Length;
                padding += (words.Length - 2) * 3;
            }

            // Center alignment
            padding = (int)Math.Round((Console.WindowWidth - padding) / 2.0) + 1;

            // write per line
            for (int i = 0; i < words[0].Length; i++)
            {
                
                Console.Write(Write.Padding(padding));

                // write per word
                for (int j = 0; j < words.Length; j++)
                {
                    
                    Console.ForegroundColor = colors[j % colors.Length];
                    Console.Write(words[j][i]);

                    // space between words
                    if (j < words.Length - 1)
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