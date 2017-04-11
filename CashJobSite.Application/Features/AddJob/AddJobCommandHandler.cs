using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.AddJob
{
    public class AddJobCommandHandler : IRequestHandler<AddJobCommand, Job>
    {
        private readonly ICashJobSiteDbContext _dbContext;

        public AddJobCommandHandler(ICashJobSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Job Handle(AddJobCommand message)
        {
            _dbContext.Jobs.Add(message.Job);
            _dbContext.SaveChanges();
            return message.Job;
        }
    }
}
