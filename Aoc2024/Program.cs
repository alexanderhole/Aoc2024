// See https://aka.ms/new-console-template for more information

using Aoc2024.Days._1;
using Aoc2024.Days._2;
using Aoc2024.Interfaces;
using Microsoft.Extensions.DependencyInjection;
Environment.SetEnvironmentVariable("test", "false");
const int currentDay = 2;

var serviceProvider = new ServiceCollection()
    .AddKeyedSingleton<IDay, Day1>(1)
    .AddKeyedSingleton<IDay, Day2>(2)
    .BuildServiceProvider();
for (int i = 1; i <= currentDay; i++)
{
    Console.WriteLine($"Day 1 P1 : {serviceProvider.GetKeyedService<IDay>(i).RunP1()}");
    Console.WriteLine($"Day 1 P2 : {serviceProvider.GetKeyedService<IDay>(i).RunP2()}");
}