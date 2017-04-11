using CashJobSite.Application.Notifications;
using CashJobSite.Application.Services;
using MediatR;

namespace CashJobSite.Application.NotificationHandlers.JobApplicationSent
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
            var emailBody = "You have sent a application for your job #" + notification.Job.Id;

            _emailService.SendEmail(notification.CandidateEmail, emailSubject, emailBody);
        }
    }
}