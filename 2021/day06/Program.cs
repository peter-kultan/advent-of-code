using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace day06
{
    class Lanternfish
    {
        private string[] text;

        public Lanternfish(string path)
        {
            text = LoadFile(path);
        }

        private string[] LoadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public ulong Simulate(int days)
        {
            string[] splittedLine = text[0].Split(",");
            Dictionary<int, ulong> nums = new Dictionary<int, ulong>();
            //List<int> nums = new List<int>();
            foreach (string str in splittedLine)
            {
                int num = Convert.ToInt32(str);
                ulong a;
                if (nums.TryGetValue(num, out a))
                {
                    nums[num]++;
                }
                else
                {
                    nums[num] = 1;
                }
            }
            return SimulateRec(days, nums);
        }

        private ulong SimulateRec(int days, Dictionary<int, ulong> nums)
        {
            if (days == 0)
            {
                ulong result = 0;
                foreach (ulong x in nums.Values)
                {
                    result += (ulong)x;
                }
                return result;
            }
            Dictionary<int, ulong> new_dict = new Dictionary<int, ulong>();
            foreach (int key in nums.Keys)
            {
                if (key == 0)
                {
                    AddOrIncrement(new_dict, 8, nums[key]);
                    AddOrIncrement(new_dict, 6, nums[key]);
                }
                else
                {
                    AddOrIncrement(new_dict, key - 1, nums[key]);
                }
            }
            return SimulateRec(--days, new_dict);
        }

        private void AddOrIncrement(Dictionary<int, ulong> dict, int key, ulong newValue)
        {
            ulong a;
            if (dict.TryGetValue(key, out a))
            {
                dict[key] += newValue;
            }
            else
            {
                dict[key] = newValue;
            }
            //return dict;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lanternfish part01 = new Lanternfish("input01.txt");
            Console.WriteLine(part01.Simulate(80));
            Lanternfish part02 = new Lanternfish("input02.txt");
            Console.WriteLine(part02.Simulate(2048));
        }
    }
}
