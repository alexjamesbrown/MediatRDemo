using System.Threading.Tasks;
using CashJobSite.Application.Logging;
using MediatR;

namespace CashJobSite.Application.Features.AddJob
{
    public class AddJobLoggingHandler : IPipelineBehavior<AddJobCommand, Unit>
    {
        private readonly ILogger _logger;

        public AddJobLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(AddJobCommand request, RequestHandlerDelegate<Unit> next)
        {
            _logger.Info("Saving job");

            var response = await next();

            _logger.Info("Job saved");

            return response;
        }
    }
}