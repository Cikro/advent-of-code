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

var bingoBoards = new List<BingoBoard>();

var calledNumbers = lines[0].Split(',').Select(numStr => int.Parse(numStr)).ToList();
lines.RemoveAt(0);


var currentBoard = new BingoBoard();
foreach (var line in lines)
{
    if(line.Count() == 0) {
        if (currentBoard.NumRows > 0) { bingoBoards.Add(currentBoard); }
        currentBoard = new BingoBoard();
        continue;
    }
    currentBoard.AddRow(line);
}

if (currentBoard.NumRows > 0) { bingoBoards.Add(currentBoard); }

BingoBoard? firstWinningBoard = null;
BingoBoard? mostRecentWinningBoard = null;

foreach (var number in calledNumbers)
{
    foreach (var board in bingoBoards)
    {
        // Only mark number if board has not won yet.
        if (board.HasBingo) { continue; }

        board.MarkNumber(number);
        if (board.HasBingo)
        {
            if (firstWinningBoard == null) { firstWinningBoard = board; }
            mostRecentWinningBoard = board;
            Debug.WriteLine($"A Board won when {number} was called.");
        }
    }
}
if (firstWinningBoard != null)
{
    Console.WriteLine($"The Score of the first winning board is { firstWinningBoard.CurrentScore }");
}

if (mostRecentWinningBoard != null)
{
    Console.WriteLine($"The Score of the last winning board is { mostRecentWinningBoard.CurrentScore }");
}






Console.ReadKey();


public class BingoBoard
{

    public bool HasBingo { get; private set; } = false;
    public int NumRows { get; set; }

    public List<List<BingoSquare>> Board { get; set; }

    public int CurrentScore = 0;

    public BingoBoard()
    {
        Board = new List<List<BingoSquare>>(); 
    }

    public void AddRow(string bingoRow)
    {
        var rowOfNums = bingoRow.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(numStr => {
            return new BingoSquare(int.Parse(numStr));
        }).ToList();
        Board.Add(rowOfNums);
        NumRows++;
    }


    public void MarkNumber(int calledNumber)
    {
        var anyMarked = true;
        var unmarkedSum = 0;
        foreach (var (row, i) in Board.WithIndex())
        {
            foreach (var (square, j) in row.WithIndex())
            {
                if (square.Marked) { continue; }

                if (square.Number == calledNumber)
                {
                    square.Marked = true;
                    anyMarked = true;
                } 

                if(!square.Marked)
                {
                    unmarkedSum += square.Number;
                }
            }
        }
        if (anyMarked)
        {
            CheckForBingo();
            if (HasBingo)
            {
                CurrentScore = unmarkedSum * calledNumber;
            }
        }
    }

    private void CheckForBingo()
    {
        if (CheckRowsForBingo() || CheckColsForBingo())
        {
            HasBingo = true;
        }
    }

    private bool CheckRowsForBingo()
    {
        bool isBingo = true;
        for (int i = 0; i < NumRows; i++)
        {
            isBingo = true;
            for (int j = 0; j < Board[i].Count; j++)
            {
                if (!Board[i][j].Marked)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                return true;
            }
        }
        return false;

    }


    private bool CheckColsForBingo()
    {
        bool isBingo = true;
        for (int j = 0; j < Board[0].Count; j++)
        {
            isBingo = true;
            for (int i = 0; i < NumRows; i++)
            {
                if (!Board[i][j].Marked)
                {
                    isBingo = false;
                    break;
                }
            }

            if (isBingo)
            {
                return true;
            }
        }
        return false;

    }
}

public class BingoSquare
{
    public int Number { get; set; }
    public bool Marked { get; set; } = false;

    public BingoSquare(int number)
    {
        Number = number;
    }
    
 }

public static class EnumerableExtenstions
{
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
    {
        return source.Select((item, index) => (item, index));
    }

}