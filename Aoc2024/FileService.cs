using System.Security.Cryptography.X509Certificates;
using Aoc2024.Interfaces;

namespace Aoc2024;

public class FileService(int day) : IFileService
{
    public string LoadFile()
    {
        var filename = "input.txt";
        if (Environment.GetEnvironmentVariable("test") == "true") filename = "testInput.txt";
        return File.ReadAllText($"Days/{day}/{filename}");
    }
    public string[] LoadLines()
    {
        var file = LoadFile();
        return file.Split(Environment.NewLine).Where(x => !String.IsNullOrEmpty(x)).ToArray();
    }
    public IEnumerable<IEnumerable<int>> LoadIntLines()
    {
        var file = LoadFile();
        var lines = file.Split(Environment.NewLine);
        var intLines = lines.Select(x => x.Split(" ").Select(y => int.Parse(y)));
        return intLines;
    }
    
    public Grid LoadGrid()
    {
        var file = LoadFile();
        var grid = new Grid(){};
        var rows = file.Split(Environment.NewLine);
        grid.maxY = rows.Count();
        for (int i = 0; i < rows.Count(); i++)
        {
            var letters = rows[i].ToArray();
            if (i == 0)
            {
                grid.maxX = letters.Length;
                grid.items = new GridCoord[grid.maxX, grid.maxY];
            }
            for (int j = 0; j < letters.Count(); j++)
            {
                grid.items[j,i] =  new GridCoord(){Coord = (j,i), value = letters[j]};
                if (letters[j] == '^') grid.Start = grid.items[j, i];

            }
        }
        return grid;
    }
    
}

public class GridCoord
{
    public (int x, int y) Coord;
    public char value;
    public char Direction;
}

public class StolenCode
{
    public static string ToBase(int input)
    {
        int dividend = input+1;
        string output = String.Empty;
        int modulo;

        while (dividend > 0)
        {
            modulo = (dividend - 1) % 3;
            output = Convert.ToChar('A' + modulo).ToString() + output;
            dividend = (int)((dividend - modulo) / 3);
        } 

        return output;
    }
}