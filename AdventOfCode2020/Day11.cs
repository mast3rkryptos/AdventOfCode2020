using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day11
    {

        private static void PrintState(List<char[]> seating)
        {
            foreach (char[] ca in seating)
            {
                foreach(char c in ca)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool CheckEmptySeat(int x, int y, List<char[]> seating)
        {
            bool retVal = true;
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= seating.Count || j >= seating[y].Length) retVal &= true; // Meaningless
                    else if (i == y && j == x) continue;
                    else retVal &= seating[i][j] == 'L' || seating[i][j] == '.';
                }
            }
            return retVal;
        }

        private static int CheckOccupiedSeat(int x, int y, List<char[]> seating)
        {
            int retVal = 0;
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= seating.Count || j >= seating[y].Length) retVal += 0; // Meaningless
                    else if (i == y && j == x) continue;
                    else retVal += (seating[i][j] == '#') ? 1 : 0;
                }
            }
            return retVal;
        }

        private static bool CompareSeating(List<char[]> seating1, List<char[]> seating2)
        {
            bool retVal = true;
            for (int y = 0; y < seating1.Count; y++)
            {
                for (int x = 0; x < seating1[y].Length; x++)
                {
                    retVal &= seating1[y][x] == seating2[y][x];
                }
            }
            return retVal;
        }

        public static void Part1()
        {
        
            List<char[]> seating = new List<char[]>();
            List<char[]> nextSeating = new List<char[]>();
            using (var sr = new StreamReader("Day11_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    seating.Add(line.ToCharArray());
                    nextSeating.Add(line.ToCharArray());
                }
            }

            //PrintState(seating);

            do
            {
                seating = nextSeating;
                nextSeating = new List<char[]>();
                for (int y = 0; y < seating.Count; y++)
                {
                    nextSeating.Add(new char[seating[y].Length]);
                    for (int x = 0; x < seating[y].Length; x++)
                    {
                        if (seating[y][x] == 'L' && CheckEmptySeat(x, y, seating)) nextSeating[y][x] = '#';
                        else if (seating[y][x] == '#' && CheckOccupiedSeat(x, y, seating) >= 4) nextSeating[y][x] = 'L';
                        else nextSeating[y][x] = seating[y][x];
                    }
                }

                //PrintState(nextSeating);
            } while (!CompareSeating(seating, nextSeating));

            UInt64 count = 0;
            for (int y = 0; y < seating.Count; y++)
            {
                for (int x = 0; x < seating[y].Length; x++)
                {
                    count += (seating[y][x] == '#' ? (UInt64) 1 : (UInt64) 0);
                }
            }
            Console.WriteLine(count);

             Console.ReadKey();
        }

        private static bool CheckEmptySeatP2(int x, int y, List<char[]> seating)
        {
            bool retVal = true;
            int i, j;

            // Check NW
            i = y - 1;
            for (j = x - 1; i >= 0 && j >= 0 && retVal; i--, j--)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal = false;
                }
            }

            // Check NE
            i = y - 1;
            for (j = x + 1; i >= 0 && j < seating[i].Length && retVal; i--, j++)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal = false;
                }
            }

            // Check SE
            i = y + 1;
            for (j = x + 1; i < seating.Count && j < seating[i].Length && retVal; i++, j++)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal = false;
                }
            }

            // Check SW
            i = y + 1;
            for (j = x - 1; i < seating.Count && j >= 0 && retVal; i++, j--)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal = false;
                }
            }

            // Check N
            for (i = y - 1; i >= 0 && retVal; i--)
            {
                if (seating[i][x] == 'L') break;
                else if (seating[i][x] == '#')
                {
                    retVal = false;
                }
            }

            // Check S
            for (i = y + 1; i < seating.Count && retVal; i++)
            {
                if (seating[i][x] == 'L') break;
                else if (seating[i][x] == '#')
                {
                    retVal = false;
                }
            }

            // Check W
            for (j = x - 1; j >= 0 && retVal; j--)
            {
                if (seating[y][j] == 'L') break;
                else if (seating[y][j] == '#')
                {
                    retVal = false;
                }
            }

            // Check E
            for (j = x + 1; j < seating[y].Length && retVal; j++)
            {
                if (seating[y][j] == 'L') break;
                else if (seating[y][j] == '#')
                {
                    retVal = false;
                }
            }
            return retVal;
        }

        private static int CheckOccupiedSeatP2(int x, int y, List<char[]> seating)
        {
            int retVal = 0;
            bool keepGoing = true;
            int i, j;

            // Check NW
            i = y - 1;
            for (j = x - 1; i >= 0 && j >= 0 && keepGoing; i--, j--)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check NE
            keepGoing = true;
            i = y - 1;
            for (j = x + 1; i >= 0 && j < seating[i].Length && keepGoing; i--, j++)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check SE
            keepGoing = true;
            i = y + 1;
            for (j = x + 1; i < seating.Count && j < seating[i].Length && keepGoing; i++, j++)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check SW
            keepGoing = true;
            i = y + 1;
            for (j = x - 1; i < seating.Count && j >= 0 && keepGoing; i++, j--)
            {
                if (seating[i][j] == 'L') break;
                else if (seating[i][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check N
            keepGoing = true;
            for (i = y - 1; i >= 0 && keepGoing; i--)
            {
                if (seating[i][x] == 'L') break;
                else if (seating[i][x] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check S
            keepGoing = true;
            for (i = y + 1; i < seating.Count && keepGoing; i++)
            {
                if (seating[i][x] == 'L') break;
                else if (seating[i][x] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check W
            keepGoing = true;
            for (j = x - 1; j >= 0 && keepGoing; j--)
            {
                if (seating[y][j] == 'L') break;
                else if (seating[y][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            // Check E
            keepGoing = true;
            for (j = x + 1; j < seating[y].Length && keepGoing; j++)
            {
                if (seating[y][j] == 'L') break;
                else if (seating[y][j] == '#')
                {
                    retVal++;
                    keepGoing = false;
                }
            }

            return retVal;
        }

        public static void Part2()
        {
            List<char[]> seating = new List<char[]>();
            List<char[]> nextSeating = new List<char[]>();
            using (var sr = new StreamReader("Day11_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    seating.Add(line.ToCharArray());
                    nextSeating.Add(line.ToCharArray());
                }
            }

            //PrintState(seating);

            do
            {
                seating = nextSeating;
                nextSeating = new List<char[]>();
                for (int y = 0; y < seating.Count; y++)
                {
                    nextSeating.Add(new char[seating[y].Length]);
                    for (int x = 0; x < seating[y].Length; x++)
                    {
                        if (seating[y][x] == 'L' && CheckEmptySeatP2(x, y, seating)) nextSeating[y][x] = '#';
                        else if (seating[y][x] == '#' && CheckOccupiedSeatP2(x, y, seating) >= 5) nextSeating[y][x] = 'L';
                        else nextSeating[y][x] = seating[y][x];
                    }
                }

                //PrintState(nextSeating);
            } while (!CompareSeating(seating, nextSeating));

            UInt64 count = 0;
            for (int y = 0; y < seating.Count; y++)
            {
                for (int x = 0; x < seating[y].Length; x++)
                {
                    count += (seating[y][x] == '#' ? (UInt64)1 : (UInt64)0);
                }
            }
            Console.WriteLine(count);

            Console.ReadKey();
        }
    }
}
