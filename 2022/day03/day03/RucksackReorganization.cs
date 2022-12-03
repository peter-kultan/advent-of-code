namespace day03;

public static class RucksackReorganization
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

        Console.WriteLine(Duplicate(input));
        Console.WriteLine(DuplicateFindGroups(input));
    }

    public static int Duplicate(string path)
    {
        var duplicateSum = 0;

        foreach (var line in File.ReadLines(path))
        {
            var firstHalf = line.Substring(0, line.Length / 2);
            var secondHalf = line.Substring(line.Length / 2, line.Length / 2);

            foreach (var ch in firstHalf.Intersect(secondHalf))
            {
                duplicateSum += (char.IsLower(ch) ? (int)ch + 1 - (int)'a' : (int)ch - (int)'A' + 27);
            }
        }

        return duplicateSum;
    }

    public static int DuplicateFindGroups(string path)
    {
        var duplicateSum = 0;
        var currentLine = 0;

        var groupDuplicate = new HashSet<char>();

        foreach (var line in File.ReadLines(path))
        {
            currentLine++;

            if (currentLine % 3 == 1)
            {
                groupDuplicate = new HashSet<char>(line);
            }
            else
            {
                groupDuplicate.IntersectWith(line);

                if (currentLine % 3 == 0)
                {
                    duplicateSum += (char.IsLower(groupDuplicate.First())
                        ? (int)groupDuplicate.First() + 1 - (int)'a'
                        : (int)groupDuplicate.First() - (int)'A' + 27);
                }
            }
        }

        return duplicateSum;
    }
}