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
        private double balance = 0;

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

        public double Balance
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

        public Person(string name, double balance)
        {
            Name = name;
            Balance = balance;
        }

        public void updateBalance(double amount)
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
                List<double> balance = new List<double>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var elements = line.Split(',');

                    if (noOfLines > 0)
                    {
                        Console.WriteLine(elements[4]);

                        double amount = Convert.ToDouble(elements[4]);

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
                        Console.WriteLine(person.Name);
                    }
                }


            }
        }
    }

