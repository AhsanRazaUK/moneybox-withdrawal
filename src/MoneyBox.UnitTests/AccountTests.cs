using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.Domain.Services;
using Moq;
using System;

namespace MoneyBox.UnitTests
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Deposit_PayInLimitExceeded_InvalidOperationExceptionThrown()
        {

            var account = new Account
            {
                PaidIn = 3999m
            };

            account.Deposit(100m);
        }
        [TestMethod]
        public void Deposit_ApproachingPayInLimit_NotifyApproachingPayInLimitEmailSentToUser()
        {

            var email = "money@box.com";

            var mock = MockingHelper.GetNotificationServiceMock(email);

            var account = new Account()
            {
                User = new User(mock.Object)
                {
                    Email = email
                },

                PaidIn = 3000m
            };

            account.Deposit(999m);

            mock.Verify(m => m.NotifyApproachingPayInLimit(It.Is<string>(em => em.Equals(email))), Times.AtLeastOnce);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Withdraw_InsufficientFundsLimitReached_InvalidOperationExceptionThrown()
        {

            var account = new Account
            {
                Balance = 0m
            };

            account.Withdraw(100m);
        }

        [TestMethod]
        public void Withdraw_NotificationLowFunds_NotifyNotifyFundsLowEmailSentToUser()
        {

            var email = "money@box.com";

            var mock = MockingHelper.GetNotificationServiceMock(email);

            var account = new Account()
            {
                User = new User(mock.Object)
                {
                    Email = email
                },

                Balance = 500m
            };

            account.Withdraw(100m);

            mock.Verify(m => m.NotifyFundsLow(It.Is<string>(em => em.Equals(email))), Times.AtLeastOnce);
        }
    }
}
