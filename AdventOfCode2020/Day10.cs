using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day10
    {

        public static void Part1()
        {
            List<UInt64> joltOutputs = new List<UInt64>();
            using (var sr = new StreamReader("Day10_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    joltOutputs.Add(Convert.ToUInt64(line));
                }

                int[] counts = new int[4];
                foreach (int i in counts) counts[i] = 0;
                
                UInt64 previousJolts = 0;
                joltOutputs.Sort();
                foreach (UInt64 u in joltOutputs)
                {
                    counts[u - previousJolts]++;
                    previousJolts = u;
                }
                counts[3]++;
                Console.WriteLine("{0} {1} {2}", counts[1], counts[3], counts[1] * counts[3]);
            }
            Console.ReadKey();
        }

        public static List<UInt64> joltOutputs = new List<UInt64>();
        public static void Part2()
        {
            using (var sr = new StreamReader("Day10_Input.txt"))
            {
                string line;
                joltOutputs.Add(0);
                while ((line = sr.ReadLine()) != null)
                {
                    joltOutputs.Add(Convert.ToUInt64(line));
                }
                joltOutputs.Sort();
                joltOutputs.Add(joltOutputs[joltOutputs.Count - 1] + 3);

                UInt64 sum = 0;
                for (int i = 1; i < 4; i++)
                {
                    sum += Go(0, i);
                }
                Console.WriteLine(sum);
            }
            Console.ReadKey();
        }

        private static UInt64 Go(int prevIndex, int currIndex)
        {
            if ((currIndex == (joltOutputs.Count - 1)) && (joltOutputs[currIndex] - joltOutputs[prevIndex] <= 3)) return 1;
            else if (joltOutputs[currIndex] - joltOutputs[prevIndex] > 3) return 0;
            else if (joltOutputs[currIndex] - joltOutputs[prevIndex] <= 3)
            {
                UInt64 sum = 0;
                for (int i = currIndex + 1; i <= (currIndex + 3) && i < joltOutputs.Count; i++)
                {
                    sum += Go(currIndex, i);
                }
                return sum;
            }
            return 0;
        }
    }
}
