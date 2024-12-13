using System.Security.Cryptography.X509Certificates;

namespace Aoc2024.Days._12;

public class Day13() : BaseDay(13), IDay
{
    public dynamic RunP1()
    {
        List<long> costs = new();
        var machines = FileService.LoadLines().Chunk(3).Select(Machine).ToList();
        foreach (var machine in machines)
        {
            List<long> currentCosts = new();
            var potentials = GetPotentials(machine.PrizeX, machine.A.X, machine.B.X);
            var potentialsY = GetPotentials(machine.PrizeY, machine.A.Y, machine.B.Y);
            var actualpotentials = potentials.Where(x => potentialsY.Where(y => y.a == x.a && y.b ==x.b).Count() > 0);            foreach (var potential in actualpotentials)
            {
                currentCosts.Add(GetCost(potential));
            }
            if(currentCosts.Count() > 0)
            costs.Add(currentCosts.Min());
        }
        
        return costs.Sum();
    }

    private long GetCost((long a, long b) potential)
    {
        return (potential.a * 3) + potential.b;
    }
    private Machine MachineP2(string[] arg)
    {
        var buttonAText = arg[0].Split(":")[1].Trim();
        var buttonBText = arg[1].Split(":")[1].Trim();
        var prizeText = arg[2].Split(":")[1].Trim();
        return new Machine()
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
            PrizeX = long.Parse("10000000000000" + prizeText.Split(",")[0].Split("=")[1]),
            PrizeY = long.Parse("10000000000000" + prizeText.Split(",")[1].Split("=")[1])
        };
    }
    private Machine Machine(string[] arg)
    {
        var buttonAText = arg[0].Split(":")[1].Trim();
        var buttonBText = arg[1].Split(":")[1].Trim();
        var prizeText = arg[2].Split(":")[1].Trim();
        return new Machine()
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
    }

    public dynamic RunP2()
    {
        List<long> costs = new();
        var machines = FileService.LoadLines().Chunk(3).Select(MachineP2).ToList();
        foreach (var machine in machines)
        {
            Console.WriteLine("machine 1");
            List<long> currentCosts = new();
            var potentials = GetPotentialsP2(machine.PrizeX, machine.A.X, machine.B.X);
            var potentialsY = GetPotentialsP2(machine.PrizeY, machine.A.Y, machine.B.Y);
            var actualpotentials = potentials.Where(x => potentialsY.Where(y => y.a == x.a && y.b ==x.b).Count() > 0);            foreach (var potential in actualpotentials)
            {
                currentCosts.Add(GetCost(potential));
            }
            if(currentCosts.Count() > 0)
                costs.Add(currentCosts.Min());
        }
        
        return costs.Sum();
    }

    private List<(long a, long b)> GetPotentials(long target, int offsetA, int offsetB)
    {
        var list = new List<(long a, long b)>();
        for(int i = 100; i >= 0; i--)
        {
            var res = i * offsetB;

            var a = (target-res) % offsetA;
            if(a == 0)
            {
                var b = (target - res) / offsetA;
                //if (i <= 100 && b <= 100)
                {
                    //if ((i * offsetA) + (b * offsetB) == target)
                    {
                        list.Add((b,i));
                    }
                    //else if ((b * offsetA) + (i * offsetB) == target)
                    {
                     //   list.Add((b,i));
                    }
                    //else
                    {
                       // throw new Exception();
                    }
                }
            }
        }

        return list;
    }
    private List<(long a, long b)> GetPotentialsP2(long target, int offsetA, int offsetB)
    {
        var max = target / offsetA;
        if (offsetB < offsetA)
            max = target / offsetB;
        var list = new List<(long a, long b)>();
        for(int i = 1; i <= max; i = i + offsetB)
        {
            var res = i * offsetB;

            var a = (target-res) % offsetA;
            if(a == 0)
            {
                var b = (target - res) / offsetA;
                //if (i <= 100 && b <= 100)
                {
                    //if ((i * offsetA) + (b * offsetB) == target)
                    {
                        list.Add((b,i));
                    }
                    //else if ((b * offsetA) + (i * offsetB) == target)
                    {
                        //   list.Add((b,i));
                    }
                    //else
                    {
                        // throw new Exception();
                    }
                }
            }
        }

        return list;
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