using System;

namespace BullsAndCows
{
    // Static class used to write onto the console

    public static class Write
    {
        // Write on to console with center alignment
        public static void Center(string s)
        {
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
        }

        // Clear the line above and set cursor position back to previous line 
        public static void ClearLastLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            EmptyLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        // Clear the current line
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            EmptyLine();
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        // Clear all from screen
        public static void ClearAll()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();
            Console.ResetColor();
        }

        // Highlight to show currently selected option
        public static void Highlight()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        // Writes an empty line
        public static void EmptyLine()
        {
            Console.WriteLine(Padding(Console.WindowWidth));
        }

        // Writes empty spaces of length (length)
        public static string Padding(int length)
        {
            return Repeat(" ", length);
        }

        // Writes a line across the window 
        public static void HorizontalDivider()
        {
            Console.WriteLine(Underline(Console.WindowWidth));
        }

        // Writes a line of length (length)
        public static string Underline(int length)
        {
            return Repeat("_", length);
        }

        // Repeats a string (length) times
        public static string Repeat(string r, int length)
        {
            string s = "";
            for (int i = 0; i < length; i++)
            {
                s = s + r;
            }
            return s;
        }

        // Sets a string to be of length (length) with padding on each side 
        // if length is longer than original length, otherwise truncates string
    
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

        // Writes an array of data into a table row with alternating colors

        public static void TableRow(string[] rowData, int rowLength)
        {
            ConsoleColor[] colors = { ConsoleColor.DarkGray,
                                      ConsoleColor.Yellow,
                                      ConsoleColor.Red,
                                      ConsoleColor.Cyan};
            // center the table
            Console.Write(Padding((Console.WindowWidth - rowLength * CELL_SIZE) / 2));

            // write row
            for (int i = 0; i < rowLength; i++)
            {
                // write cell
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
            // next row
            Console.WriteLine();
        }

        //Cursor position for left most point of the guess input
        public static int GuessPosition(int codeLength)
        {
            return (Console.WindowWidth - 4 * CELL_SIZE) / 2 + CELL_SIZE + (CELL_SIZE - ((CELL_SIZE - codeLength) / 2 + codeLength));
        }
    }


}