using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5___Seeds
{
    class Seed
    {
        public int Id { get; private set; }
        public int translatedValue { get; set; }

        public Seed(int num)
        {
            Id = num;
            translatedValue = num;
        }
    }

    class Map
    {
        public int sourceStart { get; private set; }
        public int destinationStart { get; private set; }
        public int range { get; private set; }

        public Map(string row)
        {
            int[] n = Array.ConvertAll(row.Trim().Split(' '), int.Parse);
            destinationStart = n[0];
            sourceStart = n[1];
            range = n[2];
        }

        public int processNumber(int input)
        {
            if (input >= sourceStart && input < sourceStart + range)
            {
                return (input - sourceStart) + destinationStart;
            }
            else
            {
                return -1;
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {
            List<Seed> seeds = new List<Seed>();
            List<Map> soil = new List<Map>();
            List<Map> fertilizer = new List<Map>();
            List<Map> water = new List<Map>();
            List<Map> light = new List<Map>();
            List<Map> temperature = new List<Map>();
            List<Map> humidity = new List<Map>();
            List<Map> location = new List<Map>();
            List<List<Map>> maps = new List<List<Map>>() { soil, fertilizer, water, light, temperature, humidity, location };

            int metaRowCounter = 0;

            foreach (string row in File.ReadAllLines("input.txt"))
            {
                if (row == "") continue;
                if (!int.TryParse(row[0].ToString(), out int x))
                {
                    if (metaRowCounter++ == 0)
                    {
                        foreach (int num in Array.ConvertAll(row.Split(':')[1].Trim().Split(' '), int.Parse))
                        {
                            seeds.Add(new Seed(num));
                        }
                    }
                } else
                {
                    maps[metaRowCounter - 2].Add(new Map(row));
                }
            }



            foreach (Seed seed in seeds)
            {
                foreach (var map in maps)
                {
                    foreach (var translation in map)
                    {

                    }
                }
            }


        }
    }
}
