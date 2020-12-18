using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day18
    {
        private static Int64 EvaluateExpression(string expression)
        {
            Int64 retVal = 0;

            Regex rx = new Regex(@"\([\d\s\+\*]+\)");
            MatchCollection matches = rx.Matches(expression);
            while (matches.Count != 0)
            {
                foreach (Match match in matches)
                {
                    string result = Convert.ToString(EvaluateExpression(match.Value.Substring(1, match.Value.Length - 2)));
                    expression = expression.Replace(match.Value, result);
                }
                matches = rx.Matches(expression);
            }

            string[] splitExpression = expression.Split(" ");
            retVal = Convert.ToInt64(splitExpression[0]);
            for (int i = 1; i < splitExpression.Length; i+=2)
            {
                if (splitExpression[i] == "*") retVal *= Convert.ToInt64(splitExpression[i + 1]);
                else retVal += Convert.ToInt64(splitExpression[i + 1]);
            }

            return retVal;
        }

        private static Int64 EvaluateExpressionP2(string expression)
        {
            Int64 retVal = 0;
            string[] splitExpression;
            Regex rx = new Regex(@"\([\d\s\+\*]+\)");
            MatchCollection matches = rx.Matches(expression);
            while (matches.Count != 0)
            {
                foreach (Match match in matches)
                {
                    string result = Convert.ToString(EvaluateExpressionP2(match.Value.Substring(1, match.Value.Length - 2)));
                    expression = expression.Replace(match.Value, result);
                }
                matches = rx.Matches(expression);
            }

            rx = new Regex(@"[\d\s]+\+[\d\s]+");
            matches = rx.Matches(expression);
            if (matches.Count == 1 && expression.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length == 3)
            {
                splitExpression = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                return Convert.ToInt64(splitExpression[0]) + Convert.ToInt64(splitExpression[2]);
            }

            while (matches.Count != 0)
            {
                foreach (Match match in matches)
                {
                    string result = " " + Convert.ToString(EvaluateExpressionP2(match.Value)) + " ";
                    expression = expression.Replace(match.Value, result);
                }
                matches = rx.Matches(expression);
            }

            splitExpression = expression.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            retVal = Convert.ToInt64(splitExpression[0]);
            for (int i = 1; i < splitExpression.Length; i += 2)
            {
                if (splitExpression[i] == "*") retVal *= Convert.ToInt64(splitExpression[i + 1]);
                else retVal += Convert.ToInt64(splitExpression[i + 1]);
            }

            return retVal;
        }

        public static void Part1()
        {
            Int64 sum = 0, result;
            using (var sr = new StreamReader("Day18_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result = EvaluateExpression(line);
                    sum += result;
                    Console.WriteLine("{0} = {1}", line, result);
                }
            }
            Console.WriteLine("Sum: {0}", sum);
            Console.ReadKey();
        }

        public static void Part2()
        {
            Int64 sum = 0, result;
            using (var sr = new StreamReader("Day18_Input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result = EvaluateExpressionP2(line);
                    sum += result;
                    Console.WriteLine("{0} = {1}", line, result);
                }
            }
            Console.WriteLine("Sum: {0}", sum);
            Console.ReadKey();
        }
    }
}
