using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class Person
    {
        private decimal balance;

        public string Name { get; set; }
        public decimal Balance { get; set; }
        
        public Person(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public void updateBalance(decimal amount)
        {
            this.Balance += amount;
        }

        public void outputBalance()
        {
            Console.WriteLine("{0} has a balance of £{1}.", this.Name, this.Balance);
        }

    }
}
