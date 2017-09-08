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
    class Transaction
    {
        public Person Payer { get; set; }
        public Person Payee { get; set; }
        public string Narrative { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }


        public Transaction(Person payer, Person payee, string narrative, DateTime date, decimal amount)
        {
            Payer = payer;
            Payee = payee;
            Narrative = narrative;
            Date = date;
            Amount = amount;
        }

        public void OutputTransaction()
        {
            Console.WriteLine("{0} owed {1} £{2} for {3} on {4}", this.Payer.Name, this.Payee.Name, this.Amount, this.Narrative, this.Date.ToString("dd/MM/yyyy"));
        }

        

    }
}
