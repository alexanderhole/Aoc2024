using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day12() : BaseDay(12), IDay
{

    public dynamic RunP1()
    {
        var grid = FileService.LoadGrid();
        var items = grid.items;
        var processed = new HashSet<GridCoord>();
        var fenceCount = 0;
        for (int x = 0; x < grid.maxX; x++)
        {
            for (int y = 0; y < grid.maxY; y++)
            {
                if(processed.Contains(items[x, y])) continue;
                var returnSet = new HashSet<GridCoord>();
                var block = GetBlock(items[x, y], grid, returnSet);
                var count = block.Sum(x => x.fences).Value * block.Count();
                fenceCount += count;
                processed.UnionWith(block);
            }
        }
        return fenceCount;
    }

    private HashSet<GridCoord> GetBlock(GridCoord item, Grid grid, HashSet<GridCoord> returnSet)
    {
        //while (true)
        {
            var originalCount = returnSet.Count();
            returnSet.Add(item);
            var neighbours = CheckNeighbours(item, grid);
            neighbours.ExceptWith(returnSet);
            returnSet.UnionWith(neighbours);
            foreach (var neighbour in neighbours)
            {
                returnSet.UnionWith(GetBlock(neighbour, grid, returnSet));
            }
            //if (returnSet.Count() == originalCount) break;
        }

        return returnSet;
    }

    private HashSet<GridCoord> CheckNeighbours(GridCoord item, Grid grid)
    {
        var ret = new HashSet<GridCoord>();
        var fenceCount = 4;
        var coord = grid.GetDown(item);
        if (coord?.value == item.value)
        {
            Add(ret, coord);
            fenceCount -= 1;
        }
        coord = grid.GetRight(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            fenceCount -= 1;
        }        coord = grid.GetUp(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            fenceCount -= 1;
        }        coord = grid.GetLeft(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            fenceCount -= 1;
        }

        if (!item.fences.HasValue)
        {
           /* if (item.Coord.x == 0 || item.Coord.x == grid.maxX)
                fenceCount -= 1;
            if (item.Coord.y == 0 || item.Coord.y == grid.maxY)
                fenceCount -= 1;*/
            item.fences = fenceCount;
        }
        return ret;
    }

    private static bool Add(HashSet<GridCoord> ret, GridCoord coord)
    {
        //coord.fences -= 1;
        return ret.Add(coord);
    }

    public dynamic RunP2()
    {
        return 0;
    }

}