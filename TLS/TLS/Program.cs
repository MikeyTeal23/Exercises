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
        static Dictionary<string, int> regexCount(string TLS, string text)
        {
            Regex rgx = new Regex(TLS, RegexOptions.IgnoreCase);

            Dictionary<string, int> dict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            Match match = rgx.Match(text);
            string[] stringArray = new string[3];
            string s;

            while (match.Success)
            {
                s = "";

                for (int i = 0; i < 3; i++)
                {
                    stringArray[i] = match.Groups[i+1].ToString();
                    stringArray[i] = stringArray[i].ToLower();
                    s += stringArray[i];
                }

                if (dict.ContainsKey(s))
                {
                    dict[s]++;
                }
                else
                {
                    dict.Add(s, 1);
                }

                match = rgx.Match(text, match.Index + 1);
            }

            return dict;
        }

        static List<string> outputEntries(Dictionary<string, int> dict, int freq)
        {

            List<string> list = dict.Where(x => x.Value == freq).Select(x => x.Key).ToList();

            //foreach (string k in dict.Keys)
            //{
            //    if(dict[k] == freq)
            //    {
            //        list.Add(k);
            //    }
            //}


            return list;
        }


        static void Main(string[] args)
        {

            string filepath = "/Work/Training/Exercises/TLS/SampleText.txt";

            string readText = System.IO.File.ReadAllText(filepath);

            string regexPattern = @"(\w)\W*(\w)\W*(\w)";
            //string regexPattern = @"\w\w\w";


            Dictionary<string, int> dictionary = regexCount(regexPattern, readText);

            var sortedDict = from entry in dictionary orderby entry.Value descending select entry;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}) the TLS {1} has {2} counts.", i, sortedDict.ElementAt(i).Key, sortedDict.ElementAt(i).Value);
            }

            Console.WriteLine("Please enter a frequency.");

            int requestedFreq = Convert.ToInt32(Console.ReadLine());

            List<string> outputList = outputEntries(dictionary, requestedFreq);

            if (outputList.Count == 0) { Console.WriteLine("There were no TLSs appearing this number of times."); }
            foreach (string entry in outputList) { Console.WriteLine(entry); }
        }
    }
}
