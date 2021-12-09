using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day08
{

    class SevenSegmentSearch
    {

        private string[] text;

        public SevenSegmentSearch(string path)
        {
            text = LoadFile(path);
        }

        private string[] LoadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public int Part01()
        {
            int result = 0;
            foreach (string line in text)
            {
                foreach (string num in line.Split(" | ")[1].Split(" "))
                {
                    int len = num.Length;
                    if (len == 2 || len == 3 || len == 4 || len == 7)
                    {
                        result++;
                    }
                }
            }
            return result;
        }

        public int Part02()
        {
            int result = 0;
            foreach (string line in text)
            {
                string[] splitted = line.Split(" | ");
                string[] inputs = splitted[0].Split();
                string[] outputs = splitted[1].Split();
                Dictionary<int, string> unique = DetermineUnique(inputs);

                result += DecodeOutput(unique, outputs);
            }
            return result;
        }

        private Dictionary<int, string> DetermineUnique(string[] inputs)
        {
            Dictionary<int, string> unique = new Dictionary<int, string>();
            foreach (string input in inputs)
            {
                string a;
                if (input.Length == 2 && !unique.TryGetValue(1, out a))
                {
                    unique.Add(1, input);
                }
                if (input.Length == 4 && !unique.TryGetValue(4, out a))
                {
                    unique.Add(4, input);
                }
                if (input.Length == 3 && !unique.TryGetValue(7, out a))
                {
                    unique.Add(7, input);
                }
                if (input.Length == 7 && !unique.TryGetValue(8, out a))
                {
                    unique.Add(8, input);
                }
            }


            return unique;
        }

        private int DecodeOutput(Dictionary<int, string> unique, string[] outputs)
        {
            int result = 0;
            foreach (string output in outputs)
            {
                string a;
                if (output.Length == 2)
                {
                    result += 1;
                    if (!unique.TryGetValue(1, out a))
                    {
                        unique.Add(1, output);
                    }
                }
                if (output.Length == 4)
                {
                    result += 4;
                    if (!unique.TryGetValue(4, out a))
                    {
                        unique.Add(4, output);
                    }
                }
                if (output.Length == 3)
                {
                    result += 7;
                    if (!unique.TryGetValue(8, out a))
                    {
                        unique.Add(7, output);
                    }
                }
                if (output.Length == 7)
                {
                    result += 8;
                    if (!unique.TryGetValue(8, out a))
                    {
                        unique.Add(8, output);
                    }
                }
                if (output.Length == 6)
                {
                    HashSet<char> one = new HashSet<char>(unique[1]);
                    HashSet<char> num = new HashSet<char>(output);
                    HashSet<char> sub = new HashSet<char>(unique[4]);
                    sub.ExceptWith(one);
                    if (one.IsSubsetOf(num) && sub.IsSubsetOf(num))
                    {
                        result += 9;
                    }
                    else
                    {
                        if (sub.IsSubsetOf(num))
                        {
                            result += 6;
                        }
                    }
                }
                if (output.Length == 5)
                {
                    HashSet<char> one = new HashSet<char>(unique[1]);
                    HashSet<char> num = new HashSet<char>(output);
                    HashSet<char> sub = new HashSet<char>(unique[4]);
                    sub.ExceptWith(one);
                    if (one.IsSubsetOf(num))
                    {
                        result += 3;
                    }
                    else
                    {
                        if (sub.IsSubsetOf(num))
                        {
                            result += 5;
                        }
                        else
                        {
                            result += 2;
                        }
                    }
                }
                result *= 10;
            }
            return result / 10;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SevenSegmentSearch part01 = new SevenSegmentSearch("input01.txt");
            Console.WriteLine(part01.Part01());
            SevenSegmentSearch part02 = new SevenSegmentSearch("input02.txt");
            Console.WriteLine(part02.Part02());
        }
    }
}
