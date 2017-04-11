using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Notifications
{
    public class JobApplicationSentNotification : INotification
    {
        public Job Job { get; set; }
        public string CandidateName { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidateInfo { get; set; }

        public JobApplicationSentNotification(Job job, string candidateName, string candidateEmail, string candidateInfo)
        {
            Job = job;
            CandidateName = candidateName;
            CandidateEmail = candidateEmail;
            CandidateInfo = candidateInfo;
        }
    }
}
