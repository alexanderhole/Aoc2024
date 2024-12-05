using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;


public class Day5() : BaseDay(5), IDay
{
    public int RunP1()
    {
        var lines = FileService.LoadLines();
        var rules = lines.Where(x => x.Contains("|")).ToArray();
        var pagesList = lines.Where(x => !x.Contains("|")).Select(x => x.Split(",").Select(y => int.Parse(y)).ToArray());
        var count = 0;
        var correctSortedPages = pagesList.Where(x => AlreadyCorrect(x, rules));
        // foreach (var page in pagesList)
        // {
        //     bool sorted = false;
        //
        //     while (!sorted)
        //     {
        //         sorted = true;
        //         sorted = Sorted(page, rules, correctSortedPages, sorted);
        //     }
        //
        //     var ceiling = Math.Ceiling((double)page.Length / 2);
        //     count += page[(int)ceiling-1];
        //     Console.WriteLine((String.Join(",",page)));
        //     Console.WriteLine("middle: " +page[(int)ceiling-1]);
        //
        // ;
        // }
        return correctSortedPages.Select(x => x[(int)(Math.Ceiling((double)x.Length) / 2)]).Sum();
        return count;
    }

    private static bool Sorted(int[] page, string[] rules, List<int[]> correctSortedPages, bool sorted)
    {
        for (int i = 0; page.Count() > i; i++)
        {
            for (int j = 0; page.Count() > j; j++)
            {
                if (rules.Contains($"{page[i]}|{page[j]}") && j < i)
                {
                    correctSortedPages.Remove(page);
                    (page[j], page[i]) = (page[i], page[j]);
                    sorted = false;
                }

                if (rules.Contains($"{page[j]}|{page[i]}") && j > i)
                {
                    (page[j], page[i]) = (page[i], page[j]);
                    sorted = false;
                }
            }
        }

        return sorted;
    }
    private static bool AlreadyCorrect(int[] page, string[] rules)
    {
        for (int i = 0; page.Count() > i; i++)
        {
            for (int j = 0; page.Count() > j; j++)
            {
                if (rules.Contains($"{page[i]}|{page[j]}") && j < i)
                {
                    return false;
                }

                if (rules.Contains($"{page[j]}|{page[i]}") && j > i)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int RunP2()
    {
        return 0;
    }
}