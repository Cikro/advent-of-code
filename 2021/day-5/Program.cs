// See https://aka.ms/new-console-template for more information
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

int maxX = 0;
int maxY = 0;

var ventPositions = lines.Select(line => {
    var coordArr = line.Split(new String[] { "->", " ", "," }, StringSplitOptions.RemoveEmptyEntries).Select(num => int.Parse(num)).ToList();

    var coord1 = new Coordinate(coordArr[0], coordArr[1]);
    var coord2 = new Coordinate(coordArr[2], coordArr[3]);
    maxX = coord1.x > maxX ? coord1.x : maxX;
    maxX = coord2.x > maxX ? coord2.x : maxX;
    maxY = coord1.y > maxX ? coord1.y : maxY;
    maxY = coord2.x > maxX ? coord2.y : maxY;

    return new VentPosition(coord1, coord2);
})
.ToList();

var map = new int[maxX+1, maxY+1];

foreach(var vent in ventPositions)
{
    var dy = vent.End.y - vent.Start.y;
    var dx = vent.End.x - vent.Start.x;

    int incX = 0;
    int incY = 0;

    if(dx != 0)
    {
        incX = dx < 0 ? -1 : 1;

    }
    if (dy != 0)
    {
        incY = dy < 0 ? -1 : 1;
    }
    
    
    var x = vent.Start.x;
    var y = vent.Start.y;

    while (true)
    {
        if (x == vent.End.x+ incX && y == vent.End.y+ incY)
        {
            break;
        }
        map[y, x] += 1;
        x += incX;
        y += incY;


    }
}

//DrawMap(map);
Console.WriteLine($"Number of dangerous spots {countDangerousSpots(map)}");

static void DrawMap(int[,] map)
{
    for (int i =0; i < map.GetLength(0); i++)
    {
        for(int j =0; j < map.GetLength(1); j++)
        {
            Console.Write(map[i, j]);
        }
        Console.Write("\n");
    }
}

static int countDangerousSpots(int [,] map)
{
    int count = 0;
    for (int i = 0; i < map.GetLength(0); i++)
    {
        for (int j = 0; j < map.GetLength(1); j++)
        {
            count += map[i, j] > 1 ? 1 : 0;
        }
    }
    return count;
}

public struct Coordinate
{
    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int x { get; set; }
    public int y { get; set; }
}

public struct VentPosition
{
    public VentPosition(Coordinate start, Coordinate end)
    {
        this.Start = start;
        this.End = end;
    }

    public Coordinate Start { get; set; }
    public Coordinate End { get; set; }
}