namespace day06;

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


        Console.WriteLine(TuningTrouble.FindMarker(input, 4));
        Console.WriteLine(TuningTrouble.FindMarker(input, 14));
    }
}

public static class TuningTrouble
{
    public static int FindMarker(string path, int nubmerOfDistinctLetters)
    {
        var queue = new Queue<char>();
        var position = 0;
        using (StreamReader sr = new StreamReader(path))
        {
            while (sr.Peek() >= 0)
            {
                queue.Enqueue((char)sr.Read());
                position++;

                if (queue.Count == nubmerOfDistinctLetters)
                {
                    if (queue.ToHashSet().Count() == nubmerOfDistinctLetters)
                    {
                        break;
                    }
                    queue.Dequeue();
                }
            }
        }
        return position;
    }
}