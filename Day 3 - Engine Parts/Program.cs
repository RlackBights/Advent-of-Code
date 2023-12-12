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
        public List<PartNumber> neighbours { get; private set; }

        public SpecialCharacters(int row, int index)
        {
            this.row = row;
            this.index = index;
            this.neighbours = new List<PartNumber>();
        }

        public void AddNeigbour(PartNumber number)
        {
            neighbours.Add(number);
        }

    }


    internal class Day03
    {
        static List<PartNumber> ProcessInput()
            {
                int rowIndex = 0;
                bool lastCharNum = false;

                List<PartNumber> parts = new List<PartNumber>();
                List<int> numsInRows = new List<int>();


                foreach (string row in File.ReadAllLines("input.txt"))
                {

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
                        parts.Add(new PartNumber(item, rowIndex, stringcounter));
                        indexCounter++;
                    }

                    numsInRows.Clear();
                    rowIndex++;
                }
                return parts;
            }
        static List<SpecialCharacters> GetCharacters()
        {
            List<SpecialCharacters> chars = new List<SpecialCharacters>();

            string specialChars = "*";
            int rowIndex = 0;
            int colIndex = 0;

            foreach (string row in File.ReadAllLines("input.txt"))
            {
                colIndex = 0;
                foreach (char c in row)
                {
                    if (specialChars.Contains(c))
                    {
                        chars.Add(new SpecialCharacters(rowIndex, colIndex));
                    }
                    colIndex++;
                }
                rowIndex++;
            }

            return chars;
        }
        static bool IsNumInRange(int a, int b, int range)
        {
            return Math.Abs(a - b) <= range;
        }

        static void Main(string[] args)
        {

            List<PartNumber> parts = ProcessInput();
            List<SpecialCharacters> characters = GetCharacters();
            List<PartNumber> used = new List<PartNumber>();

            long finalNum = 0;

            foreach (SpecialCharacters character in characters)
            {
                foreach (PartNumber part in parts)
                {
                    if (IsNumInRange(part.row, character.row, 1) && !used.Contains(part))
                    {
                        //Console.Write($"Found match in rows between {part.row} and {character.row} - ");
                        if (IsNumInRange(part.startIndex, character.index, 1))
                        {
                            //finalNum += part.value;
                            //Console.WriteLine($"Found {part.row}-{part.startIndex} and {character.row}-{character.index} to be adjacent. Current num: {finalNum}");
                            character.AddNeigbour(part);
                            used.Add(part);
                        }
                        else
                        {
                            //Console.WriteLine($"Found {part.row}-{part.startIndex} and {character.row}-{character.index} to be too far apart");
                        }


                        if (IsNumInRange(part.startIndex + part.value.ToString().Length - 1, character.index, 1) && !used.Contains(part))
                        {
                            //finalNum += part.value;
                            character.AddNeigbour(part);
                            //Console.WriteLine($"Found {part.row}-{part.startIndex} and {character.row}-{character.index} to be adjacent. Current num: {finalNum}");
                            used.Add(part);
                        }
                        else
                        {
                            //Console.WriteLine($"Found {part.row} - {part.startIndex} and {character.row}-{character.index} to be too far apart");
                        }
                        //Console.WriteLine();
                    }
                }
            }

            foreach (var item in characters.Where(x => x.neighbours.Count == 2))
            {
                Console.Write($"* at {item.row}-{item.index}, with a power of {item.neighbours[0].value * item.neighbours[1].value}\n");
                finalNum += item.neighbours[0].value * item.neighbours[1].value;
            }

            Console.WriteLine($"\nThe final value is {finalNum}");


        }
    }
}
