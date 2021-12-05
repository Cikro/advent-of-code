using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    static class Day2
    {

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Error: Expected 1 Arguemnt: inputfile.txt");
                Console.ReadLine();
                Environment.Exit(1);
            }
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var fileName = args[0];

            var lines = File.ReadAllLines(projectDirectory + fileName).ToList();

            var lineLength = lines[0].Length;
            // 5 bits in the diagonostic report
            

            
            int[] bitSum = getMostCommonBits(lines);

            uint gamma= 0;
            int p = lineLength - 1;
            for (int i = 0; i < lineLength; i++, p--)
            {
                // There can only be a 1 or a 0 in the columns. 
                // therfore, if there are more 1s thans 0s, there are over 50% ones.
                if (bitSum[i] > (lines.Count/2))
                {
                    gamma += Convert.ToUInt32(Math.Pow(2,p));
                }

            }


            uint episolon = gamma ^ Convert.ToUInt32(Math.Pow(2, lineLength) - 1);

            Console.WriteLine($"gamma is: {gamma}.\n episolon is {episolon}");
            Console.WriteLine($"Power Consumption is: {gamma * episolon}.");
            Console.WriteLine($"Life Support Rating is: {getlifeSupportRating(lines, bitSum)}.");
            Console.ReadKey();

        }

        public static int[] getMostCommonBits(List<string> lines)
        {
            int[] bitSum = new int[lines[0].Length];
            foreach (var line in lines)
            {
                for (int i = 0; i < lines[0].Length; i++)
                {
                    // Assume file is only binary input i.e. 1 or 0..
                    bitSum[i] += (int)char.GetNumericValue(line[i]);
                }
            }
            
            return bitSum;
        }

        public static int getlifeSupportRating(List<string> lines, int[] bitSum)
        {

            Func<int, double, int> mostCommonBit = (sumOfOnes, halfLines) =>
            {
                // if more than half the bits are ones, 1 is most common, keep 1.
                // else, 0 is more common, keep 0.
                return sumOfOnes >= halfLines ? 1 : 0;
            };

            Func<int, double, int> leastCommonBit = (sumOfOnes, halfLines) =>
            {
                // if more than half the bits are ones, 1 is most common, 0 is least common. Keep 0.
                // else, 0 is more common, 1 is least common. Keep 1.
                return sumOfOnes >= halfLines ? 0 : 1;
            };

            var keepForOxy = mostCommonBit(bitSum[0], lines.Count / 2.0);
            var keepForCo2 = leastCommonBit(bitSum[0], lines.Count / 2.0);


            // oxyPartial is mostCommonBit
            List<string> oxyPartial = lines.Where(line => 
                (int)char.GetNumericValue(line[0]) == keepForOxy
            ).ToList();

            // cO2Partial is leastCommonBit
            List<string> co2Partial = lines.Where(line =>
                (int)char.GetNumericValue(line[0]) == keepForCo2
            ).ToList();


            List<string> oxyRatingList = parseLifeSupportPartial(oxyPartial, 1, mostCommonBit);
            List<string> co2ScrubberRatingList = parseLifeSupportPartial(co2Partial, 1, leastCommonBit);

            Debug.WriteLine($"OxyRating is: {Convert.ToInt32(oxyRatingList[0], 2)}.\nCO2 Ratings is: {Convert.ToInt32(co2ScrubberRatingList[0], 2)}");

            return Convert.ToInt32(oxyRatingList[0], 2) * Convert.ToInt32(co2ScrubberRatingList[0], 2);
            
        }

        public static List<string> parseLifeSupportPartial(List<string> lines, int position, Func<int, double, int> keepBits)
        {
            if (lines.Count <= 1 || position >= lines[0].Length)
            {
                return lines;
            }

            // Get Most commmon bits @ position
            var bitSum = getMostCommonBits(lines);

            // filter to line items with the most common bit
            var bitToKeep = keepBits(bitSum[position],(lines.Count / 2.0));
            var filtered = lines.Where(line =>
                (int)char.GetNumericValue(line[position]) == bitToKeep
            ).ToList();

            position++;
            return parseLifeSupportPartial(filtered, position, keepBits);
            

        }


    }
}
