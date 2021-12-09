using System.Diagnostics;

if (args.Length != 1)
{
    Console.WriteLine("Error: Expected 1 Arguemnt: inputfile.txt");
    Console.ReadLine();
    Environment.Exit(1);
}
var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
var fileName = args[0];

var lines = File.ReadAllLines(projectDirectory + fileName).ToList();

var startingCrabPositions = lines[0].Split(",").Select(x => int.Parse(x)).GroupBy(i => i).OrderBy(grp => grp.Key).ToList();


int[] positions = new int[startingCrabPositions[startingCrabPositions.Count-1].Key+1];


foreach (var grp in startingCrabPositions)
{
    positions[grp.Key] += grp.Count();
}

var minFuel = int.MaxValue;
var currentDirection = direction.right;

var currentSpace = positions.Length / 2;
var prevSpave = currentSpace;
var fuel = 0;

var leftFuel = 0;
var leftCrabs = 0;

var rightFuel = 0;
var rightCrabs = 0;

// Calcualte total fuel to on the left side and the right side of teh current space
for (var i = 0; i < positions.Length; i++)
{
    if (i < currentSpace)
    {
        leftCrabs += positions[i];
        leftFuel += positions[i] * (currentSpace - i);
    }
    else if (i > currentSpace)
    {
        rightCrabs += positions[i];
        rightFuel += positions[i] * (i - currentSpace);
    }
}

fuel = leftFuel + rightFuel;


while (fuel < minFuel)
{
    prevSpave = currentSpace;
    minFuel = fuel;

    // move 1 step towards the most crabs
    // More Left Crabs
    if (leftCrabs > rightCrabs)
    {
        leftCrabs -= positions[currentSpace - 1];
        rightCrabs += positions[currentSpace];
        fuel = fuel + rightCrabs - leftCrabs - positions[currentSpace - 1];
        currentSpace--;
    }
    // More Right Crabs
    else
    {
        rightCrabs -= positions[currentSpace + 1];
        leftCrabs += positions[currentSpace];
        fuel = fuel - rightCrabs + leftCrabs - positions[currentSpace + 1]; ;
        currentSpace++;
    }
}


Console.WriteLine($"LeftCrabs: {leftCrabs}");
Console.WriteLine($"rightCrabs: {rightCrabs}");
Console.WriteLine($"currentPosition: {prevSpave}");
Console.WriteLine($"MinFuel: {minFuel}");



static int calcFuelUsed(int initialPos, int newPosition)
{
    var distance = Math.Abs(newPosition - initialPos);
    return 0;
}
