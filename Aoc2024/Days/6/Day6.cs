using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Aoc2024.Days._1;
using Aoc2024.Interfaces;

namespace Aoc2024.Days._2;

public class Day6() : BaseDay(6), IDay
{
    private const char Up = '^';
    private const char Down = 'v';
    private const char Right = '>';
    private const char Left = '<';
    private const char Wall = '#';

    public int RunP1()
    {
        var visited = GridCoords();
        return visited.Count();
    }

    private HashSet<(int x, int y, char direction)> GridCoords()
    {
        var grid = FileService.LoadGrid();
        var start = grid.items.Single(x => x.Value.value == Up);
        return HashSet(grid, start.Value);
    }

    private HashSet<(int x, int y, char direction)> HashSet(Grid grid, GridCoord start)
    {
        
        var visited = Traverse(start, grid, Up);
        return visited;
    }

    private GridCoord? MoveInDirection(Grid grid, char direction, GridCoord current)
    {
        switch (direction)
        {
            case (Up):
                return grid.GetUp(current);
            case (Down):
                return grid.GetDown(current);
            case (Right):
                return grid.GetRight(current);
            case (Left):
                return grid.GetLeft(current);
        }

        throw new Exception();
    }

    private HashSet<(int x, int y, char direction)> Traverse(GridCoord? current, Grid grid, char direction)
    {
        var visited = new HashSet<(int x, int y, char direction)>();
        var next = current;
        {
            while (current?.value != null)
            {
                next = MoveInDirection(grid, direction, current);
                if (next is null) break;
                if (next?.value != Wall)
                {
                    current.Direction = direction;
                    current = next;
                    if(visited.Contains((current.Coord.x, current.Coord.y,direction)))
                        return null;
                    visited.Add((current.Coord.x, current.Coord.y,direction));
                }
                else
                    direction = Turn(direction);
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
    public int RunP2()
    {
        var original  = FileService.LoadGrid();
        var path = GridCoords();
        var counter = 0;
        var start = original.items.Single(x => x.Value.value == Up);
        foreach (var places in path)
        {
            if(places.x == start.Value.Coord.x && places.y == start.Value.Coord.y) continue;
            var originalValue = original.items[(places.x, places.y)].value;
            original.items[(places.x, places.y)].value = '#';

            var foundLoop = HashSet(original,start.Value) == null;
            if (foundLoop) counter += 1;
            original.items[(places.x, places.y)].value = originalValue;
        }
        return counter;
    }
}