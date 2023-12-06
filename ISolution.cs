using System.Diagnostics;
using Utility;

public interface ISolution
{
    public string Name { get; }
    public void Run()
    {
        Console.Write("\nWhich part?\n\n> ");
        bool correct = false;
        while(!correct)
        {
            string input = Console.ReadLine().Trim();
            if(input == "1")
            {
                Console.Clear();
                correct = true;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Part1();
                sw.Stop();
                ConsoleEx.WriteLineColor(("Time taken: ",ConsoleColor.White),($"{sw.Elapsed}\n",ConsoleColor.Green));
            }
            else if(input == "2")
            {
                Console.Clear();
                correct = true;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Part2();
                sw.Stop();
                ConsoleEx.WriteLineColor(("Time taken: ",ConsoleColor.White),($"{sw.Elapsed}\n",ConsoleColor.Green));
            }
            else Console.Write("> ");
        }
    }
    public void Part1();
    public void Part2();
}