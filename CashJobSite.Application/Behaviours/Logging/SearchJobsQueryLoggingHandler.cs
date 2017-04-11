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
    public class SearchJobsQueryLoggingHandler : IPipelineBehavior<SearchJobsQuery, IEnumerable<Job>>
    {
        private readonly ILogger _logger;

        public SearchJobsQueryLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<Job>> Handle(SearchJobsQuery request, RequestHandlerDelegate<IEnumerable<Job>> next)
        {
            _logger.Info("Searching jobs");

            var stopwatch = Stopwatch.StartNew();
            var result = await next();
            stopwatch.Stop();

            _logger.Info($"Found {result.Count()} jobs in {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }
    }
}
