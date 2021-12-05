using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day03
{
    class part01
    {

        public static int report(string path)
        {
            string[] text = File.ReadAllLines(path);
            int[] array = new int[text[0].Length];

            string gamma = "";
            string epsilon = "";


            foreach (string line in text)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        array[i]++;
                    }
                }
            }
            foreach (int num in array)
            {
                if (num <= text.Length / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }
    }

    class part02
    {
        public static int levels(string path)
        {
            string[] text = File.ReadAllLines(path);
            int[] array = new int[text[0].Length];
            List<string> oxygen_levels = new List<string>(text);
            List<string> co2_levels = new List<string>(text);

            foreach (string line in text)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        array[i]++;
                    }
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (oxygen_levels.Count > 1)
                {
                    oxygen_levels = oxygen_levels.Where(x => x[i] == ((array[i] <= text.Length / 2) ? '1' : '0')).ToList();
                }
                if (co2_levels.Count > 1)
                {
                    co2_levels = co2_levels.Where(x => x[i] == ((array[i] <= text.Length / 2) ? '0' : '1')).ToList();
                }
            }
            return Convert.ToInt32(oxygen_levels[0], 2) * Convert.ToInt32(co2_levels[0], 2);
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine(part01.report("input01.txt"));
            Console.WriteLine(part02.levels("input02.txt"));
        }
    }
}
