using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day13
    {
        public static void Part1()
        {
            using (var sr = new StreamReader("Day13_Input.txt"))
            {
                Int64 arrivalTime = Convert.ToInt64(sr.ReadLine());
                string[] busIds = sr.ReadLine().Split(",");
                Dictionary<int, Int64> busSchedule = new Dictionary<int, Int64>();
                foreach (string busId in busIds)
                {
                    if (busId == "x") continue;
                    else busSchedule.Add(Convert.ToInt32(busId), 0);
                }

                List<int> tempKeys = new List<int>();
                foreach (int key in busSchedule.Keys) tempKeys.Add(key);

                foreach (int key in tempKeys)
                {
                    while (busSchedule[key] < arrivalTime) busSchedule[key] += key;
                }

                int lowestKey = int.MaxValue;
                Int64 lowestValue = Int64.MaxValue;
                foreach (int key in busSchedule.Keys)
                {
                    if (busSchedule[key] < lowestValue)
                    {
                        lowestKey = key;
                        lowestValue = busSchedule[key];
                    }
                }

                Console.WriteLine((lowestValue - arrivalTime) * lowestKey);
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            using (var sr = new StreamReader("Day13_Input.txt"))
            {
                sr.ReadLine();
                string[] inputBusIds = sr.ReadLine().Split(",");
                List<UInt64> busIds = new List<UInt64>();
                List<UInt64> busOffsets = new List<UInt64>();
                UInt64 offsetCount = 0, maxValue = UInt64.MinValue, maxValueOffset = 0;
                foreach (string busId in inputBusIds)
                {
                    if (busId == "x") offsetCount++;
                    else
                    {
                        if (Convert.ToUInt64(busId) > maxValue)
                        {
                            maxValue = Convert.ToUInt64(busId);
                        }
                        busIds.Add(Convert.ToUInt64(busId));
                        busOffsets.Add(offsetCount);
                        offsetCount++;
                    }
                }
                //busOffsets.RemoveAt(0);

                int i = 1;
                UInt64 t, incValue = busIds[0];
                for (t = busIds[0]; t < UInt64.MaxValue; t += incValue)
                {
                    if ((t + busOffsets[i]) % busIds[i] == 0)
                    {
                        Console.WriteLine("Found bus #{0}", busIds[i]);
                        if (i + 1 == busIds.Count) break;
                        incValue *= busIds[i++];
                    }
                }

                Console.WriteLine(t);
            }
            Console.ReadKey();
        }
    }
}
