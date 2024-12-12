namespace Aoc2024.Days._2;

public class Day2() : BaseDay(2), IDay
{
    public dynamic RunP1()
    {
        return FileService.LoadIntLines().Count(row => CheckSafe(row.ToArray()));
    }


    public dynamic RunP2()
    {
        var safeReports = 0;
        foreach (var row in FileService.LoadIntLines())
        {
            var safe = CheckSafe(row.ToArray());
            if (safe)
                safeReports += 1;
            else
                for (var i = 0; i < row.Count(); i++)
                {
                    var firstList = row.ToList();
                    firstList.RemoveAt(i);
                    if (!CheckSafe(firstList.ToArray())) continue;
                    safeReports += 1;
                    break;
                }
        }

        return safeReports;
    }

    private bool CheckSafe(int[] levels)
    {
        return levels[0] != levels[1] && CheckSafe(levels, levels[0] < levels[1]);
    }

    private static bool CheckSafe(IReadOnlyList<int> levels, bool desc)
    {
        for (var i = 0; i < levels.Count - 1; i++)
        {
            var secondLevel = levels[i + 1];
            var firstLevel = levels[i];
            if (Math.Abs(firstLevel - secondLevel) > 3 || firstLevel == secondLevel)
                return false;
            if (desc)
            {
                if (firstLevel > secondLevel)
                    return false;
            }
            else if (firstLevel < secondLevel)
            {
                return false;
            }
        }

        return true;
    }
}