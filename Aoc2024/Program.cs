// See https://aka.ms/new-console-template for more information

using Aoc2024.Days._1;
using Aoc2024.Days._2;
using Aoc2024.Interfaces;
using Microsoft.Extensions.DependencyInjection;
Environment.SetEnvironmentVariable("test", "false");
const int currentDay = 5;


var serviceProvider = new ServiceCollection()
    .AddKeyedSingleton<IDay, Day1>(1)
    .AddKeyedSingleton<IDay, Day2>(2)
    .AddKeyedSingleton<IDay, Day3>(3)
    .AddKeyedSingleton<IDay, Day4>(4)
    .AddKeyedSingleton<IDay, Day5>(5)


    .BuildServiceProvider();
for (int i = 1; i <= currentDay; i++)
{
    Console.WriteLine($"Day {i} P1 : {serviceProvider.GetKeyedService<IDay>(i).RunP1()}");
    Console.WriteLine($"Day {i} P2 : {serviceProvider.GetKeyedService<IDay>(i).RunP2()}");
}