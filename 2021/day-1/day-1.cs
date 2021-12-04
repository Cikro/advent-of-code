using System;
using System.Collections.Generic;
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
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var fileName = args[0];

            var lines = File.ReadAllLines(projectDirectory + fileName).ToList();

           
            
            

            Console.WriteLine($"Beginning regular increases");

            Console.WriteLine($"Number of increases {regularIncreases(lines)}");


            Console.WriteLine($"Beginning Sliding Window Increases");
            Console.WriteLine($"Number of increases {slidingWindowIncreases(lines, 3)}");


            Console.WriteLine("Exiting...");
            Console.ReadKey();
        }

        public static int regularIncreases(List<string> lines)
        {
            int? previousDepth = int.Parse(lines[0]);
            int numIncreases = 0;

            // element 0 has already been read
            for (int i = 1; i<lines.Count; i++)
            {
                var currentDepth = int.Parse(lines[i]);

                if (currentDepth > previousDepth)
                {
                    numIncreases++;
                }
                previousDepth = currentDepth;
            }
            return numIncreases;
        }


        public static int slidingWindowIncreases(List<string> lines, int windowSize)
        {
            
            int previousWindowSum = 0;
            int numIncreases = 0;

            Queue<int> windowQueue = new Queue<int>();

            for (int i = 0; i < windowSize; i++)
            {
                var depth = int.Parse(lines[i]);
                windowQueue.Enqueue(depth);
                previousWindowSum += depth;
            }


            int lineNum = windowSize;
            while (lineNum < lines.Count )
            {
                int depth = int.Parse(lines[lineNum]);
                var currentWindowSum = previousWindowSum - windowQueue.Dequeue() + depth;
                windowQueue.Enqueue(depth);


                if (currentWindowSum > previousWindowSum)
                {
                    numIncreases++;
                }
                previousWindowSum = currentWindowSum;
                lineNum++;
            }
            return numIncreases;
        }


    }
}