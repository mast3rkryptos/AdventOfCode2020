using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day6
    {
        public static void Part1()
        {
            bool[] answers = new bool[26];
            UInt64 count = 0, sum = 0;
            using (var sr = new StreamReader("Day6_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        foreach (bool b in answers) if (b) count++;
                        sum += count;
                        Console.WriteLine(count);
                        count = 0;
                        answers = new bool[26];
                    }
                    else
                    {
                        foreach (char c in line)
                        {
                            answers[c - 97] = true;
                        }
                    }
                }
                foreach (bool b in answers) if (b) count++;
                sum += count;
                Console.WriteLine(count);
                Console.WriteLine(sum);
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            bool[] answers = new bool[26];
            for (int i = 0; i < answers.Length; i++) answers[i] = true;
            List<bool[]> groupAnswers = new List<bool[]>();
            using (var sr = new StreamReader("Day6_Input.txt"))
            {
                UInt64 sum = 0;
                string line;
                bool[] tempAnswers = new bool[26];
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        foreach (bool[] ba in groupAnswers)
                        {
                            for (int i = 0; i < ba.Length; i++)
                            {
                                answers[i] &= ba[i];
                            }
                        }
                        foreach (bool b in answers) if (b) sum++;
                        groupAnswers = new List<bool[]>();
                        answers = new bool[26];
                        for (int i = 0; i < answers.Length; i++) answers[i] = true;
                    }
                    else
                    {
                        foreach (char c in line)
                        {
                            tempAnswers[c - 97] = true;
                        }
                        groupAnswers.Add(tempAnswers);
                        tempAnswers = new bool[26];
                    }
                }
                foreach (bool[] ba in groupAnswers)
                {
                    for (int i = 0; i < ba.Length; i++)
                    {
                        answers[i] &= ba[i];
                    }
                }
                foreach (bool b in answers) if (b) sum++;

                Console.WriteLine(sum);
            }
            Console.ReadKey();
        }
    }
}
