using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;
        public const decimal InsufficientFundsLimit = 0m;
        public const decimal LowFundsLimit = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public void Deposit(decimal amount)
        {

            var paidIn = this.PaidIn + amount;

            if (paidIn > PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (PayInLimit - paidIn < LowFundsLimit)
            {
                this.User.ApproachingPayInLimitNotification();
            }

            //update

            this.Balance += amount;
            this.PaidIn = paidIn;
        }

        public void Withdraw(decimal amount)
        {

            var balance = this.Balance - amount;

            if (balance < InsufficientFundsLimit)
            {
                throw new InvalidOperationException("");
            }

            if (balance < LowFundsLimit)
            {
                this.User.FundsLowNotification();
            }

            // update 

            this.Balance = balance;
            this.Withdrawn -= amount;
        }
    }
}
