using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day14
    {
        private static Dictionary<UInt64, UInt64> memory;
        public static void Part1()
        {
            UInt64 andMask = 0x0000000000000000, orMask = 0x0000000000000000;
            memory = new Dictionary<UInt64, ulong>();
            using (var sr = new StreamReader("Day14_Input.txt"))
            {
                string line;
                string[] splitLine;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("mask"))
                    {
                        splitLine = line.Split(" ");
                        andMask = Convert.ToUInt64(splitLine[splitLine.Length - 1].Replace("X", "1"), 2);
                        orMask = Convert.ToUInt64(splitLine[splitLine.Length - 1].Replace("X", "0"), 2);
                    }
                    else if (line.StartsWith("mem"))
                    {
                        UInt64 address = Convert.ToUInt64(line.Split("[")[1].Split("]")[0]);
                        splitLine = line.Split(" ");
                        UInt64 value = Convert.ToUInt64(splitLine[splitLine.Length - 1]);

                        memory[address] = (value & andMask ) | orMask;
                    }
                }
            }

            UInt64 sum = 0;
            foreach (UInt64 value in memory.Values) sum += value;
            Console.WriteLine(sum);

            Console.ReadKey();
        }

        public static void Go(string modAddr, UInt64 value)
        {
            char[] addr0 = modAddr.ToCharArray();
            char[] addr1 = modAddr.ToCharArray();
            int index = modAddr.IndexOf("X");
            if (index == -1) memory[Convert.ToUInt64(modAddr, 2)] = value;
            else
            {
                addr0[index] = '0';
                Go(new string(addr0), value);
                addr1[index] = '1';
                Go(new string(addr1), value);
            }
        }

        public static void Part2()
        {
            string mask = new string("");
            memory = new Dictionary<UInt64, ulong>();
            using (var sr = new StreamReader("Day14_Input.txt"))
            {
                string line;
                string[] splitLine;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("mask"))
                    {
                        splitLine = line.Split(" ");
                        mask = splitLine[splitLine.Length - 1];
                    }
                    else if (line.StartsWith("mem"))
                    {
                        UInt64 address = Convert.ToUInt64(line.Split("[")[1].Split("]")[0]);
                        string addressString = Convert.ToString((long)address, 2);
                        addressString = new string('0', 36 - addressString.Length) + addressString;
                        string addressStringMasked = new string("");
                        for (int i = 0; i < mask.Length; i++)
                        {
                            if (mask[i] == '0') addressStringMasked += addressString[i];
                            else if (mask[i] == '1') addressStringMasked += '1';
                            else addressStringMasked += 'X';
                        }

                        splitLine = line.Split(" ");
                        UInt64 value = Convert.ToUInt64(splitLine[splitLine.Length - 1]);

                        Go(addressStringMasked, value);
                    }
                }
            }

            UInt64 sum = 0;
            foreach (UInt64 value in memory.Values) sum += value;
            Console.WriteLine(sum);

            Console.ReadKey();
        }
    }
}
