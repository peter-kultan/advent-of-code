using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day05
{
    class Vents
    {
        public static int part01(string path)
        {
            string[] text = File.ReadAllLines(path);
            Dictionary<Tuple<int, int>, int> values = new Dictionary<Tuple<int, int>, int>();
            foreach (string line in text)
            {
                values = find_straight(values, line);
            }

            return sum_overlaps(values);
        }


        public static int part02(string path)
        {
            string[] text = File.ReadAllLines(path);
            Dictionary<Tuple<int, int>, int> values = new Dictionary<Tuple<int, int>, int>();
            foreach (string line in text)
            {
                values = find_straight(values, line);
                values = find_diagonal(values, line);
            }
            return sum_overlaps(values);
        }

        private static Dictionary<Tuple<int, int>, int> find_straight(Dictionary<Tuple<int, int>, int> values, string line)
        {
            string[] splitted_line = line.Split(" -> ");
            string[] first_enter = splitted_line[0].Split(',');
            string[] second_enter = splitted_line[1].Split(',');
            if (first_enter[0] == second_enter[0])
            {
                int first_y = Math.Min(Convert.ToInt32(first_enter[1]), Convert.ToInt32(second_enter[1]));
                int last_y = Math.Max(Convert.ToInt32(first_enter[1]), Convert.ToInt32(second_enter[1]));
                for (int y = first_y; y <= last_y; y++)
                {
                    Tuple<int, int> key = new Tuple<int, int>(Convert.ToInt32(first_enter[0]), y);
                    int a = 0;
                    if (values.TryGetValue(key, out a))
                    {
                        values[key] += 1;
                    }
                    else
                    {
                        values.Add(key, 1);
                    }
                }
            }
            else
            {
                if (first_enter[1] == second_enter[1])
                {
                    int first_x = Math.Min(Convert.ToInt32(first_enter[0]), Convert.ToInt32(second_enter[0]));
                    int last_x = Math.Max(Convert.ToInt32(first_enter[0]), Convert.ToInt32(second_enter[0]));
                    for (int x = first_x; x <= last_x; x++)
                    {
                        Tuple<int, int> key = new Tuple<int, int>(x, Convert.ToInt32(first_enter[1]));
                        int a = 0;
                        if (values.TryGetValue(key, out a))
                        {
                            values[key] += 1;
                        }
                        else
                        {
                            values.Add(key, 1);
                        }
                    }
                }
            }
            return values;
        }

        private static int sum_overlaps(Dictionary<Tuple<int, int>, int> values)
        {
            int result = 0;
            foreach (int value in values.Values)
            {
                if (value > 1)
                {
                    result += 1;
                }
            }
            return result;
        }

        private static Dictionary<Tuple<int, int>, int> find_diagonal(Dictionary<Tuple<int, int>, int> values, string line)
        {
            string[] splitted_line = line.Split(" -> ");
            string[] first_enter = splitted_line[0].Split(',');
            string[] second_enter = splitted_line[1].Split(',');
            int first_x = Convert.ToInt32(first_enter[0]);
            int first_y = Convert.ToInt32(first_enter[1]);
            int last_x = Convert.ToInt32(second_enter[0]);
            int last_y = Convert.ToInt32(second_enter[1]);

            int min_x = Math.Min(first_x, last_x);
            int min_y = Math.Min(first_y, last_y);
            int actuall_x = first_x;
            int actuall_y = first_y;
            if (Math.Abs(first_x - last_x) == Math.Abs(first_y - last_y))
            {
                for (int i = 0; i <= Math.Abs(first_x - last_x); i++)
                {
                    Tuple<int, int> key = new Tuple<int, int>(actuall_x, actuall_y);
                    int a = 0;
                    if (values.TryGetValue(key, out a))
                    {
                        values[key] += 1;
                    }
                    else
                    {
                        values.Add(key, 1);
                    }
                    actuall_x += first_x == min_x ? 1 : -1;
                    actuall_y += first_y == min_y ? 1 : -1;
                }
            }
            return values;
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Vents.part01("input01.txt"));
            Console.WriteLine(Vents.part02("input02.txt"));
        }
    }
}
