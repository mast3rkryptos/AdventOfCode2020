using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day16
    {
        public static void Part1()
        {               
            List<int> globalValidValues = new List<int>(); 
            string line;
            bool ignoreYourTicket = true;
            int scanningErrorRate = 0;
            using (var sr = new StreamReader("Day16_Input.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (line == "" || line == "your ticket:" || line == "nearby tickets:")
                    {
                        globalValidValues.Sort();
                    }
                    else if (splitLine.Length > 1)
                    {
                        if (ignoreYourTicket) ignoreYourTicket = false;
                        else
                        {
                            foreach(string s in splitLine)
                            {
                                int value = Convert.ToInt32(s);
                                if (!globalValidValues.Contains(value)) scanningErrorRate += value;
                            }
                        }
                    }
                    else
                    {
                        splitLine = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        int lowerBound = Convert.ToInt32(splitLine[0].Split('-')[0]);
                        int upperBound = Convert.ToInt32(splitLine[0].Split('-')[1]);
                        for (int i = lowerBound; i <= upperBound; i++)
                        {
                            if (!globalValidValues.Contains(i)) globalValidValues.Add(i);
                        }
                        lowerBound = Convert.ToInt32(splitLine[2].Split('-')[0]);
                        upperBound = Convert.ToInt32(splitLine[2].Split('-')[1]);
                        for (int i = lowerBound; i <= upperBound; i++)
                        {
                            if (!globalValidValues.Contains(i)) globalValidValues.Add(i);
                        }
                    }
                }
            }

            Console.WriteLine(scanningErrorRate);
            Console.ReadKey();
        }

        public static void Part2()
        {
            List<int> globalValidValues = new List<int>();
            Dictionary<string, List<int>> validValues = new Dictionary<string, List<int>>();
            Dictionary<string, List<int>> fieldValidity = new Dictionary<string, List<int>>();
            string line;
            string[] yourTicket = null;
            using (var sr = new StreamReader("Day16_Input.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (line == "" || line == "your ticket:" || line == "nearby tickets:")
                    {
                        globalValidValues.Sort();
                    }
                    else if (splitLine.Length > 1)
                    {
                        if (yourTicket == null)
                        {
                            foreach (string s in fieldValidity.Keys)
                            {
                                for (int j = 0; j < splitLine.Length; j++)
                                {
                                    fieldValidity[s].Add(j);
                                }
                            }
                            yourTicket = new string[splitLine.Length];
                            for (int i = 0; i < splitLine.Length; i++) yourTicket[i] = splitLine[i];
                        }
                        else
                        {
                            for (int i = 0; i < splitLine.Length; i++)
                            {
                                int value = Convert.ToInt32(splitLine[i]);
                                if (!globalValidValues.Contains(value)) break;
                                foreach (string s in validValues.Keys)
                                {
                                    if (!validValues[s].Contains(value)) fieldValidity[s].Remove(i);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        string fieldName = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[0];
                        validValues[fieldName] = new List<int>();
                        fieldValidity[fieldName] = new List<int>();
                        splitLine = line.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        int lowerBound = Convert.ToInt32(splitLine[0].Split('-')[0]);
                        int upperBound = Convert.ToInt32(splitLine[0].Split('-')[1]);
                        for (int i = lowerBound; i <= upperBound; i++)
                        {
                            if (!globalValidValues.Contains(i)) globalValidValues.Add(i);
                            if (!validValues[fieldName].Contains(i)) validValues[fieldName].Add(i);
                        }
                        lowerBound = Convert.ToInt32(splitLine[2].Split('-')[0]);
                        upperBound = Convert.ToInt32(splitLine[2].Split('-')[1]);
                        for (int i = lowerBound; i <= upperBound; i++)
                        {
                            if (!globalValidValues.Contains(i)) globalValidValues.Add(i);
                            if (!validValues[fieldName].Contains(i)) validValues[fieldName].Add(i);
                        }
                        validValues[fieldName].Sort();
                    }
                }
            }

            int count = 0;
            foreach (string s in fieldValidity.Keys)
            {
                count += fieldValidity[s].Count;
            }
            while (count != fieldValidity.Count)
            {
                foreach (string s0 in fieldValidity.Keys)
                {
                    if (fieldValidity[s0].Count == 1)
                    {
                        foreach (string s1 in fieldValidity.Keys)
                        {
                            if (s0 != s1) fieldValidity[s1].Remove(fieldValidity[s0][0]);
                        }
                    }
                }


                count = 0;
                foreach (string s in fieldValidity.Keys)
                {
                    count += fieldValidity[s].Count;
                }
            }

            foreach (string s in fieldValidity.Keys) Console.WriteLine("{0} {1}", s, fieldValidity[s][0]);

            UInt64 product = 1;
            foreach (string s in fieldValidity.Keys)
            {
                if (s.StartsWith("departure")) product *= Convert.ToUInt64(yourTicket[fieldValidity[s][0]]);
            }
            Console.WriteLine(product);
            Console.ReadKey();
        }
    }
}
