namespace Aoc2024.Days._2;

public class Day8() : BaseDay(8), IDay
{
    public dynamic RunP1()
    {
        var grid = FileService.LoadGrid();
        var antennas = new List<GridCoord>();
        var antiNodes = new List<(int, int)>();
        foreach (var item in grid.Items)
            if (item.Value != '.')
                antennas.Add(item);

        foreach (var antennaTypes in antennas.GroupBy(x => x.Value))
        foreach (var antennaType in antennaTypes)
            antiNodes.AddRange(antennaTypes.Where(x => x != antennaType)
                .Select(x =>
                {
                    var findDistancex = FindDistance(antennaType.Coord.x, x.Coord.x);
                    var findDistancey = FindDistance(antennaType.Coord.y, x.Coord.y);
                    if (antennaType.Coord.x >= x.Coord.x)
                        findDistancex = antennaType.Coord.x + findDistancex;
                    else
                        findDistancex = antennaType.Coord.x - findDistancex;
                    if (antennaType.Coord.y >= x.Coord.y)
                        findDistancey = antennaType.Coord.y + findDistancey;
                    else
                        findDistancey = antennaType.Coord.y - findDistancey;
                    return (findDistancex,
                        findDistancey);
                }));
        return antiNodes.ToHashSet()
            .Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < grid.MaxX && x.Item2 < grid.MaxY).Count();
    }

    public dynamic RunP2()
    {
        var grid = FileService.LoadGrid();
        var antennas = new List<GridCoord>();
        var antiNodes = new HashSet<(int, int)>();
        foreach (var item in grid.Items)
            if (item.Value != '.')
            {
                antennas.Add(item);
                antiNodes.Add((item.Coord.x, item.Coord.y));
            }

        foreach (var antennaTypes in antennas.GroupBy(x => x.Value))
        foreach (var antennaType in antennaTypes)
        {
            var others = antennaTypes.Where(x => x != antennaType);
            foreach (var other in others)
            {
                var findDistancex = antennaType.Coord.x - other.Coord.x;
                var findDistancey = antennaType.Coord.y - other.Coord.y;
                var currentX = findDistancex + antennaType.Coord.x;
                var currentY = findDistancey + antennaType.Coord.y;
                while (currentX <= grid.MaxX && currentX >= 0
                                             && currentY >= 0 && currentY <= grid.MaxY)
                {
                    antiNodes.Add((currentX, currentY));
                    currentX = findDistancex + currentX;
                    currentY = findDistancey + currentY;
                }
            }
        }

        return antiNodes.Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < grid.MaxX && x.Item2 < grid.MaxY).Count();
    }

    public int FindDistance(int nr1, int nr2)
    {
        return Math.Abs(nr1 - nr2);
    }
}