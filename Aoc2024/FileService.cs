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
        for (int y = 0; y < grid.maxY; y++)
        {
            var letters = rows[y].ToArray();
            if (y == 0)
            {
                grid.maxX = letters.Length;
                grid.items = new GridCoord[grid.maxX, grid.maxY];
            }
            for (int x = 0; x < letters.Count(); x++)
            {
                grid.items[x,y] =  new GridCoord(){Coord = (x,y), value = letters[x]};
                if (letters[x] == '^') grid.Start = grid.items[x, y];

            }
        }
        return grid;
    }
    
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