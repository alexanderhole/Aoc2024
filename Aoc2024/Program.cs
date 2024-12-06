// See https://aka.ms/new-console-template for more information

using System.Data.SqlTypes;
using System.Diagnostics;
using Aoc2024.Days._1;
using Aoc2024.Days._2;
using Aoc2024.Interfaces;
using Microsoft.Extensions.DependencyInjection;
Environment.SetEnvironmentVariable("test", "false");
const int currentDay = 6;


var serviceProvider = new ServiceCollection()
    .AddKeyedSingleton<IDay, Day1>(1)
    .AddKeyedSingleton<IDay, Day2>(2)
    .AddKeyedSingleton<IDay, Day3>(3)
    .AddKeyedSingleton<IDay, Day4>(4)
    .AddKeyedSingleton<IDay, Day5>(5)
    .AddKeyedSingleton<IDay, Day6>(6)



    .BuildServiceProvider();
for (int i = 1; i <= currentDay; i++)
{
    Stopwatch sw = new Stopwatch();
    sw.Start();
    var runP1 = serviceProvider.GetKeyedService<IDay>(i).RunP1();
    sw.Stop();
    Console.WriteLine($"Day {i} P1 : {runP1} took {sw.ElapsedMilliseconds}ms");
    sw.Reset();
    sw.Start();
    var runP2 = serviceProvider.GetKeyedService<IDay>(i).RunP2();
    sw.Stop();
    Console.WriteLine($"Day {i} P2 : {runP2} took {sw.ElapsedMilliseconds}ms");
}