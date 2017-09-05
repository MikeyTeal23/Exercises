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

            bool divisBy3;
            bool divisBy5;
            bool divisBy7;
            bool divisBy11;
            bool divisBy13;
            string result;

            for(int i = 1; i < 201; i++)
            {
                divisBy3 = i % 3 == 0;
                divisBy5 = i % 5 == 0;
                divisBy7 = i % 7 == 0;
                divisBy11 = i % 11 == 0;
                divisBy13 = i % 13 == 0;
                result = "";

                //if (divisBy3 && divisBy5) { Console.WriteLine("FizzBuzz"); }
                if (divisBy3) { result += "Fizz"; }
                if (divisBy13) { result += "Fezz"; }
                if (divisBy5) { result += "Buzz"; }
                if (divisBy7) { result += "Bang"; }
                if (divisBy11)
                {
                    if (divisBy13) { result = "FezzBong"; }
                    else { result = "Bong"; }
                }
                if (result == "") { result = i.ToString(); }

                Console.WriteLine(result);
            }

        }
    }
}
