using Moneybox.App.DataAccess;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository accountRepository;
        public TransferMoney(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {

            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            from.Withdraw(amount);
            to.Deposit(amount);

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
