namespace Aoc2024;

public class Grid
{
    //public List<GridCoord?> items;
    public GridCoord[,] Items;
    public int MaxX;
    public int MaxY;
    public GridCoord Start { get; set; }

    public GridCoord? GetLeft(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x - 1, coord.Coord.y);
    }

    public GridCoord? GetRight(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x + 1, coord.Coord.y);
    }

    public GridCoord? GetUp(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x, coord.Coord.y - 1);
    }

    private GridCoord? SafeGetItem(int x, int y)
    {
        if (x < MaxX && x >= 0 && y < MaxY && y >= 0)
            //  return null;
            return Items[x, y];
        return null;
    }

    public GridCoord? GetDown(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x, coord.Coord.y + 1);
    }

    public GridCoord? GetUpLeft(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x - 1, coord.Coord.y - 1);
    }

    public GridCoord? GetUpRight(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x + 1, coord.Coord.y - 1);
    }

    public GridCoord? GetDownRight(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x + 1, coord.Coord.y + 1);
    }

    public GridCoord? GetDownLeft(GridCoord coord)
    {
        return SafeGetItem(coord.Coord.x - 1, coord.Coord.y + 1);
    }

    public List<GridCoord> FindNeighbours(GridCoord item, char current)
    {
        var neighbours = new List<GridCoord>();
        var neighbour = GetUp(item);
        if (neighbour?.Value == current)
            neighbours.Add(neighbour);
        neighbour = GetDown(item);
        if (neighbour?.Value == current)
            neighbours.Add(neighbour);
        neighbour = GetRight(item);
        if (neighbour?.Value == current)
            neighbours.Add(neighbour);
        neighbour = GetLeft(item);
        if (neighbour?.Value == current)
            neighbours.Add(neighbour);
        return neighbours;
    }
}