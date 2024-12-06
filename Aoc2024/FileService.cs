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
        var grid = new Grid(){items = new Dictionary<(int x, int y), GridCoord?>()};
        var rows = file.Split(Environment.NewLine);
        grid.maxY = rows.Count();
        for (int i = 0; i < rows.Count(); i++)
        {
            var letters = rows[i].ToArray();
            if (i == 0) grid.maxX = letters.Length;
            for (int j = 0; j < letters.Count(); j++)
            {
                grid.items.Add((j,i), new GridCoord(){Coord = (j,i), value = letters[j]});
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