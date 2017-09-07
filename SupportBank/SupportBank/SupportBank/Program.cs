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



    class Program
    {
        static Person findPerson(string personName, List<Person> people)
        {
            logger.Info("Finding name in list");

            Person person = people.Find(p => (p.Name == personName));
            return person;
        }

        static Person updatePerson(List<Person> people, string name, decimal amount)
        {
            Person fromPerson;

            if (!people.Any(p => p.Name.Equals(name)))
            {
                logger.Info("Adding new person");

                fromPerson = new Person(name, amount);
                people.Add(fromPerson);
            }
            else
            {
                logger.Info("Updating person");

                fromPerson = findPerson(name, people);
                fromPerson.updateBalance(amount);
            }

            return fromPerson;
        }

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            int lineCounter = 0;

            logger.Info("Starting to read through CSV file");

            using (var reader = new StreamReader(@"\Work\Training\Exercises\SupportBank\Transactions2015.csv"))
            {
                List<Transaction> transactions = new List<Transaction>();
                List<Person> people = new List<Person>();
                List<decimal> balance = new List<decimal>();

                Person fromPerson;
                Person toPerson;
                decimal amount = 0;
                DateTime date = DateTime.MinValue;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(',');

                    lineCounter++;

                    if (lineCounter > 1)
                    {
                        logger.Info("Taking information from new line.");

                        try
                        {
                            date = Convert.ToDateTime(elements[0]);
                        }
                        catch (FormatException ex)
                        {
                            logger.Error("Date is in incorrect format");
                            Console.WriteLine(ex);
                        }

                        string narrative = elements[3];

                        try
                        {
                            amount = Convert.ToDecimal(elements[4]);
                        }
                        catch (FormatException ex)
                        {
                            logger.Error("Amount is in incorrect format");
                            Console.WriteLine(ex);
                        }

                        fromPerson = updatePerson(people, elements[1], -amount);
                        toPerson = updatePerson(people, elements[2], amount);
                        transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));
                    }
                }

                logger.Info("Finished reading file");

                Console.WriteLine("Type the name of the account you would like to look at the transactions for.\n" +
                    "If you wish to view all transactions, please type \"all\". \n" +
                    "There are accounts for the following people:");


                //foreach (Person person in people) { person.outputBalance(); }
                foreach (Person person in people) { Console.WriteLine(person.Name); }

                logger.Info("Waiting for user input");

                string reply = Console.ReadLine();
                
                if (reply == "all")
                {
                    foreach (Transaction transaction in transactions) { transaction.outputTransaction(); }
                }
                else
                {
                    transactions.Where(t => t.Payee.Name == reply);
                    foreach (Transaction transaction in transactions.Where(t => t.Payee.Name == reply || t.Payer.Name == reply))
                    {
                        transaction.outputTransaction();
                    }
                }
            }
        }
    }
}

