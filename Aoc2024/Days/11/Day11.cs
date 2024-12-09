using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using LRU;

namespace Aoc2024.Days._2;

public class Day11() : BaseDay(11), IDay
{
    private Dictionary<(long stone, int depth), long> blinkCache;

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
        blinkCache = new Dictionary<(long stone, int depth), long>();
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

        if (blinkCache.TryGetValue((stone, blinks), out var cachedCount))
            counter += cachedCount;
        else
        {
            var rules = ApplyRules(stone);
            counter += GetChildrenCount(blinks, rules.Item1);
            counter += rules.Item2 != null ? GetChildrenCount(blinks, rules.Item2.Value) : 0;
            blinkCache[(stone, blinks)] = counter;
        }

        return counter;
    }

    public static int GetNumberOfDigitsInNumber(long n) =>
        1 + (int)Math.Log10(n);
}