using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day2() : BaseDay(2), IDay
{
    public int RunP1()
    {
        var rows = FileService.LoadLines();
        return rows.Count(checkSafe);
        foreach (var row in rows)
        {
            var safe = checkSafe(row);
        }
    }

    private bool checkSafe(string row)
    {
        var levels = row.Split(" ").Select(x => int.Parse(x)).ToArray();
        if (levels[0] == levels[1])
            return false;
        if (levels[0] > levels[1])
        {
            for (int i = 0; i < levels.Length -1; i++)
            {
                if (levels[i] < levels[i + 1] || (levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    return false;
            }
        }
        if (levels[0] < levels[1])
        {
            for (int i = 0; i < levels.Length -1; i++)
            {
                if (levels[i] > levels[i + 1] || (levels[i + 1] - levels[i]) > 3 || levels[i] == levels[i + 1])
                    return false;
            }
        }

        return true;
    }

    public int RunP2()
    {
        return 0;
    }
}