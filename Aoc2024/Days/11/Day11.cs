using System.Reflection.Metadata.Ecma335;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using LRU;

namespace Aoc2024.Days._2;

public class Day11() : BaseDay(11), IDay
{
    private CacheProvider<long> countcached = new();
    private CacheProvider<(long, long?)> applycached = new();

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
        return stones.Sum(x => GetChildrenCount(blinks,x));
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
        //var rules = applycached.Cache(() => ApplyRules(stone), stone);

        var rules = ApplyRules(stone);
        counter += countcached.Cache(() => GetChildrenCount(blinks,rules.Item1), (blinks,rules.Item1));
        counter += rules.Item2.HasValue ? countcached.Cache(() => GetChildrenCount(blinks,rules.Item2.Value), (blinks,rules.Item2.Value)) : 0;
        return counter;
    }

    public static int GetNumberOfDigitsInNumber(long n) =>
        1 + (int)Math.Log10(n);
}

public class CacheProvider<T>
{
    private Dictionary<object, T> cache = new Dictionary<object, T>();
    public T Cache(Func<T> function, object key)
    {
        if (cache.TryGetValue(key, out var output))
            return output;
        var newOutput = function();
        cache[key] = newOutput;
        return newOutput;
    }
}