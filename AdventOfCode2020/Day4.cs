using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day4
    {
        public class Passport
        {
            bool byr = false;
            bool iyr = false;
            bool eyr = false;
            bool hgt = false;
            bool hcl = false;
            bool ecl = false;
            bool pid = false;
            bool cid = false;

            /*
            Int64 byr = -1;
            Int64 iyr = -1;
            Int64 eyr = -1;
            String hgt = null;
            Int64 hcl = -1;
            String ecl = null;
            Int64 pid = -1;
            Int64 cid = -1;
            */

            public Passport(string input)
            {
                try 
                {
                    String[] splitInput = input.Split(new string[] { " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string pair in splitInput)
                    {
                        String[] splitPair = pair.Split(":");
                        switch (splitPair[0])
                        {
                            case "byr":
                                byr = (Convert.ToInt64(splitPair[1]) >= 1920 && Convert.ToInt64(splitPair[1]) <= 2002);
                                break;
                            case "iyr":
                                iyr = (Convert.ToInt64(splitPair[1]) >= 2010 && Convert.ToInt64(splitPair[1]) <= 2020);
                                break;
                            case "eyr":
                                eyr = (Convert.ToInt64(splitPair[1]) >= 2020 && Convert.ToInt64(splitPair[1]) <= 2030);
                                break;
                            case "hgt":
                                if (splitPair[1].Length == 4)
                                {
                                    hgt = (Convert.ToInt64(splitPair[1].Substring(0, 2)) >= 59 && Convert.ToInt64(splitPair[1].Substring(0, 2)) <= 76);
                                    hgt &= splitPair[1][2] == 'i';
                                    hgt &= splitPair[1][3] == 'n';
                                }
                                else if (splitPair[1].Length == 5)
                                {
                                    hgt = (Convert.ToInt64(splitPair[1].Substring(0, 3)) >= 150 && Convert.ToInt64(splitPair[1].Substring(0, 3)) <= 193);
                                    hgt &= splitPair[1][3] == 'c';
                                    hgt &= splitPair[1][4] == 'm';
                                }
                                break;
                            case "hcl":
                                hcl = splitPair[1][0] == '#';
                                for (int i = 1; i < splitPair[1].Length; i++)
                                {
                                    hcl &= char.IsDigit(splitPair[1][i]) || splitPair[1][i] == 'a' || splitPair[1][i] == 'b' || splitPair[1][i] == 'c' || splitPair[1][i] == 'd' || splitPair[1][i] == 'e' || splitPair[1][i] == 'f';
                                }
                                break;
                            case "ecl":
                                String[] allowed = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                                foreach (String s in allowed)
                                {
                                    if (s == splitPair[1])
                                    {
                                        ecl = true;
                                        break;
                                    }
                                }
                                break;
                            case "pid":
                                pid = splitPair[1].Length == 9;
                                foreach (char c in splitPair[1])
                                {
                                    if (!char.IsDigit(c)) pid &= false;
                                }
                                break;
                            case "cid":
                                cid = true;
                                break;
                        }
                    }
                } catch (Exception e)
                {
                    byr = false;
                    iyr = false;
                    eyr = false;
                    hgt = false;
                    hcl = false;
                    ecl = false;
                    pid = false;
                    cid = false;
                }
            }

            public bool Is_Valid()
            {
                return (byr && iyr && eyr && hgt && hcl && ecl && pid);
            }
        }

        public static void Part1()
        {
            using (var sr = new StreamReader("Day4_Input.txt"))
            {
                UInt64 count = 0;
                Passport p = null;
                string line, combined = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        p = new Passport(combined);
                        if (p.Is_Valid()) count++;
                        combined = "";
                    }
                    else
                    {
                        combined += line + "\n";
                    }
                }
                p = new Passport(combined);
                if (p.Is_Valid()) count++;
                Console.WriteLine(count);
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            using (var sr = new StreamReader("Day4_Input.txt"))
            {
                UInt64 count = 0;
                Passport p = null;
                string line, combined = "";
                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "")
                    {
                        Console.WriteLine("Start:\n" + combined);
                        p = new Passport(combined);
                        if (p.Is_Valid()) count++;
                        combined = "";
                    }
                    else
                    {
                        combined += line + "\n";
                    }
                }
                Console.WriteLine("Start:\n" + combined);
                p = new Passport(combined);
                if (p.Is_Valid()) count++;
                Console.WriteLine(count);
            }
            Console.ReadKey();
        }
    }
}
