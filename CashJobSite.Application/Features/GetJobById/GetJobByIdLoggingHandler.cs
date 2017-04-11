using System.Diagnostics;
using System.Threading.Tasks;
using CashJobSite.Application.Logging;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.GetJobById
{
    public class GetJobByIdLoggingHandler : IPipelineBehavior<GetJobByIdQuery, Job>
    {
        private readonly ILogger _logger;

        public GetJobByIdLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<Job> Handle(GetJobByIdQuery request, RequestHandlerDelegate<Job> next)
        {
            _logger.Info($"Getting job with id {request.Id}");

            var stopwatch = Stopwatch.StartNew();

            var result = await next();

            stopwatch.Stop();

            _logger.Info($"Retrieving job with id {request.Id} took {stopwatch.ElapsedMilliseconds}ms");

            return result;
        }
    }
}
