using System;
using CashJobSite.Application.Commands;
using CashJobSite.Application.Logging;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.CommandHandlers
{
    public class AddJobCommandHandler : IRequestHandler<AddJobCommand, Job>
    {
        private readonly ICashJobSiteDbContext _dbContext;
        private readonly ILogger _logger;

        public AddJobCommandHandler(ICashJobSiteDbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Job Handle(AddJobCommand message)
        {
            try
            {
                _logger.Info("Saving job");
                _dbContext.Jobs.Add(message.Job);
                _dbContext.SaveChanges();
                _logger.Info("Job saved");
                return message.Job;
            }
            catch (Exception ex)
            {
                _logger.Error("Error saving job - " + ex.Message);
                throw;
            }
        }
    }
}
