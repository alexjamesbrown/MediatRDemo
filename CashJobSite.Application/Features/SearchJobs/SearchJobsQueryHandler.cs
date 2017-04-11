using System.Collections.Generic;
using System.Linq;
using CashJobSite.Data;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.SearchJobs
{
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, IEnumerable<Job>>
    {
        private readonly ICashJobSiteDbContext _dbContext;

        public SearchJobsQueryHandler(ICashJobSiteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Job> Handle(SearchJobsQuery message)
        {
            IEnumerable<Job> result;

            if (string.IsNullOrEmpty(message.Title))
            {
                result = _dbContext.Jobs.Where(job => job.Cash >= message.Cash)
                    .ToList();
            }
            else
            {
                result = _dbContext.Jobs.Where(job => job.Title.StartsWith(message.Title) && job.Cash >= message.Cash)
                    .ToList();
            }

            return result;
        }
    }
}
