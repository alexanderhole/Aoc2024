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
                var bigFence = FindBigFencePieces(block, grid);
                var count = bigFence * block.Count();
                fenceCount += count;
                processed.UnionWith(block);
            }
        }
        return fenceCount;
    }

    private int FindBigFencePieces(HashSet<GridCoord> block, Grid grid)
    {
        var fenceCount = 0;
        fenceCount += UpperFence(block);
        fenceCount += LowerFence(block);
        fenceCount += RightFence(block);
        fenceCount += LeftFence(block);

        return fenceCount;
        var distinctXs = block.DistinctBy(x => x.Coord.x).Select(x => x.Coord.x).ToList();
        for (int x = 0; x < distinctXs.Count(); x++)
        {
            var row = block.Where(coord => coord.Coord.x == distinctXs[x]).Select(x => x.Coord.y).Order().ToList();
            for (int i = 0; i < row.Count()-1; i++)
            {
                if (row[i] + 1 != row[i + 1])
                {
                    fenceCount += 1;
                }
            }
        }

        return fenceCount;return 0;
    }
    private static int RightFence(HashSet<GridCoord> block)
    {
        var rightFences = block.Count(x => x.fences.right);
        var distinctXs = block.DistinctBy(x => x.Coord.x).Select(x => x.Coord.x).ToList();
        for (int x = 0; x < distinctXs.Count(); x++)
        {
            var itemsWithUpperFence =
                block.Where(gridCoord => gridCoord.Coord.x == distinctXs[x] && gridCoord.fences.right).OrderBy(x => x.Coord.y).ToList();
            
            for (int i = 0; i < itemsWithUpperFence.Count()-1; i++)
            {
                if (itemsWithUpperFence[i].Coord.y + 1 == itemsWithUpperFence[i + 1].Coord.y)
                {
                    rightFences -= 1;
                }
            }
        }

        return rightFences;
    }
    private static int LeftFence(HashSet<GridCoord> block)
    {
        var leftFences = block.Count(x => x.fences.left);
        var distinctXs = block.DistinctBy(x => x.Coord.x).Select(x => x.Coord.x).ToList();
        for (int x = 0; x < distinctXs.Count(); x++)
        {
            var itemsWithUpperFence =
                block.Where(gridCoord => gridCoord.Coord.x == distinctXs[x] && gridCoord.fences.left).OrderBy(x => x.Coord.y).ToList();
            
            for (int i = 0; i < itemsWithUpperFence.Count()-1; i++)
            {
                if (itemsWithUpperFence[i].Coord.y + 1 == itemsWithUpperFence[i + 1].Coord.y)
                {
                    leftFences -= 1;
                }
            }
        }

        return leftFences;
    }
    private static int UpperFence(HashSet<GridCoord> block)
    {
        var upperFences = block.Count(x => x.fences.up);
        var distinctYs = block.DistinctBy(x => x.Coord.y).Select(x => x.Coord.y).ToList();
        for (int y = 0; y < distinctYs.Count(); y++)
        {
            var itemsWithUpperFence =
                block.Where(x => x.Coord.y == distinctYs[y] && x.fences.up).OrderBy(x => x.Coord.x).ToList();
            for (int i = 0; i < itemsWithUpperFence.Count() - 1; i++)
            {
                if (itemsWithUpperFence[i].Coord.x + 1 == itemsWithUpperFence[i + 1].Coord.x)
                {
                    upperFences -= 1;
                }
            }
        }

        return upperFences;
    }
    private static int LowerFence(HashSet<GridCoord> block)
    {
        var lowerFences = block.Count(x => x.fences.bottom);
        var distinctYs = block.DistinctBy(x => x.Coord.y).Select(x => x.Coord.y).ToList();
        for (int y = 0; y < distinctYs.Count(); y++)
        {
            var itemsWithLowerFence =
                block.Where(x => x.Coord.y == distinctYs[y] && x.fences.bottom).OrderBy(x => x.Coord.x).ToList();
            for (int i = 0; i < itemsWithLowerFence.Count() - 1; i++)
            {
                if (itemsWithLowerFence[i].Coord.x + 1 == itemsWithLowerFence[i + 1].Coord.x)
                {
                    lowerFences -= 1;
                }
            }
        }

        return lowerFences;
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
            item.fences.bottom = false;
        }
        coord = grid.GetRight(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            item.fences.right = false;
        }
        coord = grid.GetUp(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            item.fences.up = false;
        }
        coord = grid.GetLeft(item);
        if(coord?.value == item.value)
        {
            Add(ret, coord);
            item.fences.left = false;
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