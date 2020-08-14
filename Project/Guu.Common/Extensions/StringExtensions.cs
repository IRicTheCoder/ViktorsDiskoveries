using System;
using System.Text.RegularExpressions;

/// <summary>
/// Contains extension methods for strings
/// </summary>
// ReSharper disable once CheckNamespace
public static class StringExtensions
{
    public static string Reverse(this string original)
    {
        string[] division = original.Split(' ');
        string result = "";

        foreach (string value in division)
        {
            if (Regex.IsMatch(value, @"[\d.]+"))
            {
                result += " " + value;
                continue;
            }
            
            char[] chars = value.ToCharArray();
            Array.Reverse(chars);
            result += " " + new string(chars);
        }

        return result.TrimStart();
    }

    public static string FixRTLNumbers(this string original)
    {
        string[] division = original.Split(' ');
        string result = "";

        foreach (string value in division)
        {
            if (!Regex.IsMatch(value, @"[\d.]+"))
            {
                result += " " + value;
                continue;
            }

            char[] chars = value.ToCharArray();
            Array.Reverse(chars);
            result += " " + new string(chars);
        }

        return result.TrimStart();
    }
}