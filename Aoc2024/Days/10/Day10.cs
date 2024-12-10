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

public class Day10() : BaseDay(10), IDay
{

    public dynamic RunP1()
    {
        var loadGrid = FileService.LoadGrid();
        foreach (var item in loadGrid.items)
        {
            if (item.value == '0')
            {
                NewMethod(item, loadGrid);
                Console.WriteLine($"TrailHead {item.Coord.x},{item.Coord.y} Score : {dayCount}");
                counter += dayCount.Count;
                dayCount = new HashSet<GridCoord>();
            }
        }
        return counter;
    }

    private int counter = 0;
    private HashSet<GridCoord> dayCount = new HashSet<GridCoord>();
    private void NewMethod(GridCoord item, Grid loadGrid)
    {
        //while (true)
        {
            var newSet = FindNextItems(item, loadGrid);
            if (item.value == '9')
            {
                trailcounter += 1;
                dayCount.Add(item);
                return;
            }

            foreach (var coord in newSet)
            {
                NewMethod(coord, loadGrid);
            }

            // if (newSet.Count() == 0)
            //     break;
        }
        
    }

    private List<GridCoord> FindNextItems(GridCoord item, Grid grid)
    {
        var next = int.Parse(item.value.ToString())+1;
        var neighbours = grid.FindNeighours(item, (next).ToString()[0]);
        Console.Write(String.Join("",neighbours.Select(x => $"{x.Coord.x},{x.Coord.y} : {x.value}. ")));
        return neighbours;
    }
    int trailcounter = 0;
    public dynamic RunP2()
    {
        var loadGrid = FileService.LoadGrid();
        foreach (var item in loadGrid.items)
        {
            if (item.value == '0')
            {
                NewMethod(item, loadGrid);
                Console.WriteLine($"TrailHead {item.Coord.x},{item.Coord.y} Score : {dayCount}");
            }
        }
        return trailcounter/2;
    }
}