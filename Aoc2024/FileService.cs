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
}