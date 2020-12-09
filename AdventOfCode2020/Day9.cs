using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day9
    {
        private const int PREAMBLE_LENGTH = 25;
        private const int WINDOW_SIZE = 25;

        public static void Part1()
        {
            List<UInt64> data = new List<UInt64>();
            int windowStart = 0;
            using (var sr = new StreamReader("Day9_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(Convert.ToUInt64(line));
                }
            }

            for (int i = PREAMBLE_LENGTH; i < data.Count; i++)
            {
                bool gotoNext = false;
                for (int j = windowStart; j < windowStart + WINDOW_SIZE; j++)
                {
                    for (int k = windowStart + 1; k < windowStart + WINDOW_SIZE; k++)
                    {
                        if (data[j] + data[k] == data[i])
                        {
                            gotoNext = true;
                            break;
                        }
                    }
                    if (gotoNext) break;
                }
                if (!gotoNext)
                {
                    Console.WriteLine(data[i]);
                    break;
                }
                windowStart++;
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            List<UInt64> data = new List<UInt64>();
            int windowStart = 0;
            using (var sr = new StreamReader("Day9_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(Convert.ToUInt64(line));
                }
            }

            UInt64 foundNumber = 0;
            for (int i = PREAMBLE_LENGTH; i < data.Count; i++)
            {
                bool gotoNext = false;
                for (int j = windowStart; j < windowStart + WINDOW_SIZE; j++)
                {
                    for (int k = windowStart + 1; k < windowStart + WINDOW_SIZE; k++)
                    {
                        if (data[j] + data[k] == data[i])
                        {
                            gotoNext = true;
                            break;
                        }
                    }
                    if (gotoNext) break;
                }
                if (!gotoNext)
                {
                    foundNumber = data[i];
                    Console.WriteLine(foundNumber);
                    break;
                }
                windowStart++;
            }

            bool breakout = false;
            for (int i = 0; i < data.Count; i++)
            {
                UInt64 smallest = UInt64.MaxValue;
                UInt64 largest = UInt64.MinValue;
                for (int j = 1; j < data.Count; j++)
                {
                    UInt64 sum = 0;
                    for (int k = i; k <= j; k++)
                    {
                        sum += data[k];
                        if (data[k] > largest) largest = data[k];
                        if (data[k] < smallest) smallest = data[k];
                    }
                    if (sum == foundNumber)
                    {
                        Console.WriteLine("{0} {1} {2}", smallest, largest, smallest + largest);
                        breakout = true;
                        break;
                    }
                }
                if (breakout) break;
            }
            Console.ReadKey();
        }
    }
}
