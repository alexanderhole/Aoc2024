using System.Diagnostics;
using System.Linq.Expressions;
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
        var grid = FileService.LoadGrid();
        var start = grid.items.Single(x => x.Value.value == Up);
        var visited = Traverse(start.Value, grid, Up);
        return visited.Distinct().Count();
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

    private List<GridCoord> Traverse(GridCoord? current, Grid grid, char direction)
    {
        var visited = new List<GridCoord>();
        var next = current;
        {
            while (current?.value != null)
            {
                next = MoveInDirection(grid, direction, current);

                if (next?.value != Wall)
                {
                    current = next;
                    visited.Add(current);
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
        return 0;
    }
}