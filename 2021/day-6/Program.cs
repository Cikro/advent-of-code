using System.Collections;
using System.Diagnostics;

const int TOTAL_DAYS = 256;
const int FISH_RESET_DAY = 6;
const int FISH_NEW_DAY = 8;


if (args.Length != 1)
{
    Console.WriteLine("Error: Expected 1 Arguemnt: inputfile.txt");
    Console.ReadLine();
    Environment.Exit(1);
}
var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
var fileName = args[0];

var lines = File.ReadAllLines(projectDirectory + fileName).ToList();

var fishes = lines[0].Split(',').Select(f => int.Parse(f)).ToList();

//represents day 0 - 8
var days = new long[FISH_NEW_DAY+1];

foreach (var fish in fishes)
{
    days[fish] += 1;
}


for (int i = 0; i < TOTAL_DAYS; i++)
{
    var newFishes = days[0];
    for (int j = 0; j < days.Length-1; j++)
    {
         days[j] = days[j + 1];
    }
    days[days.Length-1] = newFishes;
    days[FISH_RESET_DAY] += newFishes;
}

Console.WriteLine($"Number of Fishes after {TOTAL_DAYS} days: {days.Sum()}");

