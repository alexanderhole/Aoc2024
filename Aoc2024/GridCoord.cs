namespace Aoc2024;

public class GridCoord
{
    public (int x, int y) Coord;
    public char value;
    public char Direction;
    public (bool up, bool left, bool right, bool bottom) fences = (true,true,true,true);

    public int FenceCount
    {
        get
        {
            return (fences.right ? 1 : 0) +
                   (fences.bottom ? 1 : 0) +
                   (fences.up ? 1 : 0) +
                   (fences.left ? 1 : 0);
        }
    }
}