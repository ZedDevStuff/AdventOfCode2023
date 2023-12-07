using System.Text.RegularExpressions;
using Utility;

public class Day6 : ISolution
{
    public string Name => "Day 6: Wait for it";

    public void Part1()
    {
        string[] input = File.ReadAllLines("Data/6/input.txt");
        List<int> times = new(), distances = new();
        input[0].Replace("Time:","").Trim();
        times = Regex.Matches(input[0], @"\d+").Select(m => int.Parse(m.Value)).ToList();
        input[1].Replace("Distance:","").Trim();
        distances = Regex.Matches(input[1], @"\d+").Select(m => int.Parse(m.Value)).ToList();
        Dictionary<int,List<(int time,int distance)>> results = new();
        foreach(int i in Quick.Range(0, times.Count-1))
        {
            results.Add(i,new());
            foreach(int j in Quick.Range(0,times[i]))
            {
                int distance = j * (times[i]-j);
                if(distance > 0)
                {
                    if(distance > distances[i])
                    {
                        results[i].Add((j,distance));
                    }
                }
            }
        }
        int total = 1;
        foreach(var e in results)
        {
            // results.Value.Count ways to win race e.Key
            ConsoleEx.WriteLineColor((e.Value.Count,ConsoleColor.Green), (" ways to win race ",ConsoleColor.White), (e.Key+1,ConsoleColor.Cyan));
            total *= e.Value.Count;
        }
        ConsoleEx.WriteLineColor(("\nResult: ",ConsoleColor.White), ($"{total}\n",ConsoleColor.Green));
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines("Data/6/input.txt");
        Regex r = new Regex(@" +");
        long allowedTime = 0, bestDistance = 0;
        input[0].Replace("Time:","").Trim();
        allowedTime = long.Parse(Regex.Matches(input[0], @"\d+").Select(m => m.Value).Aggregate((a,b) => a.Trim()+b.Trim()));
        input[1].Replace("Distance:","").Trim();
        bestDistance = long.Parse(Regex.Matches(input[1], @"\d+").Select(m => m.Value).Aggregate((a,b) => a.Trim()+b.Trim()));
        List<(long time,long distance)> results = new();
        foreach(long j in Quick.Range(0,allowedTime))
        {
            long distance = j * (allowedTime-j);
            if(distance > 0)
            {
                if(distance > bestDistance)
                {
                    results.Add((j,distance));
                }
            }
        }
        ConsoleEx.WriteLineColor(("\nResult: ",ConsoleColor.White), ($"{results.Count}",ConsoleColor.Green), (" ways to win the race\n",ConsoleColor.White));
    }
}