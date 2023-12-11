using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Day01
    {
        static string ParseTextNums(string row)
        {
            string[] numTexts = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

            int numIndex = 0;

            foreach (string num in numTexts)
            {
                numIndex++;

                if (row.Contains(num))
                {
                    row = row.Replace(num, numTexts[numIndex - 1].First() + numIndex.ToString() + numTexts[numIndex - 1].Last());
                }
            }

            return row;

        }

        static void Main(string[] args)
        {
            int nums = 0;
            foreach (string row in File.ReadAllLines("input.txt"))
            {
                List<int> currentNums = new List<int>();
                foreach (char c in ParseTextNums(row))
                {
                    bool wasParsable = int.TryParse(c.ToString(), out int x);
                    if (wasParsable)
                    {
                        currentNums.Add(x);
                    }
                }
                if (currentNums.Count >= 1)
                {
                    nums += currentNums.First() * 10 + currentNums.Last();
                }

            };

            Console.WriteLine(nums);
        }



    }
}
