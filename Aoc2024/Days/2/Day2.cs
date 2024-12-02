using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day2() : BaseDay(2), IDay
{
    public int RunP1()
    {
        var rows = FileService.LoadLines();
        var safeReports = 0;
        foreach (var row in rows)
        {
            var levels = row.Split(" ").Select(x => int.Parse(x)).ToArray();
            var (safe, badReport) = checkSafe(levels);
            if (!safe)
            {
                var firstList = levels.ToList();
                firstList.RemoveAt(badReport);
                var firstCheck = checkSafe(firstList.ToArray());
                if (!firstCheck.safe)
                {
                    var secondList = levels.ToList();
                    secondList.RemoveAt(badReport + 1);
                    var secondCheck = checkSafe(secondList.ToArray());
                    if(!secondCheck.safe) continue;
                }
            }

            safeReports += 1;
        }
        return safeReports;
    }

    private (bool safe,int badReport) checkSafe(int[] levels)
    {
        if (levels[0] == levels[1])
            return (false,0);
        if (levels[0] > levels[1])
        {
            for (int i = 0; i < levels.Length -1; i++)
            {
                if (levels[i] < levels[i + 1] || (levels[i] - levels[i + 1]) > 3 || levels[i] == levels[i + 1])
                    return (false,i);
            }
        }
        if (levels[0] < levels[1])
        {
            for (int i = 0; i < levels.Length -1; i++)
            {
                if (levels[i] > levels[i + 1] || (levels[i + 1] - levels[i]) > 3 || levels[i] == levels[i + 1])
                    return (false,i);
            }
        }

        return (true,0);
    }
    
    

    public int RunP2()
    {
        return 0;
    }
}