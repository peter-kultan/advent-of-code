using System.Text.RegularExpressions;

namespace day05;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Write path to input file:");

        var input = Console.ReadLine();

        if (!File.Exists(input))
        {
            Console.WriteLine("File doesn't exists");
            return;
        }



        Console.WriteLine(SupplyStacks.FindTop(input, true));
    }
}

public static class SupplyStacks
{
    public static string FindTop(string path, bool multipleAtOnce)
    {
        //Regex.Match(@"^(((\[[A-Z]\])|\s\s\s)\s)", )
        var stack = FindStacks(path);

        foreach (var line in File.ReadLines(path))
        {
            if (!Regex.IsMatch(line, "^move (\\d*) from (\\d*) to (\\d*)$"))
            {
                continue;
            }

            var match = Regex.Match(line, @"^move (\d*) from (\d*) to (\d*)$");

            var howMany = int.Parse(match.Groups[1].Value);
            int from = int.Parse(match.Groups[2].Value) - 1;
            int to = int.Parse(match.Groups[3].Value) - 1;
            if (!multipleAtOnce)
            {
                for (var i = 0; i < howMany; i++)
                {
                    MoveBlock(stack, from, to);
                }
            }
            else
            {
                MoveMultipleBlockAtOnce(stack, from, to, howMany);
            }
        }

        return string.Join("", stack.Select(x => x.Pop()));
    }

    private static void MoveMultipleBlockAtOnce(List<Stack<char>> stacks, int from, int to, int number)
    {
        var temp = new Stack<char>();

        for (int i = 0; i < number; i++)
        {
            temp.Push(stacks[from].Pop());
        }

        for (int i = 0; i < number; i++)
        {
            stacks[to].Push(temp.Pop());
        }
    }

    private static void MoveBlock(List<Stack<char>> stacks, int from, int to) => stacks[to].Push(stacks[from].Pop());

        private static List<Stack<char>> FindStacks(string path)
    {
        var lines = new Stack<string>();
        var numberOfStack = 0;

        foreach (var line in File.ReadLines(path))
        {
            if (Regex.IsMatch(line, @"^\s1"))
            {
                numberOfStack = line.Trim().Split(' ').Select(int.Parse).Last();
                break;
            }
            lines.Push(line);
        }

        var fundedStacks = Enumerable.Range(0, numberOfStack).Select(stack => new Stack<char>()).ToList();

        while (lines.Count > 0)
        {
            var stackNum = 0;
            var line = lines.Pop();

            while (stackNum * 4 < line.Length)
            {
                if (Regex.IsMatch(line[(stackNum * 4)..], @"^\s"))
                {
                    stackNum++;
                    continue;
                }

                fundedStacks[stackNum].Push(line[(stackNum * 4 )..][1]);
                stackNum++;
            }
        }

        return fundedStacks;
    }
}