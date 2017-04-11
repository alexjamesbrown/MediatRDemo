using System.Threading.Tasks;
using CashJobSite.Application.Features.AddJobApplication.Notifications;
using CashJobSite.Application.Features.GetJobById;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.AddJobApplication
{
    public class AddJobApplicationCommandHandler : IAsyncRequestHandler<AddJobApplicationCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly ICashJobSiteDbContext _dbContext;

        public AddJobApplicationCommandHandler(IMediator mediator, ICashJobSiteDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
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

            await _mediator
                .Publish(new JobApplicationSentNotification(
                    job,
                    message.CandidateName,
                    message.CandidateEmail,
                    message.CandidateInfo)
                );

            return Unit.Value;
        }
    }
}
