using Moneybox.App.DataAccess;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository accountRepository;
        public WithdrawMoney(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {

            var from = this.accountRepository.GetAccountById(fromAccountId);

            from.Withdraw(amount);
            this.accountRepository.Update(from);
        }
    }
}
