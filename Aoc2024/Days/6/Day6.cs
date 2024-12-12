namespace Aoc2024.Days._2;

public class Day6() : BaseDay(6), IDay
{
    private const char Up = '^';
    private const char Down = 'v';
    private const char Right = '>';
    private const char Left = '<';
    private const char Wall = '#';

    public dynamic RunP1()
    {
        var visited = GridCoords();
        return visited.DistinctBy(x => (x.Key.x, x.Key.y)).Count();
    }

    public dynamic RunP2()
    {
        var original = FileService.LoadGrid();
        var path = GridCoords();
        var counter = 0;
        var start = original.Start;
        foreach (var places in path.DistinctBy(x => (x.Key.x, x.Key.y)))
        {
            var originalValue = original.Items[places.Value.Coord.x, places.Value.Coord.y].Value;
            original.Items[places.Value.Coord.x, places.Value.Coord.y].Value = Wall;

            var foundLoop = Visited(start, original, start.Value) == null;
            if (foundLoop) counter += 1;
            original.Items[places.Value.Coord.x, places.Value.Coord.y].Value = originalValue;
        }

        return counter;
    }

    private Dictionary<(int x, int y, char direction), GridCoord> GridCoords()
    {
        var grid = FileService.LoadGrid();
        var start = grid.Start;
        return HashSet(grid, start);
    }

    private Dictionary<(int x, int y, char direction), GridCoord> HashSet(Grid grid, GridCoord start)
    {
        var visited = Traverse(start, grid, Up);
        return visited;
    }

    private GridCoord? MoveInDirection(Grid grid, char direction, GridCoord current)
    {
        switch (direction)
        {
            case Up:
                return grid.GetUp(current);
            case Down:
                return grid.GetDown(current);
            case Right:
                return grid.GetRight(current);
            case Left:
                return grid.GetLeft(current);
        }

        throw new Exception();
    }

    private Dictionary<(int x, int y, char direction), GridCoord> Traverse(GridCoord? current, Grid grid,
        char direction)
    {
        var visited = new Dictionary<(int x, int y, char direction), GridCoord>();
        var next = current;
        {
            while (current?.Value != null)
            {
                next = MoveInDirection(grid, direction, current);
                if (next is null) break;
                if (next?.Value != Wall)
                {
                    current = next;
                    if (visited.ContainsKey((current.Coord.x, current.Coord.y, direction)))
                        return null;
                    visited[(current.Coord.x, current.Coord.y, direction)] = current;
                }
                else
                {
                    direction = Turn(direction);
                }
            }
        }
        return visited;
    }


    private char[,] Visited(GridCoord? current, Grid grid, char direction)
    {
        var visited = new char[grid.MaxX, grid.MaxY];
        var next = current;
        while (current?.Value != null)
        {
            next = MoveInDirection(grid, direction, current);
            if (next is null) break;
            if (next?.Value == Wall)
            {
                direction = Turn(direction);
            }
            else
            {
                if (visited[next.Coord.x, next.Coord.y] == direction)
                    return null;
                current = next;
                visited[current.Coord.x, current.Coord.y] = direction;
            }
        }

        return visited;
    }

    private char Turn(char currentDirection)
    {
        return currentDirection switch
        {
            Up => Right,
            Right => Down,
            Down => Left,
            Left => Up,
            _ => Up
        };
    }
}