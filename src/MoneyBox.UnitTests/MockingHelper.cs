using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;

namespace MoneyBox.UnitTests
{
    public static class MockingHelper
    {
        public static Mock<IAccountRepository> GetAccountRepositoryMock(
            List<Account> accounts)
        {

            var accountMock = new Mock<IAccountRepository>();

            accounts.ForEach(account =>
            {
                accountMock.Setup(a =>
                a.GetAccountById(
                    It.Is<Guid>(id => id.Equals(account.Id)))).Returns(account);
            });

            return accountMock;
        }

        public static Mock<INotificationService> GetNotificationServiceMock(string email)
        {

            var notificationMock = new Mock<INotificationService>();

            notificationMock.Setup(m => m.NotifyApproachingPayInLimit(It.Is<string>(em => em.Equals(email))));
            notificationMock.Setup(m => m.NotifyFundsLow(It.Is<string>(em => em.Equals(email))));

            return notificationMock;
        }
    }
}
