using System.Collections.Generic;
using System.Linq;
using CashJobSite.Application.Queries;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.QueryHandlers
{
    public class FindAllJobsQueryHandler:IRequestHandler<FindAllJobsQuery, IEnumerable<Job>>
    {
        private readonly ICashJobSiteDbContext _dbContext;

        public FindAllJobsQueryHandler(ICashJobSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Job> Handle(FindAllJobsQuery message)
        {
            var result = _dbContext.Jobs
                .OrderByDescending(x => x.Created)
                .ToList();

            return result;
        }
    }
}
