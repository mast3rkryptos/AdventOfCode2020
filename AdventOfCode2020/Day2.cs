using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day2
    {
        public static void Part1()
        {
            using (var sr = new StreamReader("Day2_Input.txt"))
            {
                int countValid = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int countLetter = 0;
                    string[] subline = line.Split("-");
                    int min = Convert.ToInt32(subline[0]);
                    subline = subline[1].Split(" ");
                    int max = Convert.ToInt32(subline[0]);
                    char letter = Convert.ToChar(subline[1][0]);
                    string password = subline[2];
                    foreach (char c in password)
                    {
                        if (c == letter) countLetter++;
                    }
                    if (countLetter >= min && countLetter <= max) countValid++;
                }
                Console.WriteLine(countValid);
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            using (var sr = new StreamReader("Day2_Input.txt"))
            {
                int countValid = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int countLetter = 0;
                    string[] subline = line.Split("-");
                    int first = Convert.ToInt32(subline[0]);
                    subline = subline[1].Split(" ");
                    int second = Convert.ToInt32(subline[0]);
                    char letter = Convert.ToChar(subline[1][0]);
                    string password = subline[2];
                    if (password[first - 1] == letter) countLetter++;
                    if (password[second - 1] == letter) countLetter++;
                    if (countLetter == 1) countValid++;
                }
                Console.WriteLine(countValid);
            }
            Console.ReadKey();
        }
    }
}
