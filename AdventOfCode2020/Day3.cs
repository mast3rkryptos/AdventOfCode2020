using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day3
    {
        public static void Part1()
        {
            using (var sr = new StreamReader("Day3_Input.txt"))
            {
                List<string> map = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    map.Add(line);
                }

                int x = 0, y = 0, countTree = 0;
                while (y < map.Count - 1)
                {
                    x = x + 3;
                    y = y + 1;
                    if (x >= map[0].Length) x = x % map[0].Length;
                    if (map[y][x] == '#') countTree++;
                    Console.WriteLine("{0} {1} {2}", y, x, countTree);
                }
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            using (var sr = new StreamReader("Day3_Input.txt"))
            {
                List<string> map = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    map.Add(line);
                }

                UInt64 result = 1;
                int[] xslope = { 1, 3, 5, 7, 1 }, yslope = { 1, 1, 1, 1, 2 };
                for (int i = 0; i < xslope.Length; i++)
                {
                    int x = 0, y = 0;
                    uint countTree = 0;
                    while (y < map.Count - yslope[i])
                    {
                        x = x + xslope[i];
                        y = y + yslope[i];
                        if (x >= map[0].Length) x = x % map[0].Length;
                        if (map[y][x] == '#') countTree++;
                        Console.WriteLine("{0} {1} {2} {3}", y, x, map[y][x], countTree);
                    }
                    result = result * countTree;
                    Console.WriteLine("{0} {1}", countTree, result);
                }
            }
            Console.ReadKey();
        }
    }
}
