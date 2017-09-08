using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog.Config;
using NLog;
using NLog.Targets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SupportBank
{
    class JsonParser : Parser
    {
        private static readonly ILogger loggerJson = LogManager.GetCurrentClassLogger();

        public override List<Transaction> CreateTransactionList(string filename)
        {
            List<RawTransaction> t = new List<RawTransaction>();
            List<Transaction> transactions = new List<Transaction>();
            DateTime date;
            decimal amount;

            using (StreamReader r = new StreamReader(filename))
            {
                string jsonString = r.ReadToEnd();
                t = JsonConvert.DeserializeObject<List<RawTransaction>>(jsonString);
            }
            loggerJson.Info("Finished reading file");

            int lineCounter = 0;

            foreach (RawTransaction transactionString in t)
            {
                lineCounter++;
                try
                {
                    date = Convert.ToDateTime(transactionString.Date);
                }
                catch (FormatException ex)
                {
                    loggerJson.Error("Error is in entry {0}.  Date is in incorrect format.", lineCounter);
                    Console.WriteLine("Error is in entry {0}.  Date is in incorrect format.  Note that " +
                        "this entry has not been included.\n ", lineCounter);
                    continue;
                }

                string narrative = transactionString.Narrative;

                try
                {
                    amount = Convert.ToDecimal(transactionString.Amount);
                }
                catch (FormatException ex)
                {
                    loggerJson.Error("Error is in entry {0}.  Amount is in incorrect format.", lineCounter);
                    Console.WriteLine("Error is in entry line {0}.  Amount is in incorrect format.  Note that " +
                        "this entry has not been included.\n ", lineCounter);
                    continue;
                }

                Person fromPerson = new Person(transactionString.FromAccount, 0);
                Person toPerson = new Person(transactionString.ToAccount, 0);

                transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));

            }

            return transactions;
        }

    }
}
