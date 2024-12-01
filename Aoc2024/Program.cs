// See https://aka.ms/new-console-template for more information

using Aoc2024.Days._1;
using Aoc2024.Interfaces;
using Microsoft.Extensions.DependencyInjection;
Environment.SetEnvironmentVariable("test", "true");
const int currentDay = 1;

var serviceProvider = new ServiceCollection()
    .AddKeyedSingleton<IDay, Day1>(1)
    .BuildServiceProvider();
for (int i = 1; i <= currentDay; i++)
{
    Console.WriteLine($"Day 1 P1 : {serviceProvider.GetKeyedService<IDay>(i).RunP1()}");
    Console.WriteLine($"Day 1 P2 : {serviceProvider.GetKeyedService<IDay>(i).RunP2()}");
}