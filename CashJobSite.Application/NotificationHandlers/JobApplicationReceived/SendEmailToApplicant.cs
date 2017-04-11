using CashJobSite.Application.Notifications;
using CashJobSite.Application.Services;
using MediatR;

namespace CashJobSite.Application.NotificationHandlers.JobApplicationReceived
{
    public class SendEmailToApplicant : INotificationHandler<JobApplicationSentNotification>
    {
        private readonly IEmailService _emailService;

        public SendEmailToApplicant(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(JobApplicationSentNotification notification)
        {
            var emailSubject = "Job application sent.";
            var emailBody = "You applied for job id #" + notification.Job.Id;

            _emailService.SendEmail(notification.Job.BossEmail, emailSubject, emailBody);
        }
    }
}