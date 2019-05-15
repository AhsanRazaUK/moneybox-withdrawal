using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.Features;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyBox.UnitTests
{
    [TestClass]
    public class TransferMoneyTests
    {
        [TestMethod]
        public void Execute_WithdrawDeposit_FromAccountUpdated()
        {
            var fromAccount = new Account()
            {
                Id = Guid.NewGuid(),
                Balance = 1000m
            };

            var toAccount = new Account()
            {
                Id = Guid.NewGuid(),
                Balance = 0m
            };

            var mock = MockingHelper.GetAccountRepositoryMock(
                new List<Account>()
                {
                    fromAccount, toAccount
                });

            var transfer = new TransferMoney(mock.Object);

            transfer.Execute(fromAccount.Id, toAccount.Id, 100);

            mock.Verify(m => m.Update(It.Is<Account>(a =>
            a.Id.Equals(fromAccount.Id)
            && a.Balance == 900m)),
            Times.AtLeastOnce);

        }
    }
}
