using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    class Card
    {
        public int[] winningNums { get; private set; }
        public int[] ownNumbers { get; private set; }
        public int cardValue { get; private set; }
        public int cardId { get; set; }
        public int copies { get; private set; }
        public Card(string row)
        {
            winningNums = Array.ConvertAll(row.Split('|')[0].Trim().Replace("  ", " ").Split(' '), int.Parse);
            ownNumbers = Array.ConvertAll(row.Split('|')[1].Trim().Replace("  ", " ").Split(' '), int.Parse);
            copies = 1;
            CalculateValue();
        }

        void CalculateValue()
        {
            cardValue = 0;
            foreach (int own in ownNumbers)
            {
                if (winningNums.Contains(own))
                {
                    /*if (cardValue == 0)
                    {
                        cardValue = 1;
                    } else
                    {
                        cardValue *= 2;
                    }*/

                    cardValue++;
                }
            }
        }

        public void AddCopy()
        {
            copies++;
            CalculateValue();

        }
        

    }
    internal class Day4
    {
        static void Main(string[] args)
        {
            List<Card> cards = new List<Card>();
            int rowCounter = 0;
            foreach (string row in File.ReadAllLines("input.txt"))
            {
                Card c = new Card(row.Split(':')[1].Trim());
                c.cardId = rowCounter++;
                cards.Add(c);
            }

            /* 1. part solution
            int sum = 0;

            foreach (var item in cards)
            {
                sum += item.cardValue;
            }

            Console.WriteLine(sum);
            */

            foreach (var c in cards)
            {
                Console.WriteLine($"Adding {c.copies}*{c.cardValue/c.copies} copies");
                for (int j = 0; j < c.copies; j++)
                {
                    for (int i = 1; i <= c.cardValue; i++)
                    {
                        Console.WriteLine($"Added copy to card #{c.cardId + i + 1}");
                        cards[c.cardId + i].AddCopy();
                    }
                }
            }

            Console.WriteLine($"Ended up with {cards.Sum((c) => c.copies)} copies");

        }
    }
}
