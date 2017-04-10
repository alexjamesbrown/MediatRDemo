using System.Linq;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Queries;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.QueryHandlers
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Job>
    {
        private readonly ICashJobSiteDbContext _dbContext;
        private readonly ILogger _logger;

        public GetJobByIdQueryHandler(ICashJobSiteDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Job Handle(GetJobByIdQuery message)
        {
            _logger.Info("Getting job with id " + message.Id);
            return _dbContext.Jobs
                .SingleOrDefault(x => x.Id == message.Id);
        }
    }
}
