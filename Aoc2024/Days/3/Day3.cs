using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day3() : BaseDay(3), IDay
{
    public int RunP1()
    {
        var fileString = FileService.LoadFile();
        var regex = new Regex("mul\\(\\d*,\\d*\\)");
        var matches = regex.Matches(fileString);
        var result = 0;
        foreach (var match in matches.Select(x => x.Value))
        {
            regex = new Regex("\\d*");
            var enumerable = regex.Matches(match)
                .Where(x => !string.IsNullOrEmpty(x.Value)).ToList();
            result += enumerable
                .Select(x => int.Parse(x.Value)).Aggregate((x,y) => x*y);
        }

        return result;
    }

    public int RunP2()
    {
        return 0;
    }
}