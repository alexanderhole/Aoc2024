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
            var safe = checkSafe(levels);
            if(safe)
                safeReports += 1;
            else
                for (int i = 0; i < levels.Length; i++)
                {
                    var firstList = levels.ToList();
                    firstList.RemoveAt(i);
                    var firstCheck = checkSafe(firstList.ToArray());
                    if (firstCheck)
                    {
                        safeReports += 1;
                        break;
                    }
                }
            
        }
        return safeReports;
    }

    private bool checkSafe(int[] levels)
    {
        if (levels[0] == levels[1])
            return false;
        if (levels[0] > levels[1])
        {
            for (var i = 0; i < levels.Length -1; i++)
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