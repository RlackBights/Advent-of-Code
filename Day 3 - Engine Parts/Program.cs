using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class PartNumber
    {
        public int value { get; private set; }
        public int row { get; private set; }
        public int startIndex { get; private set; }

        public PartNumber(int value, int row, int startIndex)
        {
            this.value = value;
            this.row = row;
            this.startIndex = startIndex;
        }
    }
    internal class Day03
    {
        static void Main(string[] args)
        {
            List<int> numsInRows = new List<int>();
            foreach (string row in File.ReadAllLines("input.txt"))
            {
                numsInRows.Add(0);

                foreach (char c in row)
                {

                    Console.Write(c);

                    if (char.IsDigit(c))
                    {
                        numsInRows[numsInRows.Count - 1] = (numsInRows.Last() * 10) + (c - 48);
                    }
                    else
                    {
                        if (numsInRows.Last() != 0)
                        {
                            numsInRows.Add(0);
                        }
                    }


                }
                numsInRows.RemoveAll(x => x == 0);
                foreach (var item in numsInRows)
                {
                    Console.Write(item + " ");
                }

                numsInRows.Clear();

                Console.WriteLine();
            }
        }
    }
}
