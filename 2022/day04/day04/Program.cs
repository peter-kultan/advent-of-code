namespace day04;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Write input path:");
        var input = Console.ReadLine();

        if (!File.Exists(input))
        {
            Console.WriteLine("File doesn't exists");

            return;
        }

        Console.WriteLine("Full or partial?:");

        var full = Console.ReadLine();

        Console.WriteLine(CampCleanup.Count(input, full.Contains('f')));
    }

    
}

public static class CampCleanup
{
    public static int Count(string path, bool returnFullOverlap)
    {
        var fullOverlap = 0;
        var partialOverlap = 0;
        foreach (var line in File.ReadLines(path))
        {
            var divided = line.Split(',');
            var first = SplitIntervals(divided[0]);
            var second = SplitIntervals(divided[1]);

            if ((first.Item1 <= second.Item1 && first.Item2 >= second.Item2) ||
                (first.Item1 >= second.Item1 && first.Item2 <= second.Item2))
            {
                fullOverlap++;
                partialOverlap++;
            }
            else if ((first.Item1 >= second.Item1 && first.Item1 <= second.Item2) ||
                     (first.Item2 >= second.Item1 && first.Item2 <= second.Item2))
            {
                partialOverlap++;
            }

        }
        return returnFullOverlap ? fullOverlap : partialOverlap;
    }

    private static Tuple<int, int> SplitIntervals(string section)
    {
        var splitted = section.Split("-");
        return Tuple.Create(int.Parse(splitted[0]), int.Parse(splitted[1]));
    }
}