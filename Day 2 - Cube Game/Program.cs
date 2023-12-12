using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode
{
    internal class GameClass
    {
        public bool isValidGame { get; private set; }
        public int gameID { get; private set; }
        public int highestRed { get; private set; }
        public int highestGreen { get; private set; }
        public int highestBlue { get; private set; }



        public GameClass(string row)
        {
            highestRed = 0;
            highestGreen = 0;
            highestBlue = 0;
            isValidGame = true;

            while (row.Contains(" "))
            {
                row = row.Replace(" ", "");
            }

            gameID = int.Parse(row.Split(':')[0].Substring(4));

            foreach (string round in row.Split(':')[1].Split(';'))
            {

                foreach (string draw in round.Split(','))
                {
                    int drawNum = 0;
                    string drawCol = "";

                    if (int.TryParse(draw.Substring(0, 2), out drawNum))
                    {
                        drawCol = draw.Substring(2);
                    } else {
                        drawNum = draw[0] - 48;
                        drawCol = draw.Substring(1);
                    }

                    if (drawCol == "red" && drawNum > highestRed)
                    {
                        Console.Write(highestRed + " - " + drawNum);
                        highestRed = drawNum;
                        Console.WriteLine(" => " + highestRed);
                    }
                    if (drawCol == "green" && drawNum > highestGreen)
                    {
                        Console.Write(highestGreen + " - " + drawNum);
                        highestGreen = drawNum;
                        Console.WriteLine(" => " + highestGreen);
                    }
                    if (drawCol == "blue" && drawNum > highestBlue)
                    {
                        Console.Write(highestBlue + " - " + drawNum);
                        highestBlue = drawNum;
                        Console.WriteLine(" => " + highestBlue);
                    }

                    if (drawCol == "red" && drawNum > 12) isValidGame = false;
                    if (drawCol == "green" && drawNum > 13) isValidGame = false;
                    if (drawCol == "blue" && drawNum > 14) isValidGame = false;
                }
            }
        }

    }
    internal class Day02
    {
        static void Main(string[] args)
        {

            Console.WriteLine(num + num2);


            long gamePowers = 0;

            List<GameClass> games = new List<GameClass>();
            foreach (string row in File.ReadAllLines("input.txt"))
            {
                games.Add(new GameClass(row));
            }

            foreach (var game in games)
            {
                gamePowers += game.highestGreen * game.highestRed * game.highestBlue;
                Console.WriteLine($"{game.highestRed} * {game.highestGreen} * {game.highestBlue} = {gamePowers}");
            }

        }
    }
}
