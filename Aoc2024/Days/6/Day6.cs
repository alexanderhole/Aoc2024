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
        return visited.DistinctBy(x => (x.Key.x, x.Key.y)).Count();
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

    private Dictionary<(int x, int y, char direction), GridCoord> Traverse(GridCoord? current, Grid grid, char direction)
    {
        var visited = new Dictionary<(int x, int y, char direction), GridCoord>();
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
                    if(visited.ContainsKey((current.Coord.x, current.Coord.y,direction)))
                        return null;
                    visited[(current.Coord.x, current.Coord.y,direction)] = current;
                }
                else
                    direction = Turn(direction);
            }
        }
        return visited;
    }
    
    
    private char[,] Visited(GridCoord? current, Grid grid, char direction)
    {
        var visited = new char[grid.maxX, grid.maxY];
        var next = current;
        while (current?.value != null)
        {
            next = MoveInDirection(grid, direction, current);
            if (next is null) break;
            if (next?.value == Wall)
                direction = Turn(direction);
            else
            {
                if(visited[next.Coord.x, next.Coord.y] == direction )
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
    public int RunP2()
    {
        var original  = FileService.LoadGrid();
        var path = GridCoords();
        var counter = 0;
        var start = original.Start;
        foreach (var places in path.DistinctBy(x => (x.Key.x, x.Key.y)))
        {
            var originalValue = original.items[places.Value.Coord.x, places.Value.Coord.y].value;
            original.items[places.Value.Coord.x, places.Value.Coord.y].value = Wall;

            var foundLoop = Visited(start, original,start.value) == null;
            if (foundLoop) counter += 1;
            original.items[places.Value.Coord.x, places.Value.Coord.y].value = originalValue;
        }
        return counter;
    }
}