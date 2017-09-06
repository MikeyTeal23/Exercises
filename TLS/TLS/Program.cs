using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TLS
{
    class Program
    {
        static int regexCount(string TLS, string text)
        {
            Regex rgx = new Regex(TLS, RegexOptions.IgnoreCase);

            Match match = rgx.Match(text);
            int matchCount = 0;
            while (match.Success)
            {
                matchCount++;
                match = match.NextMatch();
            }

            return matchCount;
        }


        static void Main(string[] args)
        {

            string filepath = "/Work/Training/Exercises/TLS/SampleText.txt";

            string readText = System.IO.File.ReadAllText(filepath);

            Dictionary<string, int> dict = new Dictionary<string, int>();

            string regexPattern = "";

            int counter = 0;

            for (char c0 = 'a'; c0 <= 'z'; c0++)
            {
                Console.WriteLine(c0);

                for (char c1 = 'a'; c1 <= 'z'; c1++)
                {
                    for (char c2 = 'a'; c2 <= 'z'; c2++)
                    {
                        char[] chars = { c0, c1, c2 };
                        regexPattern = new string(chars);
                        counter = regexCount(regexPattern, readText);

                        dict.Add(regexPattern, counter);

                        if(counter == 63) { Console.WriteLine(regexPattern); }
                    }
                }
            }

            dict.OrderBy(x => x.Value);

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine
            //}

            //int counter = regexCount(regexPattern, readText);

            //Console.WriteLine(counter.ToString());

        }
    }
}
