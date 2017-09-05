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
            bool divisBy11;
            bool divisBy13;
            bool divisBy17;
            List<string> result = new List<string>();

            for(int i = 1; i < 301; i++)
            {
                divisBy3 = i % 3 == 0;
                divisBy5 = i % 5 == 0;
                divisBy7 = i % 7 == 0;
                divisBy11 = i % 11 == 0;
                divisBy13 = i % 13 == 0;
                divisBy17 = i % 17 == 0;
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
