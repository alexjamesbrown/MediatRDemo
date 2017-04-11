using CashJobSite.Application.Notifications;
using MediatR;

namespace CashJobSite.Application.NotificationHandlers.JobApplicationSent
{
    public class SendEmailToApplicant : INotificationHandler<JobApplicationSentNotification>
    {
        public void Handle(JobApplicationSentNotification notification)
        {

        }
    }
}