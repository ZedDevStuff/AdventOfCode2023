using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using Utility;

public class Day8 : ISolution
{
    public string Name => "Day 8: Haunted Wasteland";

    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "8", "input.txt"));
        char[] instructions = input[0].Trim().ToCharArray();
        Dictionary<string, (string left, string right)> map = new();
        foreach (string line in input.Skip(2))
        {
            MatchCollection matches = Regex.Matches(line,@"[A-Z]+");
            map.Add(matches[0].Value, (matches[1].Value, matches[2].Value));
        }
        int totalInstructions = 0;
        bool arrived = false;
        string currentLocation = "AAA";
        while(true)
        {
            for(int i = 0; i < instructions.Length; i++)
            {
                if(instructions[i] == 'L')
                {
                    //ConsoleEx.WriteLineColor((currentLocation,ConsoleColor.Yellow), (" to ",ConsoleColor.Gray), (map[currentLocation].left,ConsoleColor.Green), ($" (total: {totalInstructions})",ConsoleColor.Yellow));
                    currentLocation = map[currentLocation].left;
                }
                else
                {
                    //ConsoleEx.WriteLineColor((currentLocation,ConsoleColor.Yellow), (" to ",ConsoleColor.Gray), (map[currentLocation].left,ConsoleColor.Green), ($" (total: {totalInstructions})",ConsoleColor.Yellow));
                    currentLocation = map[currentLocation].right;
                }
                totalInstructions += 1;
                if(currentLocation == "ZZZ")
                {
                    arrived = true;
                    break;
                }
            }
            if(arrived) break;
        }
        ConsoleEx.WriteLineColor(($"Total Instructions to reach end: ",ConsoleColor.White), (totalInstructions, ConsoleColor.Green));
    }
    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "8", "test.txt"));
        char[] instructions = input[0].Trim().ToCharArray();
        Dictionary<string, (string left, string right)> map = new();
        foreach (string line in input.Skip(2))
        {
            // This Regex is only different because i needed to test the example which had numbers in the names
            MatchCollection matches = Regex.Matches(line,@"[\d\w]+");  
            map.Add(matches[0].Value, (matches[1].Value, matches[2].Value));
        }
        ConcurrentDictionary<string, (string loc, bool done)> status = new();
        List<string> startingPoints = new();
        map.Where(x => x.Key[2] == 'A').ToList().ForEach(x =>
        {
            startingPoints.Add(x.Key);
            status.TryAdd(x.Key, (x.Key, false));
        });
        
        bool arrived = false;
        int total = 0;
        while(!arrived)
        {
            for(int i = 0;i < instructions.Length;i++)
            {
                Parallel.ForEach(startingPoints, (startingPoint) =>
                {
                    status[startingPoint] = (status[startingPoint].loc, true);
                    string location = status[startingPoint].loc;
                    if(instructions[i] == 'L')
                    {
                        status[startingPoint] = (map[location].left,false);
                        location = map[location].left;
                    }
                    else
                    {
                        status[startingPoint] = (map[location].right,false);
                        location = map[location].right;
                    }
                    if(location[2] == 'Z')
                    {
                        status[startingPoint] = (location,true);
                    }
                });
                total++;
                if(startingPoints.All(x => status[x].done))
                {
                    arrived = true;
                    break;
                }
            }
        }
        ConsoleEx.WriteLineColor(($"Total Instructions to reach end of all nodes: ",ConsoleColor.White), (total, ConsoleColor.Green));
    }
}