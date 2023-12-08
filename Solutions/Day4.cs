using Extensions;
using System.Text.RegularExpressions;
using Utility;

public class Day4 : ISolution
{
    string ISolution.Name => "Day 4: Scratchcards";

    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "4", "input.txt"));
        int current = 1;
        int total = 0;
        foreach (string line in input)
        {
            Regex regex = new Regex(@"(Card +\d+: +)");
            string card = regex.Replace(line,"");
            regex = new Regex(@"( +)");
            card = regex.Replace(card, " ");
            List<int> winning = card.Split(" | ")[0].Split(" ").Select(x => int.Parse(x)).ToList();
            List<int> ourNumbers = card.Split(" | ")[1].Split(" ").Select(x => int.Parse(x)).ToList();
            List<int> score = new List<int>();
            int finalScore = 0;
            foreach (int number in ourNumbers)
            {
                foreach(int win in winning)
                {
                    if(number == win)
                    {
                        if(finalScore == 0)
                        {
                            finalScore = 1;
                        }
                        else
                        {
                            finalScore *= 2;
                        }
                    }
                }
                
            }
            total += finalScore;
            ConsoleEx.WriteLineColor(("Card ",ConsoleColor.White),($"{current}",ConsoleColor.Green),(" is worth ",ConsoleColor.White),($"{finalScore}",ConsoleColor.Green),(" points",ConsoleColor.White));
            current++;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),($"{total}\n",ConsoleColor.Green));
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "4", "input.txt"));
        int total = 0;
        Dictionary<int,List<int>> winning = new();
        Dictionary<int,List<int>> ourNumbers = new();
        int current = 1;
        foreach (string line in input)
        {
            Regex regex = new Regex(@"(Card +\d+: +)");
            string card = regex.Replace(line,"");
            regex = new Regex(@"( +)");
            card = regex.Replace(card, " ");
            winning.Add(current,card.Split(" | ")[0].Split(" ").Select(x => int.Parse(x)).ToList());
            ourNumbers.Add(current,card.Split(" | ")[1].Split(" ").Select(x => int.Parse(x)).ToList());
            current++;
        }
        List<Card> cards = new();
        for(int i = 1;i<ourNumbers.Count+1;i++) cards.Add(new Card(i,winning[i],ourNumbers[i]));
        List<int> times = new();
        times.Fill(1,ourNumbers.Count);
        Compute(ref total, cards,0,times,1);
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),($"{total}\n",ConsoleColor.Green));
    }
    public void Compute(ref int total, List<Card> cards, int current, List<int> times, int next)
    {
        if(current < cards.Count)
        {
            total++;
            for(int i = 0;i<times[current];i++)
            {
                int wins = cards[current].GetWins();
                total += wins;
                for(int j = 0;j < wins && j < times.Count;j++)
                {
                    times[current+j+1]++;
                }
            }
            Compute(ref total,cards,next,times,next+1);
        }
        else return;
    }
}

public class Card
{
    public int CardNumber {get;private set;}
    public List<int> Winning {get;private set;}
    public List<int> Numbers {get;private set;}

    public Card(int cardNumber, List<int> winning, List<int> numbers)
    {
        CardNumber = cardNumber;
        Winning = winning;
        Numbers = numbers;
    }
    public int GetWins()
    {
        int final = 0;
        foreach (int number in Numbers)
        {
            foreach(int win in Winning)
            {
                if(number == win)
                {
                    final++;
                }
            }
        }
        return final;
    }
}