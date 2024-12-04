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
        var grid = new Grid(){items = new List<GridCoord?>()};
        var rows = file.Split(Environment.NewLine);
        for (int i = 0; i < rows.Count(); i++)
        {
            var letters = rows[i].ToArray();
            for (int j = 0; j < letters.Count(); j++)
            {
                grid.items.Add(new GridCoord(){Coord = (j,i), value = letters[j]});
            }
        }

        return grid;
    }
}

public class GridCoord
{
    public (int x, int y) Coord;
    public char value;
}
public class Grid
{
    public List<GridCoord?> items;

    public GridCoord? GetLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x - 1 && x.Coord.y == coord.Coord.y);
    }
    public GridCoord? GetRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y);
    }
    public GridCoord? GetUp(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetDown(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x && x.Coord.y == coord.Coord.y + 1);
    }
    public GridCoord? GetUpLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x - 1 && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetUpRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetDownRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y + 1);
    }
    public GridCoord? GetDownLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x -1 && x.Coord.y == coord.Coord.y + 1);
    }
}