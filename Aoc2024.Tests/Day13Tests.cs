using Aoc2024.Days._12;
using Microsoft.VisualBasic;

namespace Aoc2024.Tests;

public class Day13Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Theory]
    [TestCase(1,2,3,4)]
    public void Test1(long first, long second, long target,long result)
    {
        Day13 day = new Day13();
        day.GetLargestPossible(first,second,target)
    }
}