using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class RawTransaction
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Narrative { get; set; }
        public string Date { get; set; }
        public string Amount { get; set; }


        public RawTransaction(string fromAccount, string toAccount, string narrative, string date, string amount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Narrative = narrative;
            Date = date;
            Amount = amount;
        }
    }
}
