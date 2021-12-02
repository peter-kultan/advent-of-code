using System;
using System.IO;

namespace day02
{

    class part01
    {
        public static int navigator(string path)
        {
            int depth = 0;
            int position = 0;
            string[] text = File.ReadAllLines(path);
            foreach (string line in text)
            {
                string[] splited_line = line.Split(" ");
                int unit = Int32.Parse(splited_line[1]);
                switch (splited_line[0])
                {
                    case "forward":
                        position += unit;
                        break;
                    case "up":
                        depth -= unit;
                        break;
                    default:
                        depth += unit;
                        break;
                }
            }
            return depth * position;
        }
    }

    class part02
    {
        public static int navigator(string path)
        {
            int position = 0;
            int depth = 0;
            int aim = 0;
            string[] text = File.ReadAllLines(path);
            foreach (string line in text)
            {
                string[] splited_line = line.Split(" ");
                int unit = Int32.Parse(splited_line[1]);
                switch (splited_line[0])
                {
                    case "forward":
                        position += unit;
                        depth += (unit * aim);
                        break;
                    case "up":
                        aim -= unit;
                        break;
                    default:
                        aim += unit;
                        break;
                }
            }
            return depth * position;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(part01.navigator("input01.txt"));
            Console.WriteLine(part02.navigator("input02.txt"));
        }
    }
}
