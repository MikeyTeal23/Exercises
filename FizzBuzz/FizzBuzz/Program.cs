using System;
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

            for(int i = 1; i < 101; i++)
            {
                divisBy3 = i % 3 == 0;
                divisBy5 = i % 5 == 0;

                if (divisBy3 && divisBy5) { Console.WriteLine("FizzBuzz"); }
                else if(divisBy3) { Console.WriteLine("Fizz"); }
                else if (divisBy5) { Console.WriteLine("Buzz"); }
                else { Console.WriteLine(i); }
            }

        }
    }
}
