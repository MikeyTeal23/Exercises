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
            string result;

            for(int i = 1; i < 101; i++)
            {
                divisBy3 = i % 3 == 0;
                divisBy5 = i % 5 == 0;
                result = "";

                //if (divisBy3 && divisBy5) { Console.WriteLine("FizzBuzz"); }
                if (divisBy3) { result += "Fizz"; }
                if (divisBy5) { result += "Buzz"; }
                if (result == "") { result = i.ToString(); }

                Console.WriteLine(result);
            }

        }
    }
}
