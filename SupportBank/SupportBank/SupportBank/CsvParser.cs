using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog.Config;
using NLog;
using NLog.Targets;


namespace SupportBank
{
    public class CsvParser : Parser
    {
        private static readonly ILogger loggerCsv = LogManager.GetCurrentClassLogger();

        public List<Transaction> CreateTransactionList(string filename)
        {
            int lineCounter = 0;

            List<Transaction> transactions = new List<Transaction>();
            List<Person> people = new List<Person>();
            Person fromPerson;
            Person toPerson;
            decimal amount;
            DateTime date;

            using (var reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(',');

                    lineCounter++;

                    if (lineCounter > 1)
                    {
                        loggerCsv.Info("Taking information from new line.");

                        try
                        {
                            date = Convert.ToDateTime(elements[0]);
                        }
                        catch (FormatException ex)
                        {
                            loggerCsv.Error("Error is on line {0}.  Date is in incorrect format.", lineCounter);
                            Console.WriteLine("Error is on line {0}.  Date is in incorrect format.  Note that " +
                                "this entry has not been included.\n ", lineCounter);
                            continue;
                        }

                        string narrative = elements[3];

                        try
                        {
                            amount = Convert.ToDecimal(elements[4]);
                        }
                        catch (FormatException ex)
                        {
                            loggerCsv.Error("Error is on line {0}.  Amount is in incorrect format.", lineCounter);
                            Console.WriteLine("Error is on line {0}.  Amount is in incorrect format.  Note that " +
                                "this entry has not been included.\n ", lineCounter);
                            continue;
                        }

                        fromPerson = new Person(elements[1], 0);
                        toPerson = new Person(elements[2], 0);

                        transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));
                    }
                }
            }

            loggerCsv.Info("Finished reading file");
            return transactions;
        }

    }
}
