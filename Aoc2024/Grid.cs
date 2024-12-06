namespace Aoc2024;

public class Grid
{
    //public List<GridCoord?> items;
    public Dictionary<(int x, int y), GridCoord?> items;
    public int maxX;
    public int maxY;
    public GridCoord? GetLeft(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x - 1, coord.Coord.y));
    }

    public GridCoord? GetRight(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x + 1, coord.Coord.y));
    }

    public GridCoord? GetUp(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x, coord.Coord.y - 1));
    }

    private GridCoord? safeGetItem((int x, int y) valueTuple)
    {
        if(valueTuple.x < maxX && valueTuple.x >= 0 && valueTuple.y < maxY && valueTuple.y >= 0)
        //if(!items.ContainsKey((valueTuple.x, valueTuple.y)))
          //  return null;
            return items[(valueTuple.x, valueTuple.y)];
        return null;
    }

    public GridCoord? GetDown(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x, coord.Coord.y + 1));
    }

    public GridCoord? GetUpLeft(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x - 1, coord.Coord.y - 1));
    }

    public GridCoord? GetUpRight(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x + 1, coord.Coord.y - 1));
    }

    public GridCoord? GetDownRight(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x + 1, coord.Coord.y + 1));
    }

    public GridCoord? GetDownLeft(GridCoord coord)
    {
        return safeGetItem((coord.Coord.x - 1, coord.Coord.y + 1));
    }
}