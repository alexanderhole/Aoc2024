using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using Microsoft.VisualBasic.CompilerServices;
using SG.Utilities;

namespace Aoc2024.Days._2;

public class Day7() : BaseDay(7), IDay
{

    public dynamic RunP1()
    {
        var lines = FileService.LoadLines();
        Int64 counter = 0;
        
        var bc = new BaseConverter("01");

        counter = Counter(lines, bc, counter);
        
        return counter;
    }

    public dynamic RunP2()
    {
        var lines = FileService.LoadLines();
        Int64 counter = 0;
        var bc = new BaseConverter("012");

        counter = Counter(lines, bc, counter);
        
        return counter;
    }

    private long Counter(string[] lines, BaseConverter bc, long counter)
    {
        foreach (var line in lines)
        {
            var strings = line.Split(":");
            var result = Int64.Parse(strings[0]);
            var numbers = strings[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => Int64.Parse(x)).ToArray();
            var numberOfOps = numbers.Count() - 1;
            var binaryHighest = string.Join("", Enumerable.Repeat(bc.Alphabet.Length-1, numberOfOps));
            for (int i = (int)bc.ConvertToDec(binaryHighest); i >= 0; i--)
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
        for (int i = 1; i <= numbers.Length-1; i++)
        {
            current = DoMathP2(current, numbers[i], adds[i-1]);
            if (current > result) return 0;
        }

        return result == current ? current : 0;
    }

    private long DoMathP2(Int64 a, Int64 b, char add)
    {
        if (add == '0')
            return a + b;
        if (add == '1')
            return a* b;
        return Int64.Parse($"{a}{b}");
    }
}