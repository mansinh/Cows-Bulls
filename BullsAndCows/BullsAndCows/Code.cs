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
            rand = new Random();
        }

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
        public int[] Check(string guess)
        {
            int cows = 0;
            int bulls = 0;

            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = 0; j < code.Length; j++)
                {
                    if (guess[i] == code[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
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
