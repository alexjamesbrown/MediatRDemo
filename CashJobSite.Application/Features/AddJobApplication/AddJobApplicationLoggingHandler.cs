using System.Threading.Tasks;
using CashJobSite.Application.Logging;
using MediatR;

namespace CashJobSite.Application.Features.AddJobApplication
{
    public class AddJobApplicationLoggingHandler : IPipelineBehavior<AddJobApplicationCommand, Unit>
    {
        private readonly ILogger _logger;

        public AddJobApplicationLoggingHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<Unit> Handle(AddJobApplicationCommand request, RequestHandlerDelegate<Unit> next)
        {
            _logger.Debug("Starting to send job application");

            var response = await next();

            _logger.Debug("Job application Sent");

            return response;
        }
    }
}