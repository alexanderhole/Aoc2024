using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using LRU;
using Microsoft.VisualBasic.CompilerServices;
using SG.Utilities;

namespace Aoc2024.Days._2;

public class Day11() : BaseDay(11), IDay
{
    private LeastRecentlyUsedCache<(long stone, int depth), long> blinkCache;

    public dynamic RunP1()
    {
        var stones = FileService.LoadFile().Split(" ").Select(long.Parse);
        int blinks = 25;
        for (int i = 0; i < blinks; i++)
        {
            var tempOrder = new List<long>();
            foreach (var stone in stones)
            {
                var applyRules = ApplyRules(stone);
                tempOrder.Add(applyRules.Item1);
                if (applyRules.Item2 != null)
                    tempOrder.Add(applyRules.Item2.Value);
            }

            stones = tempOrder;
        }

        return stones.Count();
    }

    private (long, long?) ApplyRules(long stone)
    {
        if (stone == 0)
            return (1, null);
        else
        {
            var digitsLog10 = Digits_Log10(stone);
            if (digitsLog10 % 2 == 0)
            {
                var pow = Math.Pow(10, digitsLog10 / 2);
                var d = stone / pow;
                var one = Math.Floor(d);
                var two = Math.Round(d % 1 * pow);
                return ((long)one, (long)two);
            }
            else
                return (stone * 2024, null);
        }
    }

    public dynamic RunP2()
    {
        var stones = FileService.LoadFile().Split(" ").Select(long.Parse);
        int blinks = 75;
        long final = 0;
        blinkCache = new LeastRecentlyUsedCache<(long stone, int depth), long>(500000000);
        foreach (var stone in stones)
        {
            final += GetChildrenCount(blinks, stone);
        }

        return final;
    }

    private long GetChildrenCount(int blinks, long stone)
    {
        if (blinks == 0)
            return 1;
        blinks -= 1;
        long counter = 0;
        var rules = ApplyRules(stone);
        long childrenCount = 0;
        if (blinkCache.ContainsKey((stone, blinks)))
            childrenCount = blinkCache[(stone, blinks)];
        else
        {
            childrenCount = GetChildrenCount(blinks, rules.Item1);
            blinkCache[(stone, blinks)] = childrenCount;
        }

        counter += childrenCount;
        if (rules.Item2 != null)
        {
            counter += rules.Item2 != null ? GetChildrenCount(blinks, rules.Item2.Value) : 0;
        }

        return counter;
    }

    public static int Digits_Log10(long n) =>
        n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));
}