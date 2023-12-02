using System.Reflection;

public class Program
{
    // This is horrid
    public static readonly string DataPath = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName,"Data");
    private static bool Running = true;
    public static void Main(string[] args)
    {
        List<ISolution> programs = new();
        foreach(Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if(type.GetInterface(nameof(ISolution)) != null)
            {
                programs.Add((ISolution)Activator.CreateInstance(type));
            }
        }

        while(Running)
        {
            Console.WriteLine("Choose a program to run (number):\n");
            int p = 1;
            foreach(ISolution program in programs)
            {
                Console.WriteLine($"    {p++}. {program.Name}");
            }
            int input = GetNumber(Console.ReadLine());
            if(input == -1 || input > programs.Count)
            {
                Console.WriteLine("Invalid input.\n");
                continue;
            }
            if(input == -2)
            {
                Console.WriteLine("Please enter a number.\n");
                continue;
            }
            programs[input-1].Run();
        }
    }

    public static int GetNumber(string num)
    {
        if(num == null) return -2;
        if (int.TryParse(num, out int result))
        {
            return result;
        }
        else return -1;
    }
}
