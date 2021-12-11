using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace day10
{
    class SyntaxScoring
    {

        private string[] text;

        public SyntaxScoring(string path)
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
                char bracket = FindBadBrackets(line);
                if (bracket == ')')
                {
                    result += 3;
                }
                if (bracket == '}')
                {
                    result += 1197;
                }
                if (bracket == ']')
                {
                    result += 57;
                }
                if (bracket == '>')
                {
                    result += 25137;
                }
            }
            return result;
        }

        public ulong Part02()
        {
            List<ulong> scores = new List<ulong>();
            foreach (string line in text)
            {
                char[] brackets = FindMissingBrackets(line);
                ulong score = 0;
                for (int i = brackets.Length - 1; i > -1; i--)
                {
                    score *= 5;
                    if (brackets[i] == ')')
                    {
                        score += 1;
                    }
                    if (brackets[i] == ']')
                    {
                        score += 2;
                    }
                    if (brackets[i] == '}')
                    {
                        score += 3;
                    }
                    if (brackets[i] == '>')
                    {
                        score += 4;
                    }
                }
                if (score != 0)
                {
                    AddToSorted(scores, score);
                }
            }
            return scores[scores.Count / 2];
        }

        private void AddToSorted(List<ulong> list, ulong newItem)
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

        private char[] FindMissingBrackets(string line)
        {
            char[] startBrackets = new[] { '{', '[', '<', '(' };
            Dictionary<char, char> dict = new Dictionary<char, char>();
            dict['{'] = '}';
            dict['['] = ']';
            dict['<'] = '>';
            dict['('] = ')';
            List<char> ends = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                if (startBrackets.Contains(line[i]))
                {
                    ends.Add(dict[line[i]]);
                }
                else
                {
                    if (line[i] == ends[ends.Count - 1])
                    {
                        ends.RemoveAt(ends.Count - 1);
                    }
                    else
                    {
                        return Array.Empty<char>();
                    }
                }
            }
            return ends.ToArray();
        }

        private char FindBadBrackets(string line)
        {
            char[] startBrackets = new[] { '{', '[', '<', '(' };
            Dictionary<char, char> dict = new Dictionary<char, char>();
            dict['{'] = '}';
            dict['['] = ']';
            dict['<'] = '>';
            dict['('] = ')';
            List<char> ends = new List<char>();
            for (int i = 0; i < line.Length; i++)
            {
                if (startBrackets.Contains(line[i]))
                {
                    ends.Add(dict[line[i]]);
                }
                else
                {
                    if (line[i] == ends[ends.Count - 1])
                    {
                        ends.RemoveAt(ends.Count - 1);
                    }
                    else
                    {
                        return line[i];
                    }
                }
            }
            return ' ';
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            SyntaxScoring part01 = new SyntaxScoring("input01.txt");
            Console.WriteLine(part01.Part01());
            SyntaxScoring part02 = new SyntaxScoring("input02.txt");
            Console.WriteLine(part02.Part02());
        }
    }
}
