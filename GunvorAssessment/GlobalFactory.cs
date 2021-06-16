using GunvorAssessment.Account;
using GunvorAssessment.Audit;
using GunvorAssessment.DateService;
using GunvorAssessment.LockDown;

namespace GunvorAssessment
{
    public enum AccountType
    {
        Current = 0,
        Saving = 1
    }
    public class GlobalFactory : IGlobalFactory
    {
        private ITransactionAudit _transactionAudit;
        private ILockDownManager _lockDownManager;
        private IDateTimeService _dateTimeService;
        public GlobalFactory()
        {
            _transactionAudit = new TransactionAudit();
            _lockDownManager = new LockDownManager();
            _dateTimeService = new DateTimeService();
        }
        public IAccount GetAccount(AccountType type, int accountNumber)
        {
            if(type == 0)
            {
                return new CurrentAccount(accountNumber, _dateTimeService, _lockDownManager, _transactionAudit);
            }
            else
            {
                return new SavingAccount(accountNumber, _dateTimeService, _lockDownManager, _transactionAudit);
            }
        }

        public ITransactionAudit GetAudit()
        {
            return _transactionAudit;
        }

        public IDateTimeService GetDateTimeService()
        {
            return _dateTimeService;
        }

        public ILockDownManager GetLockDownManager()
        {
            return _lockDownManager;
        }
    }
}
