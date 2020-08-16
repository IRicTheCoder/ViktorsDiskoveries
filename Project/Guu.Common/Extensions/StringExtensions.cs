using System;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Contains extension methods for strings
/// </summary>
// ReSharper disable once CheckNamespace
public static class StringExtensions
{
    /// <summary>
    /// Reverses the string
    /// </summary>
    public static string Reverse(this string original)
    {
        string[] division = original.Split(' ');
        string result = "";

        foreach (string value in division)
        {
            // Currently compatible with Hebrew, Arabic and Syriac Characters
            if (!Regex.IsMatch(value, @"[\u0591-\u05F4]+|[\u060C-\u06FE\uFB50-\uFDFF\uFE70-\uFEFE]+|[\u0700-\u074A]+|[\u0780-\u07B0]+"))
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

    /// <summary>
    /// Fixes the string that came from translation
    /// </summary>
    public static string FixTranslatedString(this string toFix)
    {
        return toFix.TrimStart()
                    .TrimStart('"')
                    .TrimEnd('"')
                    .Replace("\\n", "\n")
                    .Replace("\\\"", "\"");;
    }
}