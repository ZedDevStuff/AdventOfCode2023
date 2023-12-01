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
                Part1();
            }
            if(input == "2")
            {
                correct = true;
                Part2();
            }
        }
    }
    public void Part1();
    public void Part2();
}