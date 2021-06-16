using GunvorAssessment.Audit;
using GunvorAssessment.DateService;
using GunvorAssessment.Exceptions;
using GunvorAssessment.LockDown;
using System.Threading.Tasks;
using System;

namespace GunvorAssessment.Account
{
    public class CurrentAccount : IAccount
    {
        private IDateTimeService _dateTimeService;
        private ITransactionAudit _transactionAudit;
        private LockDownManager _lockDownManager;

        public CurrentAccount(int accountNumber, IDateTimeService dateTimeService, ILockDownManager lockDownManager, ITransactionAudit transactionAudit)
        {
            AccountNumber = accountNumber;
            _dateTimeService = dateTimeService;
            _lockDownManager = lockDownManager as LockDownManager;
            _transactionAudit = transactionAudit;
        }
        public int AccountNumber { get; set; }

        public decimal OverdraftLimit { get; set; }

        public decimal Balance { get; private set; }

        public async Task DepositAsync(decimal amount)
        {
            if (_lockDownManager.isLockDown)
                throw new UnauthorizedAccountOperationException();

            if(amount > 0)
            {
                var tempValue = Balance;
                var newValue = tempValue + amount;
                Balance = newValue;

                await _transactionAudit.WriteTransactionAsync(new Transaction()
                {
                    Id = Guid.NewGuid().GetHashCode(),
                    TransactionType = TransactionType.Deposit,
                    TransactionDate = _dateTimeService.GetCurrentDateTime(),
                    AccountNumber = AccountNumber
                });
            }
            else
            {
                throw new UnauthorizedAccountOperationException();
            }
        }

        public async Task WithdrawAsync(decimal amount)
        {
            try
            {
                if (_lockDownManager.isLockDown)
                    throw new UnauthorizedAccountOperationException();

                if (amount > 0 && (Balance >= amount || amount <= Math.Abs(OverdraftLimit)))
                {
                    var tempValue = Balance;
                    var newValue = tempValue - amount;
                    Balance = newValue;

                    await _transactionAudit.WriteTransactionAsync(new Transaction()
                    {
                        Id = Guid.NewGuid().GetHashCode(),
                        TransactionType = TransactionType.Withdraw,
                        TransactionDate = _dateTimeService.GetCurrentDateTime(),
                        AccountNumber = AccountNumber
                    });

                }
                else
                {
                    throw new UnauthorizedAccountOperationException();
                }
            }
            catch (UnauthorizedAccountOperationException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
           
        }
    }
}
