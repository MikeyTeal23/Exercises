using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public interface Parser
    {
         List<Transaction> CreateTransactionList(string filename);
    }
}
