using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void outputBalance()
        {
            Console.WriteLine("{0} has a balance of £{1}.", this.Name, this.Balance);
        }
    }
}
