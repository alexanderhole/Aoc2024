using System.Security.Cryptography.X509Certificates;

namespace Aoc2024.Days._12;

public class Day13() : BaseDay(13), IDay
{
    public dynamic RunP1()
    {
        List<long> costs = new();
        var machines = FileService.LoadLines().Chunk(3).Select(x => Machine(x)).ToList();
        foreach (var machine in machines)
        {
            costs.Add(CalcMachine(machine));
        }

        return costs.Sum();
    }

    private long CalcMachine(Machine machine)
    {
        var machinePrizeX = machine.PrizeX / machine.B.X * 3;
        var prizeX = machine.PrizeX / (machine.A.X);
        if (machinePrizeX > prizeX)
        {
            for (int i = (int)machinePrizeX; i > 0; i--)
            {
                var leftOver = machine.PrizeX - (machine.A.X * i);
                if (leftOver % machine.B.X == 0)
                {
                    //a presses 
                    var bpresses = leftOver / machine.B.X;
                    //check y
                    if (machine.PrizeY == (i * machine.A.Y) + (bpresses * machine.B.Y))
                        return GetCost((i,bpresses));
                }
            }
        }
        else
        {
            for (int i = (int)prizeX; i > 0; i--)
            {
                var leftOver = machine.PrizeX - (machine.B.X * i);
                if (leftOver % machine.A.X == 0)
                {
                    //a presses 
                    var bpresses = leftOver / machine.A.X;
                    //check y
                    if (machine.PrizeY == (i * machine.B.Y) + (bpresses * machine.A.Y))
                        return GetCost((bpresses, i));
                }
            }
        }

        return 0;
    }
    private long GetCost((long a, long b) potential)
    {
        return (potential.a * 3) + potential.b;
    }

    private Machine Machine(string[] arg, bool p2 = false)
    {
        var buttonAText = arg[0].Split(":")[1].Trim();
        var buttonBText = arg[1].Split(":")[1].Trim();
        var prizeText = arg[2].Split(":")[1].Trim();
        var machine = new Machine()
        {
            A = new Button()
            {
                X = int.Parse(buttonAText.Split(",")[0].Split("+")[1]),
                Y = int.Parse(buttonAText.Split(",")[1].Split("+")[1])
            },
            B = new Button()
            {
                X = int.Parse(buttonBText.Split(",")[0].Split("+")[1]),
                Y = int.Parse(buttonBText.Split(",")[1].Split("+")[1])
            },
            PrizeX = long.Parse(prizeText.Split(",")[0].Split("=")[1]),
            PrizeY = long.Parse(prizeText.Split(",")[1].Split("=")[1])
        };
        if (p2)
        {
            machine.PrizeX = long.Parse("1000000000" + prizeText.Split(",")[0].Split("=")[1]);
            machine.PrizeY = long.Parse("1000000000" + prizeText.Split(",")[1].Split("=")[1]);
        }

        return machine;
    }

    public dynamic RunP2()
    {
        List<long> costs = new();
        var count = 0;
        var machines = FileService.LoadLines().Chunk(3).Select(x => Machine(x, true)).ToList();
        foreach (var machine in machines)
        {
            count += 1;
            Console.WriteLine($"machine:{count}");
            costs.Add(CalcMachine(machine));
        }

        return costs.Sum();
    }
}

public class Button

{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Machine
{
    public Button A { get; set; }
    public long PrizeY { get; set; }
    public Button B { get; set; }
    public long PrizeX { get; set; }
}