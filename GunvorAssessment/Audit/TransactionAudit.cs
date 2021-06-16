using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunvorAssessment.Audit
{

    public interface ITransactionAudit
    {
        Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(int accountNumber);

        Task WriteTransactionAsync(Transaction transaction);
    }


    public class TransactionAudit : ITransactionAudit
    {
        public TransactionAudit()
        {
            Transactions = new List<Transaction>();
        }
        public static List<Transaction> Transactions { get; set; }
        public Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(int accountNumber)
        {
            return Task.FromResult(Transactions.Where(x => x.AccountNumber == accountNumber));
        }

        public Task WriteTransactionAsync(Transaction transaction)
        {
            Transactions.Add(transaction);
            return Task.CompletedTask;
        }
    }
}
