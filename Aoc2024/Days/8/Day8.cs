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

public class Day8() : BaseDay(8), IDay
{

    public dynamic RunP1()
    {
        var grid = FileService.LoadGrid();
        List<GridCoord> antennas = new List<GridCoord>();
        List<(int, int)> antiNodes = new List<(int, int)>();
        foreach (var item in grid.items)    
        {
            if(item.value != '.')
                antennas.Add(item);
        }

        foreach (var antennaTypes in antennas.GroupBy(x => x.value))
        {
            foreach (var antennaType in antennaTypes)
            {
                antiNodes.AddRange(antennaTypes.Where(x => x != antennaType)
                    .Select(x =>
                    {
                        var findDistancex = FindDistance(antennaType.Coord.x, x.Coord.x);
                        var findDistancey = FindDistance(antennaType.Coord.y, x.Coord.y);
                        if (antennaType.Coord.x >= x.Coord.x)
                            findDistancex = antennaType.Coord.x + findDistancex;
                        else
                            findDistancex = antennaType.Coord.x - findDistancex;
                        if (antennaType.Coord.y >= x.Coord.y)
                            findDistancey = antennaType.Coord.y + findDistancey;
                        else
                            findDistancey = antennaType.Coord.y - findDistancey;
                        return (findDistancex,
                            findDistancey);
                    }));
            }
        }
        return antiNodes.ToHashSet().Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < grid.maxX && x.Item2 < grid.maxY).Count();
    }
    public int FindDistance(int nr1, int nr2)
    {
        return Math.Abs(nr1 - nr2);
    }
    public dynamic RunP2()
    {
        
        return 0;
    }

    
}