using System.Threading.Tasks;

namespace GunvorAssessment.Account
{
    public interface IAccount
    {
        int AccountNumber { get; }

        decimal OverdraftLimit { get; set; }

        decimal Balance { get; }

        Task DepositAsync(decimal amount);

        Task WithdrawAsync(decimal amount);
    }
}
