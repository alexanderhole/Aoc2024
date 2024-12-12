namespace Aoc2024.Days._2;

public class Day10() : BaseDay(10), IDay
{
    private int _counter;
    private HashSet<GridCoord> _dayCount = new();
    private int _trailcounter;

    public dynamic RunP1()
    {
        var loadGrid = FileService.LoadGrid();
        foreach (var item in loadGrid.Items)
            if (item.Value == '0')
            {
                NewMethod(item, loadGrid);
                Console.WriteLine($"TrailHead {item.Coord.x},{item.Coord.y} Score : {_dayCount}");
                _counter += _dayCount.Count;
                _dayCount = new HashSet<GridCoord>();
            }

        return _counter;
    }

    public dynamic RunP2()
    {
        var loadGrid = FileService.LoadGrid();
        foreach (var item in loadGrid.Items)
            if (item.Value == '0')
            {
                NewMethod(item, loadGrid);
                Console.WriteLine($"TrailHead {item.Coord.x},{item.Coord.y} Score : {_dayCount}");
            }

        return _trailcounter / 2;
    }

    private void NewMethod(GridCoord item, Grid loadGrid)
    {
        //while (true)
        {
            var newSet = FindNextItems(item, loadGrid);
            if (item.Value == '9')
            {
                _trailcounter += 1;
                _dayCount.Add(item);
                return;
            }

            foreach (var coord in newSet) NewMethod(coord, loadGrid);

            // if (newSet.Count() == 0)
            //     break;
        }
    }

    private List<GridCoord> FindNextItems(GridCoord item, Grid grid)
    {
        var next = int.Parse(item.Value.ToString()) + 1;
        var neighbours = grid.FindNeighbours(item, next.ToString()[0]);
        Console.Write(string.Join("", neighbours.Select(x => $"{x.Coord.x},{x.Coord.y} : {x.Value}. ")));
        return neighbours;
    }
}