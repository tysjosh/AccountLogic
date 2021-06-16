using GunvorAssessment.Account;
using GunvorAssessment.Audit;
using GunvorAssessment.DateService;
using GunvorAssessment.LockDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunvorAssessment
{
    public interface IGlobalFactory
    {
        IAccount GetAccount(AccountType type, int accountNumber);

        ITransactionAudit GetAudit();

        ILockDownManager GetLockDownManager();

        IDateTimeService GetDateTimeService();
    }
    public static class Container
    {
        public static IGlobalFactory Factory { get; private set; }
        public static void Initialize()
        {
            Factory = new GlobalFactory();
        }
    }
}
