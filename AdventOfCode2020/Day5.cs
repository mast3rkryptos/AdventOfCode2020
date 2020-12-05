using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day5
    {
        public static void Part1()
        {
            UInt64 highest = 0;
            using (var sr = new StreamReader("Day5_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    UInt64 row = Convert.ToUInt64(line.Substring(0, 7).Replace("F", "0").Replace("B", "1"), 2);
                    UInt64 column = Convert.ToUInt64(line.Substring(7, 3).Replace("L", "0").Replace("R", "1"), 2);
                    UInt64 seatId = row * 8 + column;
                    Console.WriteLine("{0} {1} {2}", row, column, seatId);
                    if (seatId > highest) highest = seatId;
                }
            }
            Console.WriteLine(highest);
            Console.ReadKey();
        }

        public static void Part2()
        {
            List<UInt64> seatIds = new List<UInt64>();
            using (var sr = new StreamReader("Day5_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    UInt64 row = Convert.ToUInt64(line.Substring(0, 7).Replace("F", "0").Replace("B", "1"), 2);
                    UInt64 column = Convert.ToUInt64(line.Substring(7, 3).Replace("L", "0").Replace("R", "1"), 2);
                    UInt64 seatId = row * 8 + column;
                    seatIds.Add(seatId);
                    Console.WriteLine("{0} {1} {2}", row, column, seatId);
                }

                seatIds.Sort();
                for (int i = 0; i < seatIds.Count-1; i++)
                {
                    if (seatIds[i] + 1 != seatIds[i + 1]) Console.WriteLine(seatIds[i] + 1);
                }
            }
            Console.ReadKey();
        }
    }
}
