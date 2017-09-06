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

            string regexPattern = "tra";

            int counter = regexCount(regexPattern, readText);

            Console.WriteLine(counter.ToString());

            //int counter = 0;

            //for(int i = 0 ; i < readText.Length - 2; i++)
            //{

            //    if((readText[i] == 't') && (readText[i+1] == 'r') && (readText[i+2] == 'a'))
            //    {
            //        counter++;
            //    }
            //}

            //Console.WriteLine(counter.ToString());

            Dictionary<string, int> dictionary = new Dictionary<string, int>();


        }
    }
}
