using System.Collections.Generic;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.SearchJobs
{
    public class SearchJobsQuery:IRequest<IEnumerable<Job>>
    {
        public SearchJobsQuery(string title, int cash)
        {
            Title = title;
            Cash = cash;
        }

        public string Title { get; set; }

        public int Cash { get; set; }
    }
}