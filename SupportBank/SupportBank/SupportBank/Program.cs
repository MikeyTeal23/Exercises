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
using System.Xml;
using System.Xml.Serialization;

namespace SupportBank
{

    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            CreateLogger();

            logger.Info("Starting to read through file");

            string inputFile = @"\Work\Training\Exercises\SupportBank\Transactions2012.xml";

            ParserFactory parserFactory = new ParserFactory();
            var parser = parserFactory.makeParser(inputFile);

            List<Transaction> transactions = parser.CreateTransactionList(inputFile);
            List<Person> people = CreatePeopleList(transactions);

            Console.WriteLine("Type the name of the account you would like to look at the transactions for.\n" +
                "If you wish to view all transactions, please type \"all\". \n\n" +
                "There are accounts for the following people:");

            foreach (Person person in people)
            {
                person.OutputName();
            }

            logger.Info("Waiting for user input");

            string reply = Console.ReadLine();

            GetRequestedOutput(reply, transactions);
        }



        //static List<Transaction> CreateXMLTransactionList(string filename)
        //{
        //    List<RawTransaction> t = new List<RawTransaction>();
        //    List<Transaction> transactions = new List<Transaction>();
        //    DateTime date;
        //    decimal amount;

        //    using (XmlReader reader = XmlReader.Create(filename))
        //    {

        //    }

        //    logger.Info("Finished reading file");

        //    int lineCounter = 0;

        //    foreach (RawTransaction transactionString in t)
        //    {
        //        lineCounter++;
        //        try
        //        {
        //            date = Convert.ToDateTime(transactionString.Date);
        //        }
        //        catch (FormatException ex)
        //        {
        //            logger.Error("Error is in entry {0}.  Date is in incorrect format.", lineCounter);
        //            Console.WriteLine("Error is in entry {0}.  Date is in incorrect format.  Note that " +
        //                "this entry has not been included.\n ", lineCounter);
        //            continue;
        //        }

        //        string narrative = transactionString.Narrative;

        //        try
        //        {
        //            amount = Convert.ToDecimal(transactionString.Amount);
        //        }
        //        catch (FormatException ex)
        //        {
        //            logger.Error("Error is in entry {0}.  Amount is in incorrect format.", lineCounter);
        //            Console.WriteLine("Error is in entry line {0}.  Amount is in incorrect format.  Note that " +
        //                "this entry has not been included.\n ", lineCounter);
        //            continue;
        //        }

        //        Person fromPerson = new Person(transactionString.FromAccount, 0);
        //        Person toPerson = new Person(transactionString.ToAccount, 0);

        //        transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));

        //    }

        //    return transactions;
        //}

        static List<Person> CreatePeopleList(List<Transaction> transactions)
        {
            List<Person> people = new List<Person>();

            foreach (Transaction transaction in transactions)

            {
                string[] nameArray = { transaction.ToAccount.Name, transaction.FromAccount.Name };

                foreach (string name in nameArray)
                {

                    if (!people.Any(p => p.Name.Equals(transaction.ToAccount.Name)))
                    {
                        logger.Info("Adding new person");

                        Person newPerson = new Person(transaction.ToAccount.Name, -transaction.Amount);
                        people.Add(newPerson);
                    }
                    else
                    {
                        logger.Info("Updating person");

                        Person oldPerson = FindPerson(transaction.ToAccount.Name, people);
                        oldPerson.UpdateBalance(-transaction.Amount);
                    }
                }
            }

            return people;
        }

        static Person FindPerson(string personName, List<Person> people)
        {
            logger.Info("Finding name in list");

            Person person = people.Find(p => (p.Name == personName));
            return person;
        }

        static void GetRequestedOutput(string reply, List<Transaction> transactions)
        {
            if (reply == "all")
            {
                foreach (Transaction transaction in transactions) { transaction.OutputTransaction(); }
            }
            else
            {
                foreach (Transaction transaction in transactions.Where(t => t.ToAccount.Name == reply || t.FromAccount.Name == reply))
                {
                    transaction.OutputTransaction();
                }
            }
        }

        static void CreateLogger()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }
    }
}