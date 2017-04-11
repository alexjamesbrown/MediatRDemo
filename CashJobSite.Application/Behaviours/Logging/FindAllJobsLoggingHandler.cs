using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CashJobSite.Application.Logging;
using CashJobSite.Application.Queries;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Behaviours.Logging
{
    public class FindAllJobsLoggingHandler : IPipelineBehavior<FindAllJobsQuery, IEnumerable<Job>>
    {
        private readonly ILogger _logger;

        public FindAllJobsLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<Job>> Handle(FindAllJobsQuery request, RequestHandlerDelegate<IEnumerable<Job>> next)
        {
            _logger.Info("Finding all jobs");

            var stopwatch = Stopwatch.StartNew();
            var response = await next();
            stopwatch.Stop();
            
            _logger.Info($"Found {response.Count()} jobs in {stopwatch.ElapsedMilliseconds}ms");

            return response;
        }
    }
}