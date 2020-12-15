using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day15
    {
        private static Dictionary<int, int> numbers;
        public static void Part1()
        {
            numbers = new Dictionary<int, int>();
            int t = 1, lastnum = 0; ;
            using (var sr = new StreamReader("Day15_Input.txt"))
            {
                string[] numbersInput = sr.ReadLine().Split(",");

                for (int i = 0; i < numbersInput.Length - 1; i++)
                {
                    numbers[Convert.ToInt32(numbersInput[i])] = t++;
                }
                lastnum = Convert.ToInt32(numbersInput[numbersInput.Length - 1]);
            }

            while (t < 2020)
            {
                Console.WriteLine("{0} {1}", t, lastnum);
                if (!numbers.ContainsKey(lastnum))
                {
                    numbers[lastnum] = t++;
                    lastnum = 0;
                }
                else
                {
                    int tempTime = t - numbers[lastnum];
                    numbers[lastnum] = t++;
                    lastnum = tempTime;
                }
            }
            Console.WriteLine(lastnum);
            Console.ReadKey();
        }

        public static void Part2()
        {
            numbers = new Dictionary<int, int>();
            int t = 1, lastnum = 0; ;
            using (var sr = new StreamReader("Day15_Input.txt"))
            {
                string[] numbersInput = sr.ReadLine().Split(",");

                for (int i = 0; i < numbersInput.Length - 1; i++)
                {
                    numbers[Convert.ToInt32(numbersInput[i])] = t++;
                }
                lastnum = Convert.ToInt32(numbersInput[numbersInput.Length - 1]);
            }

            while (t < 30000000)
            {
                //Console.WriteLine("{0} {1}", t, lastnum);
                if (!numbers.ContainsKey(lastnum))
                {
                    numbers[lastnum] = t++;
                    lastnum = 0;
                }
                else
                {
                    int tempTime = t - numbers[lastnum];
                    numbers[lastnum] = t++;
                    lastnum = tempTime;
                }
            }
            Console.WriteLine(lastnum);
            Console.ReadKey();
        }
    }
}
