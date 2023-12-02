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
}