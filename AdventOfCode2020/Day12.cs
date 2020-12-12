using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12
    {
        public static void Part1()
        {

            Int64 x = 0, y = 0, direction = 90;
            using (var sr = new StreamReader("Day12_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    switch (line[0])
                    {
                        case 'N':
                            y += Convert.ToInt64(line.Substring(1));
                            break;
                        case 'S':
                            y -= Convert.ToInt64(line.Substring(1));
                            break;
                        case 'E':
                            x += Convert.ToInt64(line.Substring(1));
                            break;
                        case 'W':
                            x -= Convert.ToInt64(line.Substring(1));
                            break;
                        case 'L':
                            for (Int64 reducedDirectionChange = -(Math.Abs(Convert.ToInt64(line.Substring(1))) % 360); reducedDirectionChange != 0; reducedDirectionChange += 90)
                            {
                                if (direction == 0) direction = 270;
                                else direction -= 90;
                            }
                            break;
                        case 'R':
                            direction += Convert.ToInt64(line.Substring(1)) % 360;
                            break;
                        case 'F':
                            switch (direction)
                            {
                                case 0:
                                    y += Convert.ToInt64(line.Substring(1));
                                    break;
                                case 90:
                                    x += Convert.ToInt64(line.Substring(1));
                                    break;
                                case 180:
                                    y -= Convert.ToInt64(line.Substring(1));
                                    break;
                                case 270:
                                    x -= Convert.ToInt64(line.Substring(1));
                                    break;
                            }
                            break;
                    }
                }
                Console.WriteLine(Math.Abs(x) + Math.Abs(y));
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            
        }
    }
}
