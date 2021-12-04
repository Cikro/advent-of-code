using System;
using System.Collections.Generic;
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

            var sub = new Submarine();
            Console.WriteLine("beginning naviation");
            Console.WriteLine($"Sub Position: {sub.HorizontalPosition}, Sub Depth: {sub.Depth}");
            foreach (var line in lines)
            {
                var commandText = line.Split(new[] { ' ' });

                var command = new Command
                {
                    direction = (Direction)Enum.Parse(typeof(Direction), commandText[0]),
                    unit = int.Parse(commandText[1])
                };
                Console.WriteLine($"Navigating.... Direction: {command.direction} | Unit: {command.unit}");
                sub.Navigate(command);
                Console.WriteLine($"Sub Position: {sub.HorizontalPosition}, Sub Depth: {sub.Depth}");
            }

            Console.WriteLine($"Final Sub Position: {sub.HorizontalPosition}, Sub Depth: {sub.Depth}");
            Console.ReadKey();

        }
    }

    class Submarine
    {
        public int HorizontalPosition { get; set; }
        public int Depth { get; set; }

        public Submarine(int depth = 0, int position = 0)
        {
            this.HorizontalPosition = position;
            this.Depth = depth;
        }

        public void Navigate(Command command)
        {
            switch(command.direction)
            {
                case Direction.forward:
                    this.HorizontalPosition += command.unit;
                break;
                case Direction.up:
                    this.Depth -= command.unit;
                break;
                case Direction.down:
                    this.Depth += command.unit;
                break;

            }

        }
    }

    struct Command
    {
        public Direction direction;
        public int unit;
    }

    enum Direction
    {
        forward = 0,
        down,
        up

    }
}
