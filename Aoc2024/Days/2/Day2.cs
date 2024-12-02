using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day2() : BaseDay(2), IDay
{
    public int RunP1()
    {
        return FileService.LoadIntLines().Count(row => checkSafe(row.ToArray()));
    }

    private bool checkSafe(int[] levels)
    {
        return levels[0] != levels[1] && CheckSafe(levels, levels[0] < levels[1]);
    }

    private static bool CheckSafe(int[] levels, bool desc)
    {
        for (var i = 0; i < levels.Length - 1; i++)
        {
            if (desc)
            {
                if (levels[i] > levels[i + 1] || Math.Abs(levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    return false;
            }
            else
                if (levels[i] < levels[i + 1] || Math.Abs(levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    return false;   
        }
        return true;
    }


    public int RunP2()
    {
        var rows = FileService.LoadIntLines();
        var safeReports = 0;
        foreach (var row in rows)
        {
            var levels = row.ToArray();
            var safe = checkSafe(levels);
            if(safe)
                safeReports += 1;
            else
                for (var i = 0; i < levels.Length; i++)
                {
                    var firstList = levels.ToList();
                    firstList.RemoveAt(i);
                    var firstCheck = checkSafe(firstList.ToArray());
                    if (!firstCheck) continue;
                    safeReports += 1;
                    break;
                }
        }
        return safeReports;
    }
}