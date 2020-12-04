using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day1
    {
        public static void Part1()
        {
            List<UInt64> l = new List<UInt64>();
            using (var sr = new StreamReader("Day1_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    l.Add(Convert.ToUInt64(line));
                }
            }

            for (int i = 0; i < l.Count; i++)
            {
                for (int j = i; j < l.Count; j++)
                {
                    if (l[i] + l[j] == 2020)
                        Console.Out.WriteLine(l[i] + " " + l[j] + " " + (l[i] * l[j]));
                }
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            List<UInt64> l = new List<UInt64>();
            using (var sr = new StreamReader("Day1_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    l.Add(Convert.ToUInt64(line));
                }
            }

            for (int i = 0; i < l.Count; i++)
            {
                for (int j = i; j < l.Count; j++)
                {
                    for (int k = j; k < l.Count; k++)
                    {
                        if (l[i] + l[j] + l[k] == 2020)
                            Console.Out.WriteLine(l[i] + " " + l[j] + " " + l[k] + " " + (l[i] * l[j] * l[k]));

                    }
                }
            }
            Console.ReadKey();
        }
    }
}
