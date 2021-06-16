using GunvorAssessment.Account;
using GunvorAssessment.Exceptions;
using GunvorAssessment.LockDown;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace GunvorAssessment.Tests
{
    [TestFixture]
    public class LockDownTests
    {
        private IAccount _account;
        private ILockDownManager _lockDownManager;

        [SetUp]
        public void Setup()
        {
            Container.Initialize();
            _account = Container.Factory.GetAccount(AccountType.Current, 1111);
            _account.OverdraftLimit = -1000;
            _lockDownManager = Container.Factory.GetLockDownManager();
        }

        [Test]
        public void Subject_IsSingleton()
        {
            Assert.AreEqual(Container.Factory.GetLockDownManager(), _lockDownManager);
        }

        [Test]
        public void StartLockDown_ShouldPreventDeposits()
        {
            _lockDownManager.StartLockDown();
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.DepositAsync(10));
        }
        [Test]
        public void StartLockDown_ShouldPreventWithdrawals()
        {
            _lockDownManager.StartLockDown();
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.WithdrawAsync(10));
        }

        [Test]
        public async Task EndLockDown_ShouldAllowAllOperations()
        {
            _lockDownManager.StartLockDown();
            _lockDownManager.EndLockDown();

            await _account.DepositAsync(10);
            await _account.WithdrawAsync(10);
        }

        [Test]
        public void LockDownMethods_ShouldRaiseEvents()
        {
            int lockDownStartedCount = 0;
            int lockDownEndedCount = 0;

            _lockDownManager.LockDownEnded += (sender, args) => lockDownEndedCount++;
            _lockDownManager.LockDownStarted += (sender, args) => lockDownStartedCount++;

            Assert.AreEqual(0, lockDownEndedCount);
            Assert.AreEqual(0, lockDownStartedCount);

            _lockDownManager.StartLockDown();
            Assert.AreEqual(0, lockDownEndedCount);
            Assert.AreEqual(1, lockDownStartedCount);

            _lockDownManager.StartLockDown();
            Assert.AreEqual(0, lockDownEndedCount);
            Assert.AreEqual(1, lockDownStartedCount);

            _lockDownManager.EndLockDown();
            Assert.AreEqual(1, lockDownEndedCount);
            Assert.AreEqual(1, lockDownStartedCount);

            _lockDownManager.EndLockDown();
            Assert.AreEqual(1, lockDownEndedCount);
            Assert.AreEqual(1, lockDownStartedCount);

            _lockDownManager.StartLockDown();
            Assert.AreEqual(1, lockDownEndedCount);
            Assert.AreEqual(2, lockDownStartedCount);

        }
    }
}
