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
            var digitsLog10 = Digits_Log10(stone);
            if (digitsLog10 % 2 == 0)
            {
                var pow = Math.Pow(10, digitsLog10/2);
                var d = stone / pow;
                var one = Math.Floor(d);
                var two = Math.Round(d % 1 * pow);
                return ((long)one, (long)two);
            }
            else 
                return (stone * 2024,null);
        }
    }

    public dynamic RunP2()
  {
      var stones = FileService.LoadFile().Split(" ").Select(long.Parse);
      int blinks = 40;
      Stopwatch sw = new Stopwatch();
      sw.Start();
      sw.Restart();
      var stonecounter = 0;
      var final = new List<long>();
      foreach (var stone in stones)
      {
          stonecounter += 1;
          var tempOrder = new List<long>(){stone};

          for (int i = 0; i < blinks; i++)
          {
              var ruleOrder = new List<long>();
              foreach (var temp in tempOrder)
              {
                  var applyRules = ApplyRules(temp);
                  ruleOrder.Add(applyRules.Item1.Value);
                  if (applyRules.Item2 != null)
                      ruleOrder.Add(applyRules.Item2.Value);
              }
              Console.WriteLine(stonecounter + " stone time " + i +" blink "   + sw.ElapsedMilliseconds + "ms" + " with " + tempOrder.Count() +" items");

              tempOrder = ruleOrder;
          }

          //stones = tempOrder;
          Console.WriteLine(stonecounter + " stone time " + sw.ElapsedMilliseconds + "ms" + " with " + stones.Count() +" items");
          final.AddRange(tempOrder);
      }
      return final.Count();
  }
    public static int Digits_Log10(long n) =>
        n == 0L ? 1 : (n > 0L ? 1 : 2) + (int)Math.Log10(Math.Abs((double)n));

}