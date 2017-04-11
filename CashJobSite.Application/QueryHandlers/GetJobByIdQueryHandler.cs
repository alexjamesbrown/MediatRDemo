using System.Linq;
using CashJobSite.Application.Queries;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.QueryHandlers
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, Job>
    {
        private readonly ICashJobSiteDbContext _dbContext;

        public GetJobByIdQueryHandler(ICashJobSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Job Handle(GetJobByIdQuery message)
        {
            return _dbContext.Jobs
                .SingleOrDefault(x => x.Id == message.Id);
        }
    }
}
