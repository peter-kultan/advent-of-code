namespace day01;

public static class CalorieCounting
{
    public static void Main()
    {
        Console.WriteLine("Enter file path:");
        
        string path = Console.ReadLine();

        if (!File.Exists(path))
        {
            Console.WriteLine("File doesn't exists");
            return;
        }

        Console.WriteLine(ParseCalories(path));
    }

    public static int ParseCalories(string path)
    {
        int currentCount = 0;
        int currentOrderNum = 1;
        int mostCount = 0;
        int mostOrderNum = 0;

        foreach (var line in File.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (currentCount > mostCount)
                {
                    mostCount = currentCount;
                    mostOrderNum = currentOrderNum;
                }
                currentCount = 0;
                currentOrderNum++;
            }
            else
            {
                currentCount += Int32.Parse(line);
            }
        }
        return mostCount;
    }
}