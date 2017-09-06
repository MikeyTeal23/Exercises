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
            string s = "";

            while (match.Success)
            {
                Group g = match.Groups[0];
                s = g.ToString();
                s = s.ToLower();

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
            List<string> list = new List<string>();

            list = dict.Where(x => x.Value == freq).Select(x => x.Key).ToList();

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

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            string regexPattern = @"\w\w\w";

            dictionary = regexCount(regexPattern, readText);

            List<string> outputList = outputEntries(dictionary, 63);
            foreach (string entry in outputList) { Console.WriteLine(entry); }

            var sortedDict = from entry in dictionary orderby entry.Value descending select entry;

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("{0}) the TLS {1} has {2} counts.", i, sortedDict.ElementAt(i).Key, sortedDict.ElementAt(i).Value);
            }

        }
    }
}
