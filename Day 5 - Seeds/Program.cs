using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5___Seeds
{
    class Seed
    {
        public long Id { get; private set; }
        public long translatedValue { get; set; }

        public Seed(long num)
        {
            Id = num;
            translatedValue = num;
        }
    }

    class Map
    {
        public long sourceStart { get; private set; }
        public long destinationStart { get; private set; }
        public long range { get; private set; }

        public Map(string row)
        {
            long[] n = Array.ConvertAll(row.Trim().Split(' '), long.Parse);
            destinationStart = n[0];
            sourceStart = n[1];
            range = n[2];
        }

        public long processNumber(long input)
        {
            long result = -1;
            if (input >= sourceStart && input < sourceStart + range)
            {
                result = (long)((input - sourceStart) + destinationStart);
            }
            return result;
        }
    }

   

    internal class Program
    {
        static long ProcessCurrentSeeds(List<long> seeds, List<List<Map>> maps, long closest)
        {
            for (int i = 0; i < seeds.Count; i++)
            {
                foreach (var map in maps)
                {
                    foreach (var translation in map)
                    {
                        if (translation.processNumber(seeds[i]) != -1)
                        {
                            seeds[i] = translation.processNumber(seeds[i]);
                            Console.WriteLine("Translated seed");
                            break;
                        }
                    }
                }
            }

            foreach (long seed in seeds)
            {
                if (seed < closest)
                {
                    closest = seed;
                }
            }

            return closest;
        }

        static void Main(string[] args)
        {
            List<long> seeds = new List<long>();
            List<Map> soil = new List<Map>();
            List<Map> fertilizer = new List<Map>();
            List<Map> water = new List<Map>();
            List<Map> light = new List<Map>();
            List<Map> temperature = new List<Map>();
            List<Map> humidity = new List<Map>();
            List<Map> location = new List<Map>();
            List<List<Map>> maps = new List<List<Map>>() { soil, fertilizer, water, light, temperature, humidity, location };

            int metaRowCounter = 0;
            long closest = long.MaxValue;

            foreach (string row in File.ReadAllLines("input.txt"))
            {
                if (row == "") continue;
                if (!long.TryParse(row[0].ToString(), out long x))
                {
                    if (metaRowCounter++ == 0)
                    {
                        long[] seedNums = Array.ConvertAll(row.Split(':')[1].Trim().Split(' '), long.Parse);
                        Console.WriteLine(seedNums.Length);
                        for (int i = 0; i < seedNums.Length; i += 2)
                        {
                            seeds.Clear();
                            for (int j = 0; j < seedNums[i + 1]; j++)
                            {
                                seeds.Add(seedNums[i] + j);
                                Console.WriteLine($"Added {seedNums[i] + j}");
                            }
                            closest = ProcessCurrentSeeds(seeds, maps, closest);
                        }


                    }
                } else
                {
                    maps[metaRowCounter - 2].Add(new Map(row));
                }
            }

            Console.WriteLine(closest);

        }
    }
}
