using CashJobSite.Application.Services;
using MediatR;

namespace CashJobSite.Application.Features.AddJobApplication.Notifications
{
    public class SendEmailToBoss : INotificationHandler<JobApplicationSentNotification>
    {
        private readonly IEmailService _emailService;

        public SendEmailToBoss(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Handle(JobApplicationSentNotification notification)
        {
            var emailSubject = "Job application received.";
            var emailBody = "You have a new application for your job #" + notification.Job.Id + "\n" +
                            "Name: " + notification.CandidateName + "\n" +
                            "Email: " + notification.CandidateEmail + "\n" +
                            "Info: " + notification.CandidateInfo + "\n";

            _emailService.SendEmail(notification.Job.BossEmail, emailSubject, emailBody);
        }
    }
}
