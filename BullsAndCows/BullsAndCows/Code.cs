using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    class Code
    {
        string code;
        Random rand;

        public Code()
        {
            // initiate random number generator
            rand = new Random();
        }

        // Generate a code with no repeating numbers of length (length)
        public void GenerateCode(int length)
        {
            //length = Math.Min(Math.Max(0, length), 10);
            string available = "0123456789";
            code = "";
            for (int i = 0; i < length; i++)
            {
                int index = rand.Next(0, available.Length - 1);
                char next = available[index];

                available = available.Remove(index, 1);
                code = code + "" + next;
            }

        }

        // Check for cows (correct number, incorrect position) and bulls (correct number and position)
        public int[] Check(string guess)
        {
            int cows = 0;
            int bulls = 0;

            // for each number in guess, compare with each number in code
            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = 0; j < code.Length; j++)
                {
                    // correct number
                    if (guess[i] == code[j])
                    {
                        
                        if (i == j)
                        {
                            // correct position
                            bulls++;
                        }
                        else
                        {
                            // incorrect position
                            cows++;
                        }
                    }
                }
            }
            return new int[] { bulls, cows };
        }


        public string GetCode()
        {
            return code;
        }

        public void Print()
        {
            Console.WriteLine(code);
        }
    }
}
