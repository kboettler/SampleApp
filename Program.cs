using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace appApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args[0];
            var length = uint.Parse(args[1]);

            foreach(var (substring, count) in Patterns(input, length))
            {
                Console.WriteLine($"{substring}: {count}");
            }
        }

        private static IEnumerable<string> Substrings(string input, uint substringLength) 
        {
            if(input.Length < substringLength)
            {
                yield break;
            }

            int endIndex = input.Length - (int)substringLength;
            for(int index = 0; index <= endIndex; index++)
            {
                //In using substring, enumerating the full result has O(n+m) complexity
                //where n is the length of the input and m is the substring length.
                //However, unless m is large this can be treated as O(n)
                yield return input.Substring(index, (int)substringLength);
            }
        }

        private static IEnumerable<(string, uint)> Patterns(string input, uint substringLength)
        {
            //using concurrent dictionary saves having to check whether the substring has already been added before counting
            var countMap = new ConcurrentDictionary<string, uint>();

            //as mentioned, Substrings can be considered O(n) assuming small substringLength
            //as Substrings is lazy we only keep the original string and a single instance of each substring in memory
            foreach(var substring in Substrings(input, substringLength))
            {
                countMap.AddOrUpdate(substring, 1, (_, previousCount) => previousCount + 1);
            }
            
            return 
                from count in countMap
                where count.Value > 1
                select (count.Key, count.Value);
        }
    }
}
