namespace Aoc2024.Days._2;

public class Day5() : BaseDay(5), IDay
{
    public dynamic RunP1()
    {
        var lines = FileService.LoadLines();
        var rules = lines.Where(x => x.Contains("|")).ToArray();
        var pagesList = lines.Where(x => !x.Contains("|"))
            .Select(x => x.Split(",").Select(y => int.Parse(y)).ToArray());
        var correctSortedPages = pagesList.Where(x => AlreadyCorrect(x, rules));
        return correctSortedPages.Select(x => x[(int)(Math.Ceiling((double)x.Length) / 2)]).Sum();
    }

    public dynamic RunP2()
    {
        Stopwatch sw = new();
        sw.Start();
        var lines = FileService.LoadLines();
        var rules = lines.Where(x => x.Contains("|")).ToArray();
        var pagesList = lines.Where(x => !x.Contains("|"))
            .Select(x => x.Split(",").Select(y => int.Parse(y)).ToArray());
        var count = 0;
        var incorrectSortedPages = pagesList; //.Where(x => !AlreadyCorrect(x, rules));
        foreach (var page in incorrectSortedPages)
        {
            var sorted = false;

            while (!sorted)
            {
                sorted = true;
                sorted = Sorted(page, rules, sorted);
            }

            var ceiling = Math.Ceiling((double)page.Length / 2);
            count += page[(int)ceiling - 1];
        }

        return count;
    }

    private static bool Sorted(int[] page, string[] rules, bool sorted)
    {
        for (var i = 0; page.Length > i; i++)
        for (var j = 0; page.Length > j; j++)
        {
            if (rules.Contains($"{page[i]}|{page[j]}") && j < i)
            {
                (page[j], page[i]) = (page[i], page[j]);
                sorted = false;
            }

            if (rules.Contains($"{page[j]}|{page[i]}") && j > i)
            {
                (page[j], page[i]) = (page[i], page[j]);
                sorted = false;
            }
        }

        return sorted;
    }

    private static bool AlreadyCorrect(int[] page, string[] rules)
    {
        for (var i = 0; page.Length > i; i++)
        for (var j = 0; page.Length > j; j++)
        {
            if (rules.Contains($"{page[i]}|{page[j]}") && j < i) return false;

            if (rules.Contains($"{page[j]}|{page[i]}") && j > i) return false;
        }

        return true;
    }
}