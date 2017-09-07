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
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;

            logger.Info("Starting to read through CSV file");

            string inputFile = @"\Work\Training\Exercises\SupportBank\Transactions2015.csv";
            List<Transaction> transactions = readTransactions(inputFile);
            List<Person> people = createPeople(transactions);

            Console.WriteLine("Type the name of the account you would like to look at the transactions for.\n" +
                "If you wish to view all transactions, please type \"all\". \n\n" +
                "There are accounts for the following people:");

            foreach (Person person in people) { person.outputName(); }

            logger.Info("Waiting for user input");

            string reply = Console.ReadLine();

            GetRequestedOutput(reply, transactions);

        }

        static List<Transaction> readTransactions(string filename)
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
                        logger.Info("Taking information from new line.");

                        try
                        {
                            date = Convert.ToDateTime(elements[0]);
                        }
                        catch (FormatException ex)
                        {
                            logger.Error("Error is on line {0}.  Date is in incorrect format.", lineCounter);
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
                            logger.Error("Error is on line {0}.  Amount is in incorrect format.", lineCounter);
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

            logger.Info("Finished reading file");
            return transactions;
        }

        static List<Person> createPeople(List<Transaction> transactions)
        {
            List<Person> people = new List<Person>();

            foreach (Transaction transaction in transactions)

                if (!people.Any(p => p.Name.Equals(transaction.Payee.Name)))
                {
                    logger.Info("Adding new person");

                    Person newPerson = new Person(transaction.Payee.Name, -transaction.Amount);
                    people.Add(newPerson);
                }
                else
                {
                    logger.Info("Updating person");

                    Person oldPerson = findPerson(transaction.Payee.Name, people);
                    oldPerson.updateBalance(-transaction.Amount);
                }

            foreach (Transaction transaction in transactions)

                if (!people.Any(p => p.Name.Equals(transaction.Payer.Name)))
                {
                    logger.Info("Adding new person");

                    Person newPerson = new Person(transaction.Payer.Name, transaction.Amount);
                    people.Add(newPerson);
                }
                else
                {
                    logger.Info("Updating person");

                    Person oldPerson = findPerson(transaction.Payer.Name, people);
                    oldPerson.updateBalance(transaction.Amount);
                }

            return people;
        }

        static Person findPerson(string personName, List<Person> people)
        {
            logger.Info("Finding name in list");

            Person person = people.Find(p => (p.Name == personName));
            return person;
        }

        static void GetRequestedOutput(string reply, List<Transaction> transactions)
        {
            if (reply == "all")
            {
                foreach (Transaction transaction in transactions) { transaction.outputTransaction(); }
            }
            else
            {
                foreach (Transaction transaction in transactions.Where(t => t.Payee.Name == reply || t.Payer.Name == reply))
                {
                    transaction.outputTransaction();
                }
            }
        }
    }
}