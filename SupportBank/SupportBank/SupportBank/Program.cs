using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SupportBank
{

    class Person
    {
        private string name;
        private decimal balance = 0;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public Person(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public void updateBalance(decimal amount)
        {
            this.balance += amount;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            int noOfLines = 0;

            using (var reader = new StreamReader(@"\Work\Training\Exercises\SupportBank\Transactions2014.csv"))
            {
                List<Person> people = new List<Person>();
                List<decimal> balance = new List<decimal>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(',');

                    if (noOfLines > 0)
                    {
                        Console.WriteLine(elements[4]);

                        decimal amount = Convert.ToDecimal(elements[4]);

                        if (!people.Any(p => p.Name.Equals(elements[1])))
                        {
                            people.Add(new Person(elements[1], -amount));
                        }
                        else
                        {
                            Person tempPerson = people.Find(p => (p.Name == elements[1]));
                            people[people.IndexOf(tempPerson)].updateBalance(-amount);
                        }

                        if (!people.Any(p => p.Name.Equals(elements[2])))
                        {
                            people.Add(new Person(elements[2], amount));
                        }
                        else
                        {
                            Person tempPerson = people.Find(p => (p.Name == elements[2]));
                            people[people.IndexOf(tempPerson)].updateBalance(amount);
                        }

                        }

                        noOfLines++;
                    }

                    foreach (Person person in people)
                    {
                        Console.WriteLine("{0} has a balance of £{1}.", person.Name, person.Balance);
                    }
                }


            }
        }
    }

