using System.Text.RegularExpressions;
public class Day3 : ISolution
{
    public string Name => "Day 3";

    private static char[] symbols = new char[] {'*', '@', '+', '-', '%', '=', '/', '&', '$', '#'};
    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "3/input.txt"));
        char[][] map = new char[input.Length][];
        map.InitializeJaggedArray(input[0].Length, input.Length);
        for (int i = 0; i < input.Length; i++)
        {
            map[i] = input[i].ToCharArray();
        }

        // That was purely to see if my extension method worked and the input was correctly parsed
        /*foreach (char[] row in map)
        {
            foreach (char c in row)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }*/

        int total = 0;
        int lineNum = 0;
        foreach(string line in input)
        {
            MatchCollection matches = Regex.Matches(line, @"\d+");
            if(matches.Count > 0)
            {
                foreach(Match match in matches)
                {
                    for(int i = 0; i < match.Length; i++)
                    {
                        if(map.HasNeighbor<char>(match.Index + i, lineNum, symbols))
                        {
                            total += match.Value.ParseInt();
                            break;
                        }
                    }
                }
            }
            lineNum++;
        }
        Console.WriteLine($"Total: {total}");
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "3/input.txt"));
        char[][] map = new char[input.Length][];
        map.InitializeJaggedArray(input[0].Length, input.Length);
        for (int i = 0; i < input.Length; i++)
        {
            map[i] = input[i].ToCharArray();
        }
        int total = 0;
        int lineNum = 0;
        Dictionary<(int x, int y), List<int>> coords = new();
        foreach(string line in input)
        {
            // I hate regex but its so freaking useful for this. Think i've used it in all my solutions so far (even if poorly)
            MatchCollection matches = Regex.Matches(line, @"\d+");
            if(matches.Count > 0)
            {
                foreach(Match match in matches)
                {
                    for(int i = 0; i < match.Length; i++)
                    {
                        if(map.HasNeighborsOut<char>(match.Index + i, lineNum, out (int x, int y)[] charCoords, '*'))
                        {
                            foreach((int x, int y) coord in charCoords)
                            {
                                if(!coords.ContainsKey(coord))
                                {
                                    coords.Add(coord, new List<int>());
                                }
                                coords[coord].Add(match.Value.ParseInt());
                            }
                            break;
                        }
                    }
                }
            }
            lineNum++;
        }
        foreach(List<int> gears in coords.Values)
        {
            if(gears.Count == 2)
            {
                total += gears[0] * gears[1];
            }
        }
        Console.WriteLine($"Total: {total}");
    }
}