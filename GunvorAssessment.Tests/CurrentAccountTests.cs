using GunvorAssessment.Account;
using GunvorAssessment.Exceptions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GunvorAssessment.Tests
{
    [TestFixture]
    public class CurrentAccountTests
    {
        private IAccount _account;

        [SetUp]
        public void Setup()
        {
            Container.Initialize();
            _account = Container.Factory.GetAccount(AccountType.Current, 1111);
            _account.OverdraftLimit = -2000;
        }

        [Test]
        public async Task Withdraw_LessThanAgreedOverdraft_ShouldBePossible()
        {
            await _account.DepositAsync(2000);
            await _account.WithdrawAsync(1000);
            Assert.AreEqual(1000, _account.Balance, "Balance should be 1000");

            await _account.WithdrawAsync(1000);
            Assert.AreEqual(0, _account.Balance, "Balance should be 0");

            await _account.WithdrawAsync(500);
            Assert.AreEqual(-500, _account.Balance, "Balance should be -500");
        }

        [Test]
        public void Withdraw_MoreThanAgreedOverdraft_ShouldThrowAnException()
        {
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.WithdrawAsync(2500));
        }

        [Test]
        public void Deposit_NegativeAmount_ShouldThrowAnException()
        {
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.DepositAsync(-50));
        }

        [Test]
        public void Withdraw_NegativeAmount_ShouldThrowAnException()
        {
            Assert.CatchAsync<UnauthorizedAccountOperationException>(() => _account.WithdrawAsync(-50));
        }
    }
}
