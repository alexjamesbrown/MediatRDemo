using MediatR;

namespace CashJobSite.Application.Features.AddJobApplication.Notifications
{
    public class SendEmailToBoss : INotificationHandler<JobApplicationSentNotification>
    {
        public void Handle(JobApplicationSentNotification notification)
        {

        }
    }
}
