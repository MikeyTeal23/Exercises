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
                        Console.WriteLine(elements[4]);

                        DateTime date = Convert.ToDateTime(elements[0]);
                        string narrative = elements[3];
                        decimal amount = Convert.ToDecimal(elements[4]);

                        if (!people.Any(p => p.Name.Equals(elements[1])))
                        {
                            fromPerson = new Person(elements[1], -amount);
                            people.Add(fromPerson);
                        }
                        else
                        {
                            fromPerson = people.Find(p => (p.Name == elements[1]));
                            people[people.IndexOf(fromPerson)].updateBalance(-amount);
                        }

                        if (!people.Any(p => p.Name.Equals(elements[2])))
                        {
                            toPerson = new Person(elements[2], amount);
                            people.Add(toPerson);
                        }
                        else
                        {
                            toPerson = people.Find(p => (p.Name == elements[2]));
                            people[people.IndexOf(toPerson)].updateBalance(amount);
                        }

                        transactions.Add(new Transaction(fromPerson, toPerson, narrative, date, amount));
                    }

                    noOfLines++;
                }

                foreach (Person person in people)
                {
                    Console.WriteLine("{0} has a balance of £{1}.", person.Name, person.Balance);
                }

                foreach (Transaction transaction in transactions)
                {
                    Console.WriteLine("{0} owed {1} £{2} for {3} on {4}", transaction.Payer.Name, transaction.Payee.Name, transaction.Amount, transaction.Narrative, transaction.Date.ToString("dd/MM/yyyy"));
                }
            }
        }
    }
}

