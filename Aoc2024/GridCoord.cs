namespace Aoc2024;

public class GridCoord
{
    public (int x, int y) Coord;
    public (bool up, bool left, bool right, bool bottom) Fences = (true, true, true, true);
    public char Value;

    public int FenceCount =>
        (Fences.right ? 1 : 0) +
        (Fences.bottom ? 1 : 0) +
        (Fences.up ? 1 : 0) +
        (Fences.left ? 1 : 0);
}