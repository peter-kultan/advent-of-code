namespace day02;

public static class RockPaperScissors
{
    private static readonly Dictionary<string, int> ResponsePoints = new()
    {
        { "A", 1 },
        { "X", 1 },
        { "B", 2 },
        { "Y", 2 },
        { "C", 3 },
        { "Z", 3 }
    };

    private static readonly Dictionary<string, string> WinsAgaints = new()
    {
        { "A", "Z" },
        { "B", "X" },
        { "C", "Y" }
    };

    private static readonly Dictionary<string, string> LoseAgaints = new()
    {
        { "A", "Y" },
        { "B", "Z" },
        { "C", "X" }
    };

    public static void Main()
    {
        Console.WriteLine("Write file path:");

        var path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("File doesn't exists");
            return;
        }

        Console.WriteLine(Game(path));
        Console.WriteLine(GameWinLoseDraw(path));
    }

    public static int Game(string path)
    {
        var secondPlayer = 0;

        foreach (var line in File.ReadLines(path))
        {
            var splitedLine = line.Split(' ');
            var first = splitedLine[0];
            var second = splitedLine[1];

            secondPlayer += ResponsePoints[second];

            if ((first.Equals("A") && second.Equals("X")) ||
                (first.Equals("B") && second.Equals("Y")) ||
                (first.Equals("C") && second.Equals("Z")))
                secondPlayer += 3;
            else if (!WinsAgaints[first].Equals(second)) secondPlayer += 6;
        }

        return secondPlayer;
    }

    public static int GameWinLoseDraw(string path)
    {
        var secondPlayer = 0;

        foreach (var line in File.ReadLines(path))
        {
            var splitedLine = line.Split(' ');
            var first = splitedLine[0];
            var second = splitedLine[1];

            if (second.Equals("X"))
                secondPlayer += ResponsePoints[WinsAgaints[first]];
            else if (second.Equals("Y"))
                secondPlayer += 3 + (first.Equals("A") ? 1 : first.Equals("B") ? 2 : 3);
            else
                secondPlayer += 6 + ResponsePoints[LoseAgaints[first]];
        }

        return secondPlayer;
    }
}