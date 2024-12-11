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

public class Day11() : BaseDay(11), IDay
{

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
                if(applyRules.Any(x => x<0)) Debugger.Break();
                tempOrder.AddRange(applyRules);
            }

            stones = tempOrder;
        }
        return stones.Count();
    }

    private IEnumerable<long> ApplyRules(long stone)
    {
        var returnValue = new List<long>();
        if (stone == 0)
            return new List<long>() { 1 };
        else if (stone.ToString().Length % 2 == 0)
        {
            var charsEnumerable = stone.ToString().Chunk(stone.ToString().Length / 2);
            var enumerable = charsEnumerable.Select(x => long.Parse(new string(x)));
            return enumerable.ToList();
        }
        else 
            return new List<long>() { stone * 2024 };
    }

    public dynamic RunP2()
  {
      return 0;
  }
}