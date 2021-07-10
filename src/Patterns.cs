using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

public static class Patterns
{
    public static IEnumerable<string> Substrings(string input, uint substringLength)
    {
        //while possibly valid depending on the use case, retrieving all substrings of length 0
        //or calling substrings on an empty list are likely not useful/intended
        //To aid catching incorrect usage, another alternative would be to throw an
        //exception for such inputs (fail fast)
        if (!input.Any() ||
            substringLength == 0 ||
            input.Length < substringLength)
        {
            yield break;
        }

        int endIndex = input.Length - (int)substringLength;
        for (int index = 0; index <= endIndex; index++)
        {
            //In using string.Substring, enumerating the full result has O(n+m) complexity
            //where n is the length of the input and m is the substring length.
            //However, unless m is large this can be treated as O(n)
            yield return input.Substring(index, (int)substringLength);
        }
    }

    public static IEnumerable<(string, uint)> CountPatterns(string input, uint substringLength)
    {
        //using concurrent dictionary saves having to check whether the substring has already been added before counting
        var countMap = new ConcurrentDictionary<string, uint>();

        //as mentioned, Patterns.Substrings can be considered O(n) assuming small substringLength
        //as Substrings is lazy we only keep the original string and a single instance of each substring in memory
        foreach (var substring in Substrings(input, substringLength))
        {
            countMap.AddOrUpdate(substring, 1, (_, previousCount) => previousCount + 1);
        }

        return
            from count in countMap
            where count.Value > 1
            select (count.Key, count.Value);
    }
}