namespace Aoc2024;

public class Grid
{
    public List<GridCoord?> items;

    public GridCoord? GetLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x - 1 && x.Coord.y == coord.Coord.y);
    }
    public GridCoord? GetRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y);
    }
    public GridCoord? GetUp(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetDown(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x && x.Coord.y == coord.Coord.y + 1);
    }
    public GridCoord? GetUpLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x - 1 && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetUpRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y - 1);
    }
    public GridCoord? GetDownRight(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x + 1 && x.Coord.y == coord.Coord.y + 1);
    }
    public GridCoord? GetDownLeft(GridCoord coord)
    {
        return items.SingleOrDefault(x => x.Coord.x == coord.Coord.x -1 && x.Coord.y == coord.Coord.y + 1);
    }
}