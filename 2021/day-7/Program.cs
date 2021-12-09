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

var maxPosition = startingCrabPositions[startingCrabPositions.Count - 1].Key;

int[] crabPositions = new int[maxPosition + 1];
int[] distanceArray = new int[maxPosition+1];

for (int i = 1; i < distanceArray.Length; i++)
{
    distanceArray[i] = distanceArray[i-1] + i;

}


foreach (var grp in startingCrabPositions)
{
    crabPositions[grp.Key] += grp.Count();
}

var minFuel = int.MaxValue;
for (int i = 0; i < crabPositions.Length; i++)
{
   var fuel = calculateFuelAtPosition(i, crabPositions, distanceArray);

    if (fuel < minFuel)
    {
        minFuel = fuel;
    }
}


Console.WriteLine($"MinFuel: {minFuel}");

int calculateFuelAtPosition(int destination, int[] crabPositions, int[] distanceArray)
{
    int rightFuel = 0;
    int leftFuel = 0;
    for (var i = 0; i < crabPositions.Length; i++)
    {

        if (i < destination)
        {
            leftFuel += crabPositions[i] * distanceArray[destination - i];
        }
        else if (i > destination)
        {
            rightFuel += crabPositions[i] * distanceArray[i - destination];
        }
    }

    return rightFuel + leftFuel;

}