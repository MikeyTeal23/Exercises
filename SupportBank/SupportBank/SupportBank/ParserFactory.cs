using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;
using NLog;
using NLog.Targets;

namespace SupportBank
{
    class ParserFactory
    {
        private static readonly ILogger loggerFactory = LogManager.GetCurrentClassLogger();

        public Parser makeParser(string filename)
        {
            Parser parser = new Parser();
            int index = filename.LastIndexOf(".");
            string fileType = filename.Substring(index, filename.Length - index);

            if (fileType == ".csv")
            {
                parser = new CsvParser();
            }
            else if (fileType == ".json")
            {
                parser = new JsonParser();
            }
            else
            {
                throw new Exception("Error - will not accept files of this type!\n" +
                    "Please enter .csv or .json files.\n");
            }
            return parser;
        }

    }
}
