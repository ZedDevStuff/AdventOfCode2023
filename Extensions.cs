using System.Text.RegularExpressions;

public static class Extensions
{

    /// <summary>
    ///  This extension isn't great but it works for my uses
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ParseInt(this string s)
    {
        string result = "";
        foreach(char c in s)
        {
            result += char.IsDigit(c) ? c : "";
        }
        return int.Parse(result);
    }

    // This was a char[][] but I wanted to make it more generic for absolutely no reason
    public static bool HasNeighbor<T>(this T[][] map, int x, int y, params T[] neighbors)
    {
        int xStart = x == 0 ? 0 : x - 1;
        int xEnd = x == map[0].Length - 1 ? x : x + 1;
        int yStart = y == 0 ? 0 : y - 1;
        int yEnd = y == map.Length - 1 ? y : y + 1;

        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {
                if (i == x && j == y) continue;
                foreach (T neighbor in neighbors)
                {
                    if (map[j][i].Equals(neighbor))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Useful for day 3 part 2 but i literally can't think of any other use case lol
    public static bool HasNeighborsOut<T>(this T[][] map, int x, int y, out (int x,int y)[] neighboorsCoords, params T[] neighbors)
    {
        int xStart = x == 0 ? 0 : x - 1;
        int xEnd = x == map[0].Length - 1 ? x : x + 1;
        int yStart = y == 0 ? 0 : y - 1;
        int yEnd = y == map.Length - 1 ? y : y + 1;

        List<(int x, int y)> NeighboorsCoords = new List<(int x, int y)>() {};

        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {
                if (i == x && j == y) continue;
                foreach (T neighbor in neighbors)
                {
                    if (map[j][i].Equals(neighbor))
                    {
                        NeighboorsCoords.Add((i,j));
                    }
                }
            }
        }
        neighboorsCoords = NeighboorsCoords.ToArray();
        if (NeighboorsCoords.Count > 0)
        {
            return true;
        }
        else return false;
    }
    public static void InitializeJaggedArray(this Array[] array, int x, int y)
    {
        array = new Array[y][];
        for (int i = 0; i < y; i++)
        {
            array[i] = new Array[x];
        }
    }
    public static void Fill<T>(this List<T> list,int count, Func<int,T> func)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(func(i));
        }
    }
    public static void ForEach<TKey,TValue>(this Dictionary<TKey,TValue> dict, Action<KeyValuePair<TKey,TValue>> action)
    {
        foreach (var item in dict)
        {
            action(item);
        }
    }
    public static void RemoveAll<TKey,TValue>(this Dictionary<TKey,TValue> dict, Func<KeyValuePair<TKey,TValue>,bool> func)
    {
        foreach (var item in dict.Where(func).ToList())
        {
            dict.Remove(item.Key);
        }
    }
    public static void Fill<T>(this List<T> list, T value, int count)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(value);
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