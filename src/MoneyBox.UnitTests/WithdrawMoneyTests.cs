using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.Features;
using Moq;
using System;
using System.Collections.Generic;

namespace MoneyBox.UnitTests
{

    [TestClass]
    public class WithdrawMoneyTests
    {

        [TestMethod]
        public void Execute_Withdraw_FromAccountUpdated()
        {
            var account = new Account()
            {
                Id = Guid.NewGuid(),
                Balance = 1000m
            };

            var mock = MockingHelper.GetAccountRepositoryMock(
                new List<Account>() {
                    account
                });

            var withdraw = new WithdrawMoney(mock.Object);
            withdraw.Execute(account.Id, 100m);

            mock.Verify(m =>
            m.Update(It.Is<Account>(a =>
             a.Id.Equals(account.Id)
             && a.Balance == 900m)), Times.AtLeastOnce);

        }
    }
}
