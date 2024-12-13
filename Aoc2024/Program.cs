Environment.SetEnvironmentVariable("test", "false");
const int currentDay = 13;


var serviceProvider = new ServiceCollection()
    .AddKeyedSingleton<IDay, Day1>(1)
    .AddKeyedSingleton<IDay, Day2>(2)
    .AddKeyedSingleton<IDay, Day3>(3)
    .AddKeyedSingleton<IDay, Day4>(4)
    .AddKeyedSingleton<IDay, Day5>(5)
    .AddKeyedSingleton<IDay, Day6>(6)
    .AddKeyedSingleton<IDay, Day7>(7)
    .AddKeyedSingleton<IDay, Day8>(8)
    .AddKeyedSingleton<IDay, Day9>(9)
    .AddKeyedSingleton<IDay, Day10>(10)
    .AddKeyedSingleton<IDay, Day11>(11)
    .AddKeyedSingleton<IDay, Day12>(12)
    .AddKeyedSingleton<IDay, Day13>(13)

    .BuildServiceProvider();
for (var i = currentDay; i <= currentDay; i++)
{
    var sw = new Stopwatch();
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