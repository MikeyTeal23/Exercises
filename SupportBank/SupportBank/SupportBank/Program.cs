using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SupportBank
{



    class Program
    {
        static Person findPerson(string personName, List<Person> people)
        {
            Person person = people.Find(p => (p.Name == personName));
            return person;
        }

        static Person updatePerson(List<Person> people, string name, decimal amount)
        {
            Person fromPerson;

            if (!people.Any(p => p.Name.Equals(name)))
            {
                fromPerson = new Person(name, amount);
                people.Add(fromPerson);
            }
            else
            {
                fromPerson = findPerson(name, people);
                fromPerson.updateBalance(amount);
            }

            return fromPerson;
        }
       

        static void Main(string[] args)
        {
            int noOfLines = 0;

            using (var reader = new StreamReader(@"\Work\Training\Exercises\SupportBank\Transactions2014.csv"))
            {
                List<Transaction> transactions = new List<Transaction>();
                List<Person> people = new List<Person>();
                List<decimal> balance = new List<decimal>();

                Person fromPerson;
                Person toPerson;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(',');

                    if (noOfLines > 0)
                    {
                        DateTime date = Convert.ToDateTime(elements[0]);
                        string narrative = elements[3];
                        decimal amount = Convert.ToDecimal(elements[4]);

                        fromPerson = updatePerson(people, elements[1], -amount);
                        toPerson = updatePerson(people, elements[2], amount);
                        transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));
                    }

                    noOfLines++;
                }

                Console.WriteLine("Type the name of the account you would like to look at the transactions for.\n" +
                    "If you wish to view all transactions, please type \"all\". \n" +
                    "There are accounts for the following people:");

                //foreach (Person person in people) { person.outputBalance(); }
                foreach (Person person in people) { Console.WriteLine(person.Name); }

                string reply = Console.ReadLine();
                
                if (reply == "all")
                {
                    foreach (Transaction transaction in transactions) { transaction.outputTransaction(); }
                }
                else
                {
                    foreach (Transaction transaction in transactions)
                    {
                        if (transaction.Payee.Name == reply || transaction.Payer.Name == reply)
                        {
                            transaction.outputTransaction();
                        }
                    }
                }
            }
        }
    }
}

