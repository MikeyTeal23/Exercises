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
    public class ParserFactory
    {
        private static readonly ILogger loggerFactory = LogManager.GetCurrentClassLogger();

        public static Parser MakeParser(string filename)
        {
            int index = filename.LastIndexOf(".");
            string fileType = filename.Substring(index, filename.Length - index);

            if (fileType == ".csv")
            {
                return new CsvParser();
            }
            else if (fileType == ".json")
            {
                return new JsonParser();
            }
            else
            {
                throw new Exception("Error - will not accept files of this type!\n" +
                    "Please enter .csv or .json files.\n");
            }
        }

    }
}
