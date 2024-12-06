namespace Aoc2024;

public class Grid
{
    //public List<GridCoord?> items;
    public GridCoord[,] items;
    public int maxX;
    public int maxY;
    public GridCoord Start { get; set; }

    public GridCoord? GetLeft(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x - 1, coord.Coord.y);
    }

    public GridCoord? GetRight(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x + 1, coord.Coord.y);
    }

    public GridCoord? GetUp(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x, coord.Coord.y - 1);
    }

    private GridCoord? safeGetItem(int x, int y)
    {
        if(x < maxX && x >= 0 && y < maxY && y >= 0)
          //  return null;
            return items[x, y];
        return null;
    }

    public GridCoord? GetDown(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x, coord.Coord.y + 1);
    }

    public GridCoord? GetUpLeft(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x - 1, coord.Coord.y - 1);
    }

    public GridCoord? GetUpRight(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x + 1, coord.Coord.y - 1);
    }

    public GridCoord? GetDownRight(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x + 1, coord.Coord.y + 1);
    }

    public GridCoord? GetDownLeft(GridCoord coord)
    {
        return safeGetItem(coord.Coord.x - 1, coord.Coord.y + 1);
    }
}