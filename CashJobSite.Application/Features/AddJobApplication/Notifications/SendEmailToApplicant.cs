using MediatR;

namespace CashJobSite.Application.Features.AddJobApplication.Notifications
{
    public class SendEmailToApplicant : INotificationHandler<JobApplicationSentNotification>
    {
        public void Handle(JobApplicationSentNotification notification)
        {

        }
    }
}