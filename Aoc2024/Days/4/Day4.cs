using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day4() : BaseDay(4), IDay
{
    public int RunP1()
    {
        var counter = 0;
        var grid = FileService.LoadGrid();
        foreach (var item in grid.items)
        {
            if (item.value == 'X')
            {
                if (checkNeighbour((item) => grid.GetDown(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetUp(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetLeft(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetRight(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetUpLeft(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetUpRight(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetDownRight(item), item, "MAS")) counter += 1;
                if (checkNeighbour((item) => grid.GetDownLeft(item), item, "MAS")) counter += 1;
            }
        }
        return counter;
    }

    private bool checkNeighbour(Func<GridCoord?, GridCoord> neighbourCheck, GridCoord gridCoord, string wanted)
    {
        foreach (var letterToFind in wanted.ToCharArray())
        {
            var result = neighbourCheck(gridCoord);
            if (result == null || result.value != letterToFind)
                return false;
            gridCoord = result;
        }

        return true;
    }

    public int RunP2()
    {
        return 0;
    }
}