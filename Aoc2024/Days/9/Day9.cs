namespace Aoc2024.Days._2;

public class Day9() : BaseDay(9), IDay
{
    public dynamic RunP1()
    {
        return 0;
        var numbers = Numbers();
        long checksum = 0;
        for (var i = 0; i < numbers.Length; i++) checksum += i * numbers[i];
        return checksum;
    }

    public dynamic RunP2()
    {
        var numbers = GetFileBlocksP2(); //00...111...2...333.44.5555.6666.777.888899
        var original = new List<FileThingy>(numbers);
        var working = true;
        //while (working) 
        {
            for (var i = numbers.Count - 1; i >= 0; i--)
            {
                var fileThingy = original[i];

                var indexOfFileThingy = numbers.IndexOf(fileThingy);
                var slot = numbers.FirstOrDefault(x =>
                    x.FreeLength >= fileThingy.Contents.Count && indexOfFileThingy >= numbers.IndexOf(x));

                if (slot != fileThingy && slot != null)
                {
                    numbers[indexOfFileThingy - 1].FreeLength += fileThingy.Contents.Count + fileThingy.FreeLength;
                    fileThingy.FreeLength = slot.FreeLength - fileThingy.Contents.Count;
                    slot.FreeLength = 0;
                    numbers.Remove(fileThingy);

                    numbers.Insert(numbers.IndexOf(slot) + 1, fileThingy);
                }
            }
        }
        long checksum = 0;
        var count = 0;
        foreach (var number in numbers)
        {
            number.Contents.AddRange(Enumerable.Repeat(0, number.FreeLength));
            foreach (var num in number.Contents)
            {
                checksum += num * count;
                count += 1;
            }
        }

        return checksum;
    }

    private long[] Numbers()
    {
        var result = GetFileBlocks();
        var stack = new Stack<string>(result);
        for (var i = result.Count - 1; i >= 0; i--)
        {
            if (!result.Take(i).Contains(".")) continue;
            result[result.IndexOf(".")] = result[i];
            result[i] = ".";
        }

        var numbers = result.Where(x => x != ".").Select(x => long.Parse(x)).ToArray();
        return numbers;
    }

    private List<string> GetFileBlocks()
    {
        var result = new List<string>();
        var line = FileService.LoadFile();
        var files = line.ToCharArray().Chunk(2);
        var id = 0;
        foreach (var file in files)
        {
            result.AddRange(Enumerable.Repeat(id.ToString(), int.Parse(file[0].ToString())));
            if (file.Length == 2)
                result.AddRange(Enumerable.Repeat(".", int.Parse(file[1].ToString())));
            id += 1;
        }

        return result;
    }

    private List<FileThingy> GetFileBlocksP2()
    {
        var result = new List<FileThingy>();
        var line = FileService.LoadFile();
        var files = line.ToCharArray().Chunk(2);
        var id = 0;
        foreach (var file in files)
        {
            var a = new FileThingy
            {
                Contents = Enumerable.Repeat(id, int.Parse(file[0].ToString())).ToList()
            };
            if (file.Length == 2)
                a.FreeLength = int.Parse(file[1].ToString());
            result.Add(a);
            id += 1;
        }

        return result;
    }

    public class FileThingy
    {
        public List<int> Contents { get; set; }
        public int FreeLength { get; set; }
    }
}