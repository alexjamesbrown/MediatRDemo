using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashJobSite.Application.Commands;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Queries;
using CashJobSite.Application.Services;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.CommandHandlers
{
    public class AddJobApplicationCommandHandler : IAsyncRequestHandler<AddJobApplicationCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly ICashJobSiteDbContext _dbContext;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public AddJobApplicationCommandHandler(IMediator mediator, ICashJobSiteDbContext dbContext, IEmailService emailService, ILogger logger)
        {
            _mediator = mediator;
            _dbContext = dbContext;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(AddJobApplicationCommand message)
        {
            var job = await _mediator.Send(new GetJobByIdQuery(message.JobId));

            var jobApplication = new JobApplication
            {
                CandidateName = message.CandidateName,
                CandidateEmail = message.CandidateEmail,
                CandidateInfo = message.CandidateInfo,
                Job = job
            };

            _dbContext.JobApplications.Add(jobApplication);
            _dbContext.SaveChanges();

            var emailSubject = "Job application received.";
            var emailBody = "You have a new application for your job #" + job.Id + "\n" +
                            "Name: " + message.CandidateName + "\n" +
                            "Email: " + message.CandidateEmail + "\n" +
                            "Info: " + message.CandidateInfo + "\n";

            _emailService.SendEmail(job.BossEmail, emailSubject, emailBody);

            _logger.Debug("Email Sent");

            return Unit.Value;
        }
    }
}
