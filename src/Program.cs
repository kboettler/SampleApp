using System;

namespace appApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2 ||
                !uint.TryParse(args[1], out uint length))
            {
                Console.WriteLine("Expected two arguments for input string and substring length");
                return;
            }

            var input = args[0];
            foreach (var (substring, count) in Patterns.CountPatterns(input, length))
            {
                Console.WriteLine($"{substring}: {count}");
            }
        }
    }
}
