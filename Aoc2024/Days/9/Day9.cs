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

public class Day9() : BaseDay(9), IDay
{

    public dynamic RunP1()
    {
        var result = GetFileBlocks();
        var stack = new Stack<string>(result);
        for (int i = result.Count -1; i >= 0; i--)
        {
            if(!result.Take(i).Contains(".")) continue;
            result[result.IndexOf(".")] = result[i];
            result[i] = ".";
        }
        var numbers = result.Where(x => x != ".").Select(x => long.Parse(x)).ToArray();
        long checksum = 0;
        for (int i = 0; i < numbers.Count(); i++)
        {
            checksum += i * numbers[i];
        }
        return checksum;
    }

    private List<string> GetFileBlocks()
    {
        var result = new List<string>();
        var line = FileService.LoadFile();
        var files = line.ToCharArray().Chunk(2);
        var id = 0;
        foreach (var file in files)
        {
            result.AddRange(Enumerable.Repeat(id.ToString(), int.Parse(file[0].ToString())));
            if (file.Length == 2)
                result.AddRange(Enumerable.Repeat(".", int.Parse(file[1].ToString())));
            id += 1;
        }

        return result;
    }

    public dynamic RunP2()
    {
        return 0;
    }
    
}