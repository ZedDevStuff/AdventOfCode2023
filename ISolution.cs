using System.Diagnostics;

public interface ISolution
{
    public string Name { get; }
    public void Run()
    {
        Console.WriteLine("Which part?");
        bool correct = false;
        while(!correct)
        {
            string input = Console.ReadLine().Trim();
            if(input == "1")
            {
                correct = true;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Part1();
                sw.Stop();
                Console.WriteLine($"Time taken: {sw.Elapsed}");
            }
            if(input == "2")
            {
                correct = true;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Part2();
                sw.Stop();
                Console.WriteLine($"Time taken: {sw.Elapsed}");
            }
        }
    }
    public void Part1();
    public void Part2();
}