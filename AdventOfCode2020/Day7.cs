using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day7
    {
        public static void Part1()
        {
            List<string> colors = new List<string>();
            List<int[]> rules = new List<int[]>();
            using (var sr = new StreamReader("Day7_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(" ");
                    string outerColor = splitLine[0] + " " + splitLine[1];
                    if (!colors.Contains(outerColor) && outerColor != "other bags.") colors.Add(outerColor);

                    int[] tempRule = new int[((splitLine.Length - 4) / 4) + 1];
                    tempRule[0] = colors.IndexOf(outerColor);
                    for (int i = 4; i < splitLine.Length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            // Ignore this for now, it's the number of inner bags
                        }
                        if (i % 4 == 1)
                        {
                            string innerColor = splitLine[i] + " " + splitLine[i + 1];
                            if (!colors.Contains(innerColor) && innerColor != "other bags.") colors.Add(innerColor);
                            if (innerColor != "other bags.") tempRule[((i - 5) / 4) + 1] = colors.IndexOf(innerColor);
                        }
                        if (i % 4 == 2)
                        {
                            // Do nothing, already handled
                        }
                        if (i % 4 == 3)
                        {
                            // Do nothing, this is either "bag" or "bags"
                        }
                    }
                    rules.Add(tempRule);
                }

                // Find the direct golden bag-containing rules
                bool[] tracker = new bool[colors.Count];
                int goldenColor = colors.IndexOf("shiny gold");
                foreach (int[] rule in rules)
                {
                    for (int i = 1; i < rule.Length; i++)
                    {
                        if (rule[i] == goldenColor) tracker[rule[0]] = true;
                    }
                }

                // While tracking which colors have contained golden bags, move up the chain of bags. Exit when the tracker has not changed this iteration
                bool keepGoing = true;
                while (keepGoing) 
                { 
                    bool[] tempTracker = new bool[colors.Count];
                    tracker.CopyTo(tempTracker, 0);

                    for (int i = 0; i < tracker.Length; i++)
                    {
                        if (tracker[i])
                        {
                            foreach (int[] rule in rules)
                            {
                                for (int j = 1; j < rule.Length; j++)
                                {
                                    if (rule[j] == i) tempTracker[rule[0]] = true;
                                }
                            }
                        }
                    }

                    keepGoing = false;
                    for (int i = 0; i < tracker.Length; i++)
                    {
                        keepGoing |= tracker[i] ^ tempTracker[i];
                        tracker[i] = tempTracker[i];
                    }
                }

                UInt64 count = 0;
                for (int i = 0; i < tracker.Length; i++)
                {
                    if (tracker[i])
                    {
                        Console.WriteLine(colors[i]);
                        count++;
                    }
                }
                Console.WriteLine(count);
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
