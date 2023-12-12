using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    class SpecialCharacters
    {
        public int row { get; private set; }
        public int index { get; private set; }

        public SpecialCharacters(int row, int index)
        {
            this.row = row;
            this.index = index;
        }
    }


    internal class Day03
    {
        static void ProcessInput()
            {
                int rowIndex = 0;
                bool lastCharNum = false;

                List<List<PartNumber>> parts = new List<List<PartNumber>>();
                List<int> numsInRows = new List<int>();
                foreach (string row in File.ReadAllLines("input.txt"))
                {
                    parts.Add(new List<PartNumber>());

                    numsInRows.Add(0);
                    lastCharNum = false;
                    string writeString = "";

                    foreach (char c in row)
                    {


                        if (char.IsDigit(c))
                        {
                            if (!lastCharNum)
                            {
                                writeString += '(';
                            }
                            else
                            {
                                writeString += c;
                            }
                            lastCharNum = true;
                            numsInRows[numsInRows.Count - 1] = (numsInRows.Last() * 10) + (c - 48);
                        }
                        else
                        {
                            writeString += c;
                            if (numsInRows.Last() != 0)
                            {
                                numsInRows.Add(0);
                            }
                            lastCharNum = false;
                        }

                    }
                    numsInRows.RemoveAll(x => x == 0);

                    int indexCounter = 0;
                    foreach (var item in numsInRows)
                    {
                        int stringcounter = 0;
                        foreach (var c in writeString)
                        {
                            if (c == '(')
                            {
                                writeString = writeString.Remove(stringcounter, 1).Insert(stringcounter, "."); ;
                                break;
                            }
                            stringcounter++;
                        }
                        parts.Last().Add(new PartNumber(item, parts.Count - 1, stringcounter));
                        indexCounter++;
                    }

                    numsInRows.Clear();
                    rowIndex++;
                }
            }
        static void GetCharacters()
        {
            List<List<SpecialCharacters>> chars = new List<List<SpecialCharacters>>();

            string specialChars = "/*=%@&-+$#";
            int rowIndex = 0;
            int colIndex = 0;

            foreach (string row in File.ReadAllLines("input.txt"))
            {
                chars.Add(new List<SpecialCharacters>());
                colIndex = 0;
                foreach (char c in row)
                {
                    if (specialChars.Contains(c))
                    {
                        chars.Last().Add(new SpecialCharacters(rowIndex, colIndex));
                    }
                    colIndex++;
                }
                rowIndex++;
            }

            foreach (var item in chars)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.row + "-" + item2.index);
                }
            }


        }

        static void Main(string[] args)
        {

            ProcessInput();
            GetCharacters();

        }
    }
}
