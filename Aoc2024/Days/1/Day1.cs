namespace Aoc2024.Days._1;

public class Day1() : BaseDay(1), IDay
{
    public dynamic RunP2()
    {
        var (leftList, right) = GetSortedLists();
        return leftList.Sum(left => left * right.Count(x => x == left));
    }

    public dynamic RunP1()
    {
        var (left, right) = GetSortedLists();
        var distance = 0;
        for (var i = 0; i < left.Count; i++)
            if (left[i] > right[i])
                distance += left[i] - right[i];
            else if (left[i] < right[i])
                distance += right[i] - left[i];

        return distance;
    }

    private (List<int> left, List<int> right) GetSortedLists()
    {
        var loadLines = FileService.LoadLines();
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in loadLines)
        {
            var nums = line.Split("   ");
            left.Add(int.Parse(nums[0]));
            right.Add(int.Parse(nums[1]));
        }

        left.Sort();
        right.Sort();
        return (left, right);
    }
}