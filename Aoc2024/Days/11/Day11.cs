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
                tempOrder.Add(applyRules.Item1.Value);
                if(applyRules.Item2 != null)
                    tempOrder.Add(applyRules.Item2.Value);
            }

            stones = tempOrder;
        }
        return stones.Count();
    }

    private (long?,long?) ApplyRules(long stone)
    {
        if (stone == 0)
            return (1,null);
        else
        {
            var s = stone.ToString();
            if (s.Length % 2 == 0)
            {
                var mid = s.Length / 2;
                return (long.Parse(s.Substring(0,mid)),long.Parse(s.Substring(mid)));
            }
            else 
                return (stone * 2024,null);
        }
    }

    public dynamic RunP2()
  {
      var stones = FileService.LoadFile().Split(" ").Select(long.Parse);
      int blinks = 75;
      Stopwatch sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < blinks; i++)
      {
          sw.Restart();
          var tempOrder = new List<long>();
          foreach (var stone in stones)
          {
              var applyRules = ApplyRules(stone);
              tempOrder.Add(applyRules.Item1.Value);
              if(applyRules.Item2 != null)
                  tempOrder.Add(applyRules.Item2.Value);
          }

          stones = tempOrder;
          Console.WriteLine(i + " blink time " + sw.ElapsedMilliseconds + "ms" + " with " + stones.Count() +" items");
      }
      return stones.Count();
  }
}