﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {

            bool divisBy3 = false;
            bool divisBy5 = false;
            bool divisBy7 = false;
            bool divisBy11 = false;
            bool divisBy13 = false;
            bool divisBy17 = false;
            List<string> result = new List<string>();
            int upperLimit;
            string rulesString;
            List<string> rulesList = new List<string>();

            Console.WriteLine("Please enter the number you would like to go up to.");

            upperLimit = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the numbers you would like to have rules apply to, separated by commas." +
                              "Choose from 3, 5, 7, 11, 13, 17");

            rulesString = Console.ReadLine();
            rulesList = rulesString.Split(',').ToList();

            for (int i = 1; i <= upperLimit; i++)
            {
                if (rulesList.Contains("3")) { divisBy3 = i % 3 == 0; }
                if (rulesList.Contains("5")) { divisBy5 = i % 5 == 0; }
                if (rulesList.Contains("7")) { divisBy7 = i % 7 == 0; }
                if (rulesList.Contains("11")) { divisBy11 = i % 11 == 0; }
                if (rulesList.Contains("13")) { divisBy13 = i % 13 == 0; }
                if (rulesList.Contains("17")) { divisBy17 = i % 17 == 0; }
                result.Clear();

                //if (divisBy3 && divisBy5) { Console.WriteLine("FizzBuzz"); }
                if (divisBy3) { result.Add("Fizz"); }
                if (divisBy13) { result.Add("Fezz"); }
                if (divisBy5) { result.Add("Buzz"); }
                if (divisBy7) { result.Add("Bang"); }
                if (divisBy11)
                {
                    result.Clear();
                    if (divisBy13) { result.Add("FezzBong"); }
                    else { result.Add("Bong"); }
                }
                if (result.Count == 0) { result.Add(i.ToString()); }

                if (divisBy17) { result.Reverse(); }
                Console.WriteLine(string.Join("", result));
            }

        }
    }
}
