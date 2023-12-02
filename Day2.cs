using System.Diagnostics;
using System.Text.RegularExpressions;


// Not working yet
public class Day2 : ISolution
{
    public string Name => "Day 2";

    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "2/input.txt"));
        int maxRed = 12, maxGreen = 13, maxBlue = 14;
        int total = 0;
        foreach(string line in input)
        {
            string gameString = Regex.Match(line,@"^[^\d]*(\d+)").Value;
            int gameID = gameString.ParseInt();
            int red = 0, green = 0, blue = 0;
            string[] sets = line.Replace(gameString,"").Split(";");
            bool possible = true;
            foreach(string set in sets)
            {
                foreach(Match m in Regex.Matches(set,@"([0-9]+ )\w+"))
                {
                    if(m.Value.ToLower().Contains("red"))
                    {
                        red += m.Value.ParseInt();
                    }
                    else if(m.Value.ToLower().Contains("green"))
                    {
                        green += m.Value.ParseInt();
                    }
                    else if(m.Value.ToLower().Contains("blue"))
                    {
                        blue += m.Value.ParseInt();
                    }
                }
                Console.WriteLine($"{red} {green} {blue}");
                if(red > maxRed || green > maxGreen || blue > maxBlue)
                {
                    possible = false;
                    Console.WriteLine($"Game {gameID} is impossible.");
                    red = 0;
                    green = 0;
                    blue = 0;
                    continue;
                }
                else
                {
                    red = 0;
                    green = 0;
                    blue = 0;
                }
                
            }
            //Console.WriteLine($"Game {gameID}: red:{red} green:{green} blue:{blue}");
            if(possible)
            {
                Console.WriteLine($"Game {gameID} is possible.");
                total += gameID;
            }
            else
            {
                Console.WriteLine($"Game {gameID} is impossible.");
            }
        }
        Console.WriteLine($"Total: {total}\n");

    }

    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "2/input.txt"));
        int maxRed = 12, maxGreen = 13, maxBlue = 14;
        int total = 0;
        foreach(string line in input)
        {
            string gameString = Regex.Match(line,@"^[^\d]*(\d+)").Value;
            int gameID = gameString.ParseInt();
            int minRed = 0, minGreen = 0, minBlue = 0;
            string[] sets = line.Replace(gameString,"").Split(";");
            foreach(string set in sets)
            {
                foreach(Match m in Regex.Matches(set,@"([0-9]+ )\w+"))
                {
                    if(m.Value.ToLower().Contains("red"))
                    {
                        int val = m.Value.ParseInt();
                        minRed = val > minRed ? val : minRed;
                    }
                    else if(m.Value.ToLower().Contains("green"))
                    {
                        int val = m.Value.ParseInt();
                        minGreen = val > minGreen ? val : minGreen;
                    }
                    else if(m.Value.ToLower().Contains("blue"))
                    {
                        int val = m.Value.ParseInt();
                        minBlue = val > minBlue ? val : minBlue;
                    }
                }
            }
            minRed = minRed == 0 ? 1 : minRed;
            minGreen = minGreen == 0 ? 1 : minGreen;
            minBlue = minBlue == 0 ? 1 : minBlue;
            int power = minRed * minGreen * minBlue;
            Console.WriteLine($"Game {gameID}: power:{power}");
            total += power;
        }
        Console.WriteLine($"Total power: {total}\n");
    }
}