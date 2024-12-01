namespace Aoc2024.Days._1;

public class BaseDay(int day)
{
    internal readonly FileService FileService = new(day);
}