using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using Microsoft.VisualBasic.CompilerServices;

namespace Aoc2024.Days._2;

public class Day7() : BaseDay(7), IDay
{

    public dynamic RunP1()
    {
        var lines = FileService.LoadLines();
        Int64 counter = 0;
        foreach (var line in lines)
        {
            var strings = line.Split(":");
            var result = Int64.Parse(strings[0]);
            var numbers = strings[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => Int64.Parse(x)).ToArray();
            var numberOfOps = numbers.Count() - 1;
            var binaryHighest = string.Join("", Enumerable.Repeat(1, numberOfOps));
            var highest = Convert.ToInt32(binaryHighest, 2);
            for (int i = highest; i >= 0; i--)
            {
                var a = Convert.ToString(i, 2).PadLeft(binaryHighest.Length, '0').Select(x => int.Parse(x.ToString())).ToArray();
                var isValidResult = IsValidResult(result, numbers, a);
                if(isValidResult == 0) continue;
                counter += isValidResult;
                break;
            }
        }
        
        return counter;
    }

    private long IsValidResult(long result, IEnumerable<long> numbers, int[] adds)
    {
        var current = numbers.ElementAt(0);
        for (int i = 1; i <= numbers.Count()-1; i++)
        {
            current = DoMath(current, numbers.ElementAt(i), adds[i-1]);
        }

        return result == current ? current : 0;
    }

    private Int64 DoMath(Int64 a, Int64 b, int add)
    {
        if (add == 1)
            return a + b;
        return a* b;
    }
    
    public int RunP2()
    {
        return 0;
    }
}