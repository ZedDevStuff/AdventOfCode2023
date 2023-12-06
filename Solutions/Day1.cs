using System.Diagnostics;
using System.Text.RegularExpressions;
using Utility;

public class Day1 : ISolution
{
    public string Name => "Day 1";

    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "1/input.txt"));
        int total = 0;
        foreach (string line in input)
        {
            string potNum = Regex.Replace(line, @"[^\d]+", "\n").Trim();
            if (potNum.Length == 0) continue;
            if (potNum.Length == 1) total += int.Parse(potNum + potNum);
            if (potNum.Length > 1) total += int.Parse(potNum[0].ToString() + potNum[^1].ToString());
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "1/input.txt"));
        int total = 0;
        foreach (string line in input)
        {
            int num = CheckNumbers(line);
            Console.WriteLine($"{line} = {num}");
            total += num;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ", ConsoleColor.White), ($"{total}\n", ConsoleColor.Green));
    }
    public int CheckNumbers(string str)
    {
        // First pass
        string result = "";
        str = str.ToLower();
        for(int i = 0; i < str.Length; i++)
        {
            int remaining = str.Length - i;
            if(char.IsLetter(str[i]))
            {
                if(str[i] == 'o')
                {
                    if(remaining < 3) continue;
                    if(str.Substring(i,3) == "one")
                    {
                        result += "1";
                    }
                }
                else if(str[i] == 't')
                {
                    if(!(remaining >= 3)) continue;
                    if(str.Substring(i,3) == "two")
                    {
                        result += "2";
                    }
                    else if(str.Substring(i,3) == "ten")
                    {
                        result += "10";
                    }
                    else if(remaining >= 5)
                    {
                        if(str.Substring(i, 5) == "three") result += "3";
                    }
                }
                else if(str[i] == 'f')
                {
                    if(remaining < 4) continue;
                    if(str.Substring(i,4) == "four")
                    {
                        result += "4";
                    }
                    else if(str.Substring(i,4) == "five")
                    {
                        result += "5";
                    }
                }
                else if(str[i] == 's')
                {
                    if (!(remaining >= 3)) continue;
                    if (str.Substring(i, 3) == "six")
                    {
                        result += "6";
                    }
                    else if (remaining >= 5)
                    {
                        if(str.Substring(i, 5) == "seven") result += "7";
                    }
                }
                else if(str[i] == 'e')
                {
                    if(remaining < 5) continue;
                    if(str.Substring(i,5) == "eight")
                    {
                        result += "8";
                    }
                }
                else if(str[i] == 'n')
                {
                    if(remaining < 4) continue;
                    if(str.Substring(i,4) == "nine")
                    {
                        result += "9";
                    }
                }
            }
            else if(char.IsDigit(str[i]))
            {
                result += str[i];
            }
        }
        if(result.Length == 0) return 0;
        if(result.Length == 1) return int.Parse(result + result);
        return int.Parse(result[0].ToString() + result[^1].ToString());
    }
    
}