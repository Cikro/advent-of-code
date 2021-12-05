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
            int[] bitSum =  new int[lineLength];


            foreach (var line in lines)
            {
                for(int i = 0; i < lineLength; i++)
                {
                    // Assume file is only binary input i.e. 1 or 0..
                    bitSum[i] += (int)char.GetNumericValue(line[i]);
                }
            }

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
            Console.ReadKey();

        }
    }
}
