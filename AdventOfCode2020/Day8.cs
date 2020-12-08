using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day8
    {
        private static bool RunProgram(List<int[]> program)
        {
            foreach (int[] ia in program) ia[2] = 0;
            int accumulator = 0;
            int pc = 0;
            while (true)
            {
                if (pc == program.Count)
                {
                    Console.WriteLine(accumulator);
                    return true;
                }
                if (program[pc][2] == 1)
                {
                    return false;
                }
                program[pc][2] = 1;
                switch (program[pc][0])
                {
                    case 0:
                        accumulator += program[pc++][1];
                        break;
                    case 1:
                        pc += program[pc][1];
                        break;
                    case 2:
                        pc++;
                        break;
                }

            }
        }

        public static void Part1()
        {
            List<int[]> program = new List<int[]>();
            using (var sr = new StreamReader("Day8_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(" ");
                    switch (splitLine[0])
                    {
                        case "acc":
                            program.Add(new int[] { 0, Convert.ToInt32(splitLine[1]), 0});
                            break;
                        case "jmp":
                            program.Add(new int[] { 1, Convert.ToInt32(splitLine[1]), 0 });
                            break;
                        case "nop":
                            program.Add(new int[] { 2, Convert.ToInt32(splitLine[1]), 0 });
                            break;
                    }
                }

                int accumulator = 0;
                int pc = 0;
                while (true)
                {
                    if (program[pc][2] == 1)
                    {
                        Console.WriteLine(accumulator);
                        break;
                    }
                    program[pc][2] = 1;
                    switch(program[pc][0])
                    {
                        case 0:
                            accumulator += program[pc++][1];
                            break;
                        case 1:
                            pc += program[pc][1];
                            break;
                        case 2:
                            pc++;
                            break;
                    }

                }
                    
            }
            Console.ReadKey();
        }

        public static void Part2()
        {
            List<int[]> program = new List<int[]>();
            using (var sr = new StreamReader("Day8_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(" ");
                    switch (splitLine[0])
                    {
                        case "acc":
                            program.Add(new int[] { 0, Convert.ToInt32(splitLine[1]), 0 });
                            break;
                        case "jmp":
                            program.Add(new int[] { 1, Convert.ToInt32(splitLine[1]), 0 });
                            break;
                        case "nop":
                            program.Add(new int[] { 2, Convert.ToInt32(splitLine[1]), 0 });
                            break;
                    }
                }
            }

            for (int i = 0; i < program.Count; i++)
            {
                if (program[i][0] == 1)
                {
                    program[i][0] = 2;
                    RunProgram(program);
                    program[i][0] = 1;
                }
                else if (program[i][0] == 2)
                {
                    program[i][0] = 1;
                    RunProgram(program);
                    program[i][0] = 2;
                }
            }
            Console.ReadKey();
        }
    }
}
