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
        return RunBlinks(25);
    }
    public dynamic RunP2()
    {
        return RunBlinks(75);
    }

    private dynamic RunBlinks(int blinks)
    {
        var stones = FileService.LoadFile().Split(" ").Select(long.Parse);
        blinkCache = new LeastRecentlyUsedCache<(long stone, int depth), long>(350000);
        return stones.Sum(x => GetChildrenCount(blinks, x));
    }
    
    private (long, long?) ApplyRules(long stone)
    {
        if (stone == 0)
            return (1, null);
        var lengthOfDigits = GetNumberOfDigitsInNumber(stone);
        if (lengthOfDigits % 2 != 0) return (stone * 2024, null);
        var (firstHalf, secondHalf) = One(stone, lengthOfDigits);
        return ((long)firstHalf, (long)secondHalf);
    }

    private static (double firstHalf, double secondHalf) One(long stone, int lengthOfDigits)
    {
        var pow = Math.Pow(10, lengthOfDigits / 2);
        var d = stone / pow;
        var one = Math.Floor(d);
        var two = Math.Round(d % 1 * pow);
        return (one, two);
    }

    private long GetChildrenCount(int blinks, long stone)
    {
        if (blinks == 0)
            return 1;
        blinks -= 1;
        long counter = 0;
        var rules = ApplyRules(stone);
        if (blinkCache.TryGetValue((stone, blinks), out var cachedCount))
            counter += cachedCount;
        else
        {
            counter += GetChildrenCount(blinks, rules.Item1);
            blinkCache[(stone, blinks)] = counter;
        }
        counter += rules.Item2 != null ? GetChildrenCount(blinks, rules.Item2.Value) : 0;
        return counter;
    }

    public static int GetNumberOfDigitsInNumber(long n) =>
        n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));
}