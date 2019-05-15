using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App
{
    public class User
    {
        private readonly INotificationService notificationService;

        public User(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public void FundsLowNotification()
        {
            this.notificationService.NotifyFundsLow(this.Email);
        }

        public void ApproachingPayInLimitNotification()
        {
            this.notificationService.NotifyApproachingPayInLimit(this.Email);
        }
    }
}
