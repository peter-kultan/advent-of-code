using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day07
{
    class Whale
    {
        private string[] text;

        public Whale(string path)
        {
            text = LoadFile(path);
        }

        private string[] LoadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public int Part01()
        {
            List<int> orderedList = new List<int>(OrderList(text[0]));
            int median = FindMedian(orderedList);
            int result = 0;
            foreach (int item in orderedList)
            {
                if (item <= median)
                {
                    result += median - item;
                }
                else
                {
                    result += item - median;
                }
            }
            return result;
        }

        public int Part02()
        {
            List<int> list = new List<int>();
            foreach (string num in text[0].Split(","))
            {
                list.Add(Convert.ToInt32(num));
            }
            int average = (int)list.Sum() / list.Count();
            int result = 0;
            foreach (int item in list)
            {
                result += Enumerable.Range(0, Math.Max(average, item) - Math.Min(average, item) + 1).Sum();
            }
            return result;
        }

        private List<int> OrderList(string line)
        {
            List<int> sortedList = new List<int>();
            List<string> list = new List<string>(line.Split(","));
            foreach (string item in list)
            {
                InsertToSorted(sortedList, Convert.ToInt32(item));
            }
            return sortedList;
        }

        private void InsertToSorted(List<int> list, int newItem)
        {
            if (list.Count == 0)
            {
                list.Add(newItem);
                return;
            }
            if (list[list.Count - 1].CompareTo(newItem) <= 0)
            {
                list.Add(newItem);
                return;
            }
            if (list[0].CompareTo(newItem) >= 0)
            {
                list.Insert(0, newItem);
                return;
            }
            int index = list.BinarySearch(newItem);
            if (index < 0)
                index = ~index;
            list.Insert(index, newItem);
        }

        private int FindMedian(List<int> list)
        {
            if (list.Count() % 2 == 1)
            {
                return (list[list.Count / 2] + list[(list.Count / 2) + 1]) / 2;
            }
            return list[list.Count / 2];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Whale part01 = new Whale("input01.txt");
            Console.WriteLine(part01.Part01());
            Whale part02 = new Whale("input02.txt");
            Console.WriteLine(part02.Part02());
        }
    }
}
