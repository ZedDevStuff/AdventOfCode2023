using System.Collections.Concurrent;
using Utility;

public class Day7 : ISolution
{
    public string Name => "Day 7: Camel Cards";

    public static Dictionary<char,int> Part1CardStrength = new()
    {
        { 'A', 13 },
        { 'K', 12 },
        { 'Q', 11 },
        { 'J', 10 },
        { 'T', 9 },
        { '9', 8 },
        { '8', 7 },
        { '7', 6 },
        { '6', 5 },
        { '5', 4 },
        { '4', 3 },
        { '3', 2 },
        { '2', 1 }
    };
    public static Dictionary<char,int> Part2CardStrength = new()
    {
        { 'A', 13 },
        { 'K', 12 },
        { 'Q', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { 'J', 1 }
    };

    public Comparison<Hand> Part1HandComparer = (x,y) =>
    {
        return x.IsBetterThan(y,Part1CardStrength);
    };
    public Comparison<Hand> Part2HandComparer = (x,y) =>
    {
        return x.Part2IsBetterThan(y,Part2CardStrength);
    };

    public void Part1()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "7", "input.txt"));
        List<Hand> hands = new();
        foreach(var line in input)
        {
            string[] split = line.Split(' ');
            hands.Add(new Hand(split[0].ToCharArray(), int.Parse(split[1])));
        }
        hands.Sort(Part1HandComparer);
        int total = 0;
        foreach(int i in Quick.Range(0,hands.Count-1))
        {
            total += (i+1)*hands[i].Bid;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),(total,ConsoleColor.Green));
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines(Path.Combine(Program.DataPath, "7", "input.txt"));
        List<Hand> hands = new();
        foreach(var line in input)
        {
            string[] split = line.Split(' ');
            hands.Add(new Hand(split[0].ToCharArray(), int.Parse(split[1])));
        }
        hands.Sort(Part2HandComparer);
        int total = 0;
        foreach(int i in Quick.Range(0,hands.Count-1))
        {
            total += (i+1)*hands[i].Bid;
        }
        ConsoleEx.WriteLineColor(("\nTotal: ",ConsoleColor.White),(total,ConsoleColor.Green));
    }
    public struct Hand
    {
        public char[] Cards = new char[5];
        Dictionary<char,int> CardCount = new();
        public int Bid;
        public HandName Name => GetHandName();
        public HandName Part2Name 
        {
            get
            {
                if(Cards.Contains('J'))
                {
                    int js = CardCount['J'];
                    if(js == 5) return HandName.FiveOfAKind;
                    else if(js == 4) return HandName.FiveOfAKind;
                    else if(js == 3)
                    {
                        // JJJAA, JJJAB
                        if(CardCount.Count == 2) return HandName.FiveOfAKind;
                        else if(CardCount.Count == 3) return HandName.FullHouse;
                    }
                    else if(js == 2)
                    {
                        // JJAAA, JJABB, JJABC
                        if(CardCount.Count == 2) return HandName.FiveOfAKind;
                        else if(CardCount.Count == 3) return HandName.FourOfAKind;
                        else if(CardCount.Count == 4) return HandName.ThreeOfAKind;
                    }
                    else if(js == 1)
                    {
                        if(CardCount.Count == 2) return HandName.FiveOfAKind;
                        else if(CardCount.Count == 3)
                        {
                            // JAAAA, JAAAB, JAABB
                            foreach(var card in CardCount)
                            {
                                if(card.Value == 3) return HandName.FourOfAKind;
                                else if(card.Value == 2) return HandName.FullHouse;
                            }
                        }
                        else if(CardCount.Count == 4)
                        {
                            // JAABC
                            return HandName.ThreeOfAKind;
                        }
                        else if(CardCount.Count == 5) return HandName.OnePair;
                    }
                    return HandName.HighCard;
                }
                else
                {
                    return Name;
                }
            }
        }
        public int GetStrength(Dictionary<char,int> cardStrength)
        {
            int strength = 0;
            foreach(var card in Cards)
            {
                strength += cardStrength[card];
            }
            return strength;
        }

        public Hand(char[] cards, int bid)
        {
            Cards = cards;
            Bid = bid;
            foreach(char card in Cards)
            {
                if(CardCount.ContainsKey(card))
                {
                    CardCount[card]++;
                }
                else
                {
                    CardCount.Add(card, 1);
                }
            }
            CardCount = CardCount.OrderByDescending(x => x.Value).ToDictionary();
        }

        public HandName GetHandName()
        {
            int kinds = CardCount.Count();
            if(kinds == 1) return HandName.FiveOfAKind;
            if(kinds == 2)
            {
                if(CardCount.ElementAt(0).Value == 1 || CardCount.ElementAt(0).Value == 4) return HandName.FourOfAKind;
                else return HandName.FullHouse;
            }
            if(kinds == 3)
            {
                if(CardCount.ElementAt(0).Value == 3 || CardCount.ElementAt(1).Value == 3 || CardCount.ElementAt(2).Value == 3) return HandName.ThreeOfAKind;
                else return HandName.TwoPair;
            }
            if(kinds == 4) return HandName.OnePair;
            return HandName.HighCard;
        }

        public int IsBetterThan(Hand hand, Dictionary<char,int> cardStrength)
        {
            if((int)hand.Name < (int)Name)
            {
                return 1;
            }
            else if((int)hand.Name > (int)Name)
            {
                return -1;
            }
            else
            {
                foreach(int i in Quick.Range(0,4))
                {
                    if(cardStrength[Cards[i]] > cardStrength[hand.Cards[i]]) return 1;
                    else if(cardStrength[Cards[i]] < cardStrength[hand.Cards[i]]) return -1;
                    else continue;
                }
            }
            return 0;
        }
        public int Part2IsBetterThan(Hand hand, Dictionary<char,int> cardStrength)
        {
            if((int)hand.Part2Name < (int)Part2Name)
            {
                return 1;
            }
            else if((int)hand.Part2Name > (int)Part2Name)
            {
                return -1;
            }
            else
            {
                foreach(int i in Quick.Range(0,4))
                {
                    if(cardStrength[Cards[i]] > cardStrength[hand.Cards[i]]) return 1;
                    else if(cardStrength[Cards[i]] < cardStrength[hand.Cards[i]]) return -1;
                    else continue;
                }
            }
            return 0;
        }
    }

    public enum HandName
    {
         // So apparently just changing the value of the first one shifts all the others? That's handy
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
}