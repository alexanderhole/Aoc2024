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
        return file.Split(Environment.NewLine).Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }

    public IEnumerable<IEnumerable<int>> LoadIntLines()
    {
        var file = LoadFile();
        var lines = file.Split(Environment.NewLine);
        var intLines = lines.Select(x => x.Split(" ").Select(int.Parse));
        return intLines;
    }

    public Grid LoadGrid()
    {
        var file = LoadFile();
        var grid = new Grid();
        var rows = file.Split(Environment.NewLine);
        grid.MaxY = rows.Length;
        for (var y = 0; y < grid.MaxY; y++)
        {
            var letters = rows[y].ToArray();
            if (y == 0)
            {
                grid.MaxX = letters.Length;
                grid.Items = new GridCoord[grid.MaxX, grid.MaxY];
            }

            for (var x = 0; x < letters.Length; x++)
            {
                grid.Items[x, y] = new GridCoord { Coord = (x, y), Value = letters[x] };
                if (letters[x] == '^') grid.Start = grid.Items[x, y];
            }
        }

        return grid;
    }
}