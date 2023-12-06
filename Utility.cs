namespace Utility
{
    public static class Quick
    {
        public static IntRange Range(int start, int end)
        {
            return new IntRange(start, end);
        }
    }
    public static class ConsoleEx
    {
        public static void WriteColor(object message ,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteColor(params (object message ,ConsoleColor color)[] values)
        {
            foreach (var (message, color) in values)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineColor(object message ,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteLineColor(params (object message ,ConsoleColor color)[] values)
        {
            foreach (var (message, color) in values)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
            }
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public class LongRange
    {
        private readonly long _start;
        private readonly long _end;
        public long Start => _start;
        public long End => _end;
        public long Length => (_end - _start - 1) < 0 ? (_end - _start - 1) * -1 : (_end - _start - 1);

        public LongRange(long start, long end)
        {
            _start = start;
            _end = end;
        }
        public bool Overlaps(LongRange range)
        {
            return IsWithin(range.Start) || IsWithin(range.End);
        }
        public bool IsWithin(long value)
        {
            return value >= _start && value <= _end;
        }
    }
    public class IntRange : IEnumerable<int>
    {
        private readonly int _start;
        private readonly int _end;
        public int Start => _start;
        public int End => _end;
        public int Length => (_end - _start) < 0 ? (_end - _start) * -1 : (_end - _start);

        public IntRange(int start, int end)
        {
            _start = start;
            _end = end;
            for (int i = start; i <= end; i++)
            {
                this.Append(i);
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = _start; i <= _end; i++)
            {
                yield return i;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}