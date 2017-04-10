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
    public class ReportJobCommandHandler : IAsyncRequestHandler<ReportJobCommand, Unit>
    {
        private readonly ICashJobSiteDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public ReportJobCommandHandler(ICashJobSiteDbContext dbContext, IMediator mediator, IEmailService emailService, ILogger logger)
        {
            _dbContext = dbContext;
            _mediator = mediator;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ReportJobCommand message)
        {
            var job = await _mediator.Send(new GetJobByIdQuery(message.Id));

            var emailSubject = "Job '" + job.Title + "' has been reported.";
            var emailBody = "Somebody has reported job #" + job.Id;

            _emailService.SendEmail("admin@CashJobSiteCashJobSite.com", emailSubject, emailBody);
            _logger.Debug("Email Sent");

            var jobReport = new JobReport { Job = job, ReporterIpAddress = message.IpAddress };

            _dbContext.JobReports.Add(jobReport);
            _dbContext.SaveChanges();

            _logger.Debug("Job report saved");

            return Unit.Value;
        }
    }
}
