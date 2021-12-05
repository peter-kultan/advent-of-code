using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day04
{
    class Bingo
    {
        public static int Part01(String path)
        {
            string[] text = File.ReadAllLines(path);
            string[] draws = text[0].Split(',');
            List<List<string>> players = new List<List<string>>();
            for (int i = 2; i < text.Length; i++)
            {
                if (text[i] == "")
                {
                    continue;
                }
                players.Add(new List<string>(text[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
            }

            foreach (string draw in draws)
            {
                foreach (List<string> line in players)
                {
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (line[i] == draw)
                        {
                            line[i] = "-1";
                        }
                    }
                }
                int winner = Bingo.find_winner(players);
                if (winner != -1)
                {
                    return Bingo.calculate(players, winner) * Convert.ToInt32(draw);
                }
            }
            return 0;
        }


        static int calculate(List<List<String>> players, int winner)
        {
            int sum = 0;
            for (int i = winner; i < winner + 5; i++)
            {
                foreach (String num in players[i])
                {
                    if (num != "-1")
                    {
                        sum += Convert.ToInt32(num);
                    }
                }
            }
            return sum;
        }

        static int find_winner(List<List<String>> players)
        {
            for (int i = 0; i < players.Count; i += 5)
            {
                for (int x = 0; x < 5; x++)
                {
                    int sum_row = 0;
                    int sum_column = 0;
                    for (int y = 0; y < 5; y++)
                    {
                        sum_row += Convert.ToInt32(players[i + x][y]);
                        sum_column += Convert.ToInt32(players[i + y][x]);
                        if (sum_row > 0 && sum_column > 0)
                        {
                            break;
                        }
                    }
                    if (sum_row == -5 || sum_column == -5)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int Part02(String path)
        {
            int last_still_playing = -1;
            string[] text = File.ReadAllLines(path);
            string[] draws = text[0].Split(',');
            List<List<string>> players = new List<List<string>>();
            for (int i = 2; i < text.Length; i++)
            {
                if (text[i] == "")
                {
                    continue;
                }
                players.Add(new List<string>(text[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));
            }

            foreach (string draw in draws)
            {
                foreach (List<string> line in players)
                {
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (line[i] == draw)
                        {
                            line[i] = "-1";
                        }
                    }
                }
                int still_playing = Bingo.still_playing(players);
                if (still_playing == -1)
                {
                    return Bingo.calculate(players, last_still_playing) * Convert.ToInt32(draw);
                }
                else
                {
                    last_still_playing = still_playing;
                }
            }
            return 0;
        }

        static int still_playing(List<List<string>> players)
        {
            for (int i = 0; i < players.Count; i += 5)
            {
                bool is_winning = false;
                for (int x = 0; x < 5; x++)
                {
                    int sum_row = 0;
                    int sum_column = 0;
                    for (int y = 0; y < 5; y++)
                    {
                        sum_row += Convert.ToInt32(players[i + x][y]);
                        sum_column += Convert.ToInt32(players[i + y][x]);
                        if (sum_row > 0 && sum_column > 0)
                        {
                            break;
                        }
                    }
                    if (sum_row == -5 || sum_column == -5)
                    {
                        is_winning = true;
                        break;
                    }
                }
                if (!is_winning)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Bingo.Part01("input01.txt"));
            Console.WriteLine(Bingo.Part02("input02.txt"));
        }
    }
}
