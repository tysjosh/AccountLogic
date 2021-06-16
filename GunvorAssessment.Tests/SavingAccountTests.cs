using GunvorAssessment.Account;
using GunvorAssessment.Exceptions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GunvorAssessment.Tests
{
    [TestFixture]
    public class SavingAccountTests
    {
        private IAccount _account;

        [SetUp]
        public void Setup()
        {
            Container.Initialize();
            _account = Container.Factory.GetAccount(AccountType.Saving, 2222);
        }

        [Test]
        public async Task Withdraw_LessThan10PercentOfBalance_ShouldBePossible()
        {
            await _account.DepositAsync(2000);
            await _account.WithdrawAsync(150);
            Assert.AreEqual(1850, _account.Balance, "Balance should be 1850");

            await _account.WithdrawAsync(100);
            Assert.AreEqual(1750, _account.Balance, "Balance should be 1750");
        }

        [Test]
        public async Task Withdraw_MoreThan10PercentOfBalance_ShouldThrowAnException()
        {
            await _account.DepositAsync(2000);
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.WithdrawAsync(_account.Balance * 0.2M));
        }

        [Test]
        public void Withdraw_NegativeAmount_ShouldThrowAnException()
        {
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.WithdrawAsync(-50));
        }
    }
}