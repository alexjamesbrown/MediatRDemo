using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Commands
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
