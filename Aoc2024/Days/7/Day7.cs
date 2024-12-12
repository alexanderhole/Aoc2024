using SG.Utilities;

namespace Aoc2024.Days._2;

public class Day7() : BaseDay(7), IDay
{
    public dynamic RunP1()
    {
        var lines = FileService.LoadLines();
        long counter = 0;

        var bc = new BaseConverter("01");

        counter = Counter(lines, bc, counter);

        return counter;
    }

    public dynamic RunP2()
    {
        var lines = FileService.LoadLines();
        long counter = 0;
        var bc = new BaseConverter("012");

        counter = Counter(lines, bc, counter);

        return counter;
    }

    private long Counter(string[] lines, BaseConverter bc, long counter)
    {
        foreach (var line in lines)
        {
            var strings = line.Split(":");
            var result = long.Parse(strings[0]);
            var numbers = strings[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => long.Parse(x))
                .ToArray();
            var numberOfOps = numbers.Length - 1;
            var binaryHighest = string.Join("", Enumerable.Repeat(bc.Alphabet.Length - 1, numberOfOps));
            for (var i = (int)bc.ConvertToDec(binaryHighest); i >= 0; i--)
            {
                var a = bc.ConvertFromDec(i).PadLeft(binaryHighest.Length, '0').ToArray();
                var isValidResult = IsValidResultP2(result, numbers, a);
                if (isValidResult == 0) continue;
                counter += isValidResult;
                break;
            }
        }

        return counter;
    }

    private long IsValidResultP2(long result, long[] numbers, char[] adds)
    {
        var current = numbers[0];
        for (var i = 1; i <= numbers.Length - 1; i++)
        {
            current = DoMathP2(current, numbers[i], adds[i - 1]);
            if (current > result) return 0;
        }

        return result == current ? current : 0;
    }

    private long DoMathP2(long a, long b, char add)
    {
        if (add == '0')
            return a + b;
        if (add == '1')
            return a * b;
        return long.Parse($"{a}{b}");
    }
}