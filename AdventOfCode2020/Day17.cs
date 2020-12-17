using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    // Coord z,y,x

    public static class Day17
    {
        private static int[] MakeCoordIntArray3D(string input)
        {
            int[] retVal = new int[3];
            string[] splitInput = input.Split(',');
            retVal[0] = Convert.ToInt32(splitInput[0]);
            retVal[1] = Convert.ToInt32(splitInput[1]);
            retVal[2] = Convert.ToInt32(splitInput[2]);
            return retVal;
        }

        private static string MakeCoordString3D(int[] input)
        {
            return input[0] + "," + input[1] + "," + input[2];
        }

        private static string MakeCoordString3D(int z, int y, int x)
        {
            return z + "," + y + "," + x;
        }

        private static void Print3D(List<string> input)
        {
            // Find min and max in each dimension
            int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue, minz = int.MaxValue, maxz = int.MinValue;
            foreach (string s in input)
            {
                int[] ia = MakeCoordIntArray3D(s);
                if (ia[0] < minz) minz = ia[0];
                if (ia[0] > maxz) maxz = ia[0];

                if (ia[1] < miny) miny = ia[1];
                if (ia[1] > maxy) maxy = ia[1];

                if (ia[2] < minx) minx = ia[2];
                if (ia[2] > maxx) maxx = ia[2];
            }

            for (int z = minz; z <= maxz; z++)
            {
                Console.WriteLine("z={0}", z);
                for (int y = maxy; y >= miny; y--)
                {
                    for (int x = minx; x <= maxx; x++)
                    {
                        if (input.Contains(MakeCoordString3D(z, y, x))) Console.Write("#");
                        else Console.Write(".");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        private static List<string> RunCycle3D(List<string> currentState)
        {
            List<string> nextState = new List<string>();

            // Check active cubes
            int[] coordInt;
            string coordString;
            int activeNeighborCount = 0;
            foreach (string s in currentState)
            {
                coordInt = MakeCoordIntArray3D(s);
                activeNeighborCount = 0;
                for (int z = coordInt[0] - 1; z <= coordInt[0] + 1; z++)
                {
                    for (int y = coordInt[1] - 1; y <= coordInt[1] + 1; y++)
                    {
                        for (int x = coordInt[2] - 1; x <= coordInt[2] + 1; x++)
                        {
                            coordString = MakeCoordString3D(z, y, x);
                            if (s == coordString) continue;
                            else
                            {
                                if (currentState.Contains(coordString)) activeNeighborCount++;
                            }
                        }
                    }
                }
                if (activeNeighborCount == 2 || activeNeighborCount == 3) nextState.Add(s);
            }

            // Check inactive cubes
            // Find min and max in each dimension (adding/subtracting 1 from each dimension to account for surrounding inactive cubes)
            int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue, minz = int.MaxValue, maxz = int.MinValue;
            foreach (string s in currentState)
            {
                int[] ia = MakeCoordIntArray3D(s);
                if (ia[0] < minz) minz = ia[0];
                if (ia[0] > maxz) maxz = ia[0];

                if (ia[1] < miny) miny = ia[1];
                if (ia[1] > maxy) maxy = ia[1];

                if (ia[2] < minx) minx = ia[2];
                if (ia[2] > maxx) maxx = ia[2];
            }
            minz--;
            maxz++;
            miny--;
            maxy++;
            minx--;
            maxx++;

            for (int z = minz; z <= maxz; z++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    for (int x = minx; x <= maxx; x++)
                    {
                        coordInt = MakeCoordIntArray3D(MakeCoordString3D(z, y, x));
                        activeNeighborCount = 0;
                        for (int nz = coordInt[0] - 1; nz <= coordInt[0] + 1; nz++)
                        {
                            for (int ny = coordInt[1] - 1; ny <= coordInt[1] + 1; ny++)
                            {
                                for (int nx = coordInt[2] - 1; nx <= coordInt[2] + 1; nx++)
                                {
                                    coordString = MakeCoordString3D(nz, ny, nx);
                                    if (coordString == MakeCoordString3D(z, y, x)) continue;
                                    else
                                    {
                                        if (currentState.Contains(coordString)) activeNeighborCount++;
                                    }
                                }
                            }
                        }
                        if (activeNeighborCount == 3) nextState.Add(MakeCoordString3D(z, y, x));
                    }
                }
            }
            return nextState;
        }

        public static void Part1()
        {
            List<string> activeCubes = new List<string>();
            using (var sr = new StreamReader("Day17_Input.txt"))
            {
                string line;
                int x = 0, y = 0, z = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    x = 0;
                    foreach (char c in line)
                    {
                        if (c == '#')
                            activeCubes.Add(MakeCoordString3D(z, y, x));
                        x++;
                    }
                    y--;
                }
            }
            Print3D(activeCubes);
            for (int i = 1; i <= 6; i++)
            {
                Console.WriteLine("After {0} cycles:", i);
                activeCubes = RunCycle3D(activeCubes);
                Print3D(activeCubes);

            }
            Console.WriteLine("Active Cubes: {0}", activeCubes.Distinct().ToList().Count);

            Console.ReadKey();
        }

        private static int[] MakeCoordIntArray4D(string input)
        {
            int[] retVal = new int[4];
            string[] splitInput = input.Split(',');
            retVal[0] = Convert.ToInt32(splitInput[0]);
            retVal[1] = Convert.ToInt32(splitInput[1]);
            retVal[2] = Convert.ToInt32(splitInput[2]);
            retVal[3] = Convert.ToInt32(splitInput[3]);
            return retVal;
        }

        private static string MakeCoordString4D(int[] input)
        {
            return input[0] + "," + input[1] + "," + input[2] + "," + input[3];
        }

        private static string MakeCoordString4D(int w, int z, int y, int x)
        {
            return w + "," + z + "," + y + "," + x;
        }

        private static void Print4D(List<string> input)
        {
            // Find min and max in each dimension
            int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue, minz = int.MaxValue, maxz = int.MinValue, minw = int.MaxValue, maxw = int.MinValue;
            foreach (string s in input)
            {
                int[] ia = MakeCoordIntArray4D(s);
                if (ia[0] < minw) minw = ia[0];
                if (ia[0] > maxw) maxw = ia[0];

                if (ia[1] < minz) minz = ia[1];
                if (ia[1] > maxz) maxz = ia[1];

                if (ia[2] < miny) miny = ia[2];
                if (ia[2] > maxy) maxy = ia[2];

                if (ia[3] < minx) minx = ia[3];
                if (ia[3] > maxx) maxx = ia[3];
            }
            for (int w = minw; w <= maxw; w++)
            {
                for (int z = minz; z <= maxz; z++)
                {
                    Console.WriteLine("z={0} w={1}", z, w);
                    for (int y = maxy; y >= miny; y--)
                    {
                        for (int x = minx; x <= maxx; x++)
                        {
                            if (input.Contains(MakeCoordString4D(w, z, y, x))) Console.Write("#");
                            else Console.Write(".");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
        }

        /*private static List<string> RunCycle4D(List<string> currentState)
        {
            List<string> nextState = new List<string>();

            // Check active cubes
            int[] coordInt;
            string coordString;
            int activeNeighborCount = 0;
            foreach (string s in currentState)
            {
                coordInt = MakeCoordIntArray3D(s);
                activeNeighborCount = 0;
                for (int z = coordInt[0] - 1; z <= coordInt[0] + 1; z++)
                {
                    for (int y = coordInt[1] - 1; y <= coordInt[1] + 1; y++)
                    {
                        for (int x = coordInt[2] - 1; x <= coordInt[2] + 1; x++)
                        {
                            coordString = MakeCoordString3D(z, y, x);
                            if (s == coordString) continue;
                            else
                            {
                                if (currentState.Contains(coordString)) activeNeighborCount++;
                            }
                        }
                    }
                }
                if (activeNeighborCount == 2 || activeNeighborCount == 3) nextState.Add(s);
            }

            // Check inactive cubes
            // Find min and max in each dimension (adding/subtracting 1 from each dimension to account for surrounding inactive cubes)
            int minx = int.MaxValue, maxx = int.MinValue, miny = int.MaxValue, maxy = int.MinValue, minz = int.MaxValue, maxz = int.MinValue;
            foreach (string s in currentState)
            {
                int[] ia = MakeCoordIntArray3D(s);
                if (ia[0] < minz) minz = ia[0];
                if (ia[0] > maxz) maxz = ia[0];

                if (ia[1] < miny) miny = ia[1];
                if (ia[1] > maxy) maxy = ia[1];

                if (ia[2] < minx) minx = ia[2];
                if (ia[2] > maxx) maxx = ia[2];
            }
            minz--;
            maxz++;
            miny--;
            maxy++;
            minx--;
            maxx++;

            for (int z = minz; z <= maxz; z++)
            {
                for (int y = miny; y <= maxy; y++)
                {
                    for (int x = minx; x <= maxx; x++)
                    {
                        coordInt = MakeCoordIntArray3D(MakeCoordString3D(z, y, x));
                        activeNeighborCount = 0;
                        for (int nz = coordInt[0] - 1; nz <= coordInt[0] + 1; nz++)
                        {
                            for (int ny = coordInt[1] - 1; ny <= coordInt[1] + 1; ny++)
                            {
                                for (int nx = coordInt[2] - 1; nx <= coordInt[2] + 1; nx++)
                                {
                                    coordString = MakeCoordString3D(nz, ny, nx);
                                    if (coordString == MakeCoordString3D(z, y, x)) continue;
                                    else
                                    {
                                        if (currentState.Contains(coordString)) activeNeighborCount++;
                                    }
                                }
                            }
                        }
                        if (activeNeighborCount == 3) nextState.Add(MakeCoordString3D(z, y, x));
                    }
                }
            }
            return nextState;
        }
        */

        public static void Part2()
        {
            List<string> activeCubes = new List<string>();
            using (var sr = new StreamReader("Day17_Input.txt"))
            {
                string line;
                int x = 0, y = 0, z = 0, w = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    x = 0;
                    foreach (char c in line)
                    {
                        if (c == '#')
                            activeCubes.Add(MakeCoordString4D(w, z, y, x));
                        x++;
                    }
                    y--;
                }
            }
            Print3D(activeCubes);
            /*
            for (int i = 1; i <= 6; i++)
            {
                Console.WriteLine("After {0} cycles:", i);
                activeCubes = RunCycle4D(activeCubes);
                Print3D(activeCubes);

            }
            Console.WriteLine("Active Cubes: {0}", activeCubes.Distinct().ToList().Count);
            */

            Console.ReadKey();
        }
    }
}
