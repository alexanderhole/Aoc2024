namespace Aoc2024.Days._2;

public class Day4() : BaseDay(4), IDay
{
    public dynamic RunP1()
    {
        var counter = 0;
        var grid = FileService.LoadGrid();
        foreach (var item in grid.Items)
            if (item.Value == 'X')
            {
                if (CheckNeighbour(item => grid.GetDown(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetUp(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetLeft(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetRight(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetUpLeft(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetUpRight(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetDownRight(item), item, "MAS")) counter += 1;
                if (CheckNeighbour(item => grid.GetDownLeft(item), item, "MAS")) counter += 1;
            }

        return counter;
    }

    public dynamic RunP2()
    {
        var counter = 0;
        var grid = FileService.LoadGrid();
        foreach (var item in grid.Items)
            if (item.Value == 'A')
                if (((CheckNeighbour(item => grid.GetUpLeft(item), item, "M") &&
                      CheckNeighbour(item => grid.GetDownRight(item), item, "S"))
                     ||
                     (CheckNeighbour(item => grid.GetDownRight(item), item, "M") &&
                      CheckNeighbour(item => grid.GetUpLeft(item), item, "S")))
                    &&
                    ((CheckNeighbour(item => grid.GetUpRight(item), item, "M") &&
                      CheckNeighbour(item => grid.GetDownLeft(item), item, "S"))
                     ||
                     (CheckNeighbour(item => grid.GetDownLeft(item), item, "M") &&
                      CheckNeighbour(item => grid.GetUpRight(item), item, "S")))
                   )
                    counter += 1;
        return counter;
    }

    private bool CheckNeighbour(Func<GridCoord?, GridCoord> neighbourCheck, GridCoord gridCoord, string wanted)
    {
        foreach (var letterToFind in wanted.ToCharArray())
        {
            var result = neighbourCheck(gridCoord);
            if (result == null || result.Value != letterToFind)
                return false;
            gridCoord = result;
        }

        return true;
    }
}