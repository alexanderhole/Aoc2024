namespace Aoc2024;

public static class Helpers
{
    public static string LoadFile(string filename)
    {
        return File.ReadAllText(filename);
    }
    public static string[] LoadLines(string filename)
    {
        var file = LoadFile(filename);
        return file.Split(Environment.NewLine).Where(x => !String.IsNullOrEmpty(x)).ToArray();
    }
}