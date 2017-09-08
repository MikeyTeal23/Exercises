using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;
using NLog;
using NLog.Targets;

namespace SupportBank
{
    class Person
    {
        private static readonly ILogger loggerPerson = LogManager.GetCurrentClassLogger();

        public string Name { get; set; }
        public decimal Balance { get; set; }
        
        public Person(string name, decimal balance)
        {
            Name = name;
            Balance = balance;
        }

        public void UpdateBalance(decimal amount)
        {
            this.Balance += amount;
            loggerPerson.Info("updating balance");
        }

        public void OutputBalance()
        {
            Console.WriteLine("{0} has a balance of £{1}.", this.Name, this.Balance);
        }

        public void OutputName()
        {
            Console.WriteLine("{0}", this.Name);
        }

    }
}
