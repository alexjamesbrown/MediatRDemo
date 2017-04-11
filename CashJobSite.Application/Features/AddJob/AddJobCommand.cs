using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Features.AddJob
{
    public class AddJobCommand : IRequest<Job>
    {
        public AddJobCommand(Job job)
        {
            Job = job;
        }

        public Job Job { get; set; }
    }
}
