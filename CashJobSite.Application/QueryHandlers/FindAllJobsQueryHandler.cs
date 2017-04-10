using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Queries;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.QueryHandlers
{
    public class FindAllJobsQueryHandler:IRequestHandler<FindAllJobsQuery, IEnumerable<Job>>
    {
        private readonly ICashJobSiteDbContext _dbContext;
        private readonly ILogger _logger;

        public FindAllJobsQueryHandler(ICashJobSiteDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<Job> Handle(FindAllJobsQuery message)
        {
            _logger.Info("Finding all jobs");

            var result = _dbContext.Jobs
                .OrderByDescending(x => x.Created)
                .ToList();

            _logger.Info($"Found {result.Count()} jobs");

            return result;
        }
    }
}
