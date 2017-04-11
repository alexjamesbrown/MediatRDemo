using System.Collections.Generic;
using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.FindAllJobs
{
    public class FindAllJobsQuery : IRequest<IEnumerable<Job>>
    {
    }
}
