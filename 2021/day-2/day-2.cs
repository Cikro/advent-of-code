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

            var sub = new Submarine(new CommandExecuterV2());
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
                Debug.WriteLine($"Navigating.... Direction: {command.direction} | Unit: {command.unit}");
                sub.Navigate(command);
                Debug.WriteLine($"Sub Position: {GetSubString(sub)}");
            }

            Console.WriteLine($"Final Sub Position: {GetSubString(sub)}");
            Console.ReadKey();

        }
        public static string GetSubString(Submarine sub)
        {
            return $"Sub Position: {sub.HorizontalPosition}, Sub Depth: {sub.Depth} SubAim: {sub.Aim}";
        }
    }

    public class Submarine
    {
        public int HorizontalPosition { get; set; }
        public int Depth { get; set; }

        public int Aim { get; set; }

        private readonly ISubmarineCommandExecutor _commandExecuter;

        public Submarine(ISubmarineCommandExecutor commandExecutor, int depth = 0, int position = 0, int aim = 0)
        {
            _commandExecuter = commandExecutor;
            this.HorizontalPosition = position;
            this.Depth = depth;
            this.Aim = aim;
        }

        public void Navigate(Command command)
        {
            _commandExecuter.Execute(this, command);
        }
    }

    public interface ISubmarineCommandExecutor
    {
        void Execute(Submarine sub, Command command);
    }

    public class CommandExecuterV1: ISubmarineCommandExecutor
    {
        public void Execute(Submarine sub, Command command)
        {
            switch (command.direction)
            {
                case Direction.forward:
                    sub.HorizontalPosition += command.unit;
                    break;
                case Direction.up:
                    sub.Depth -= command.unit;
                    break;
                case Direction.down:
                    sub.Depth += command.unit;
                    break;
            }
        }
    }
    public class CommandExecuterV2: ISubmarineCommandExecutor
    {
        public void Execute(Submarine sub, Command command)
        {
            switch (command.direction)
            {
                case Direction.forward:
                    sub.HorizontalPosition += command.unit;
                    sub.Depth += (sub.Aim * command.unit);
                    break;
                case Direction.up:
                    sub.Aim -= command.unit;
                    break;
                case Direction.down:
                    sub.Aim += command.unit;
                    break;
            }
        }
    }

    public struct Command
    {
        public Direction direction;
        public int unit;
    }

    public enum Direction
    {
        forward = 0,
        down,
        up

    }
}
