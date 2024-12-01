using System.Text;
using Aoc2024;
namespace Aoc2024.Days._1;

public class Day1
{
    public static int Run()
    {
        var loadLines = Helpers.LoadLines("Days/1/input.txt");
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
        var distance = 0;
        for (int i = 0; i < left.Count; i++)
        {
            if (left[i] > right[i])
                distance += (left[i] - right[i]);
            else if (left[i] < right[i])
                distance += (right[i] - left[i]);
        }

        return distance;
    }
}