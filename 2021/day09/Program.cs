using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day09
{
    class SmokeBasis
    {
        private string[] text;

        public SmokeBasis(string path)
        {
            text = LoadFile(path);
        }

        private string[] LoadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public int Part01()
        {
            List<List<int>> map = new List<List<int>>();
            foreach (string line in text)
            {
                map.Add(SplitLine(line));
            }
            int result = 0;
            foreach (Tuple<int, int> coords in FindLowest(map))
            {
                result += 1 + map[coords.Item1][coords.Item2];
            }
            return result;
        }

        public int Part02()
        {
            List<List<int>> map = new List<List<int>>();
            foreach (string line in text)
            {
                map.Add(SplitLine(line));
            }
            List<int> basins = new List<int>();
            foreach (Tuple<int, int> coords in FindLowest(map))
            {
                basins.Add(FindBasins(map, coords));
            }
            Tuple<int, int, int> max = new Tuple<int, int, int>(0, 0, 0);
            foreach (int basin in basins)
            {
                if (basin > max.Item1)
                {
                    max = new Tuple<int, int, int>(basin, max.Item1, max.Item2);
                    continue;
                }
                if (basin > max.Item2)
                {
                    max = new Tuple<int, int, int>(max.Item1, basin, max.Item2);
                }
                if (basin > max.Item3)
                {
                    max = new Tuple<int, int, int>(max.Item1, max.Item2, basin);
                }
            }
            return max.Item1 * max.Item2 * max.Item3;
        }

        private List<int> SplitLine(string line)
        {
            List<int> mapLine = new List<int>();
            foreach (char num in line)
            {
                mapLine.Add(num - '0');
            }
            return mapLine;
        }

        private List<Tuple<int, int>> FindLowest(List<List<int>> map)
        {
            List<Tuple<int, int>> lowest = new List<Tuple<int, int>>();
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    bool[] lower = new[] { false, false, false, false }; //up, down, left, right
                    int item = map[y][x];
                    lower[0] = y == 0 || map[y - 1][x] > item;
                    lower[1] = y == map.Count - 1 || map[y + 1][x] > item;
                    lower[2] = x == 0 || map[y][x - 1] > item;
                    lower[3] = x == map[0].Count - 1 || map[y][x + 1] > item;
                    if (lower.All(x => x))
                    {
                        lowest.Add(new Tuple<int, int>(y, x));
                    }
                }
            }
            return lowest;
        }

        private int FindBasins(List<List<int>> map, Tuple<int, int> coords)
        {
            HashSet<Tuple<int, int>> basins = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> newBasins = basins;
            basins.Add(coords);
            do
            {
                basins = new HashSet<Tuple<int, int>>(newBasins);
                newBasins = new HashSet<Tuple<int, int>>();
                foreach (Tuple<int, int> coord in basins)
                {
                    newBasins.Add(coord);
                    newBasins = newBasins.Union(CheckSurroundings(map, coord)).ToHashSet();
                }
            } while (!basins.SetEquals(newBasins));
            return basins.Count;
        }

        private HashSet<Tuple<int, int>> CheckSurroundings(List<List<int>> map, Tuple<int, int> coords)
        {
            HashSet<Tuple<int, int>> surroundings = new HashSet<Tuple<int, int>>();
            if (coords.Item1 > 0)
            {
                if (map[coords.Item1 - 1][coords.Item2] != 9)
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 - 1, coords.Item2));
                }
                if (coords.Item2 > 0 && map[coords.Item1 - 1][coords.Item2 - 1] != 9 && (map[coords.Item1 - 1][coords.Item2] != 9 || map[coords.Item1][coords.Item2 - 1] != 9))
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 - 1, coords.Item2 - 1));
                }
                if (coords.Item2 < map[0].Count - 1 && map[coords.Item1 - 1][coords.Item2 + 1] != 9 && (map[coords.Item1 - 1][coords.Item2] != 9 || map[coords.Item1][coords.Item2 + 1] != 9))
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 - 1, coords.Item2 + 1));
                }
            }

            if (coords.Item1 < map.Count - 1)
            {
                if (map[coords.Item1 + 1][coords.Item2] != 9)
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 + 1, coords.Item2));
                }
                if (coords.Item2 > 0 && map[coords.Item1 + 1][coords.Item2 - 1] != 9 && (map[coords.Item1 + 1][coords.Item2] != 9 || map[coords.Item1][coords.Item2 - 1] != 9))
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 + 1, coords.Item2 - 1));
                }
                if (coords.Item2 < map[0].Count - 1 && map[coords.Item1 + 1][coords.Item2 + 1] != 9 && (map[coords.Item1 + 1][coords.Item2] != 9 || map[coords.Item1][coords.Item2 + 1] != 9))
                {
                    surroundings.Add(new Tuple<int, int>(coords.Item1 + 1, coords.Item2 + 1));
                }
            }
            if (coords.Item2 > 0 && map[coords.Item1][coords.Item2 - 1] != 9)
            {
                surroundings.Add(new Tuple<int, int>(coords.Item1, coords.Item2 - 1));
            }
            if (coords.Item2 < map[0].Count - 1 && map[coords.Item1][coords.Item2 + 1] != 9)
            {
                surroundings.Add(new Tuple<int, int>(coords.Item1, coords.Item2 + 1));
            }
            return surroundings;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SmokeBasis part01 = new SmokeBasis("input01.txt");
            Console.WriteLine(part01.Part01());
            SmokeBasis part02 = new SmokeBasis("input02.txt");
            Console.WriteLine(part02.Part02());
        }
    }
}
