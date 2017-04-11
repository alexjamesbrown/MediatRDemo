using CashJobSite.Application.Notifications;
using CashJobSite.Application.Services;
using MediatR;

namespace CashJobSite.Application.NotificationHandlers.JobApplicationSent
{
    public class SendEmailToBoss : INotificationHandler<JobApplicationSentNotification>
    {
        public void Handle(JobApplicationSentNotification notification)
        {

        }
    }
}
