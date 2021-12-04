using System;
using System.IO;
using System.Linq;

namespace AdventOfCode {
    class Day1 {
        static void Main(string[] args) {

            if (args.Length != 1)
            {
                Console.WriteLine("Error: Expected 1 Arguemnt: inputfile.txt");
                Console.ReadLine();
                Environment.Exit(1);
            }

            var fileName = args[0];
            var lines = File.ReadAllLines(fileName).ToList();

            Console.WriteLine(args[0]);

            int? previousDepth = int.Parse(lines[0]);
            lines.RemoveAt(0);

            int numIncreases = 0;

            foreach (var line in  lines ) {
                var currentDepth = int.Parse(line);

                if (currentDepth > previousDepth)
                {
                    numIncreases++;
                }       
                previousDepth = currentDepth;
            }

            Console.WriteLine($"Number of increases {numIncreases}");
            Console.WriteLine("Exiting...");
            Console.ReadLine();

        }


    }
}