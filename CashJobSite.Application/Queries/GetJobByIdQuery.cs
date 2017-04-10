using CashJobSite.Models;
using MediatR;

namespace CashJobSite.Application.Queries
{
    public class GetJobByIdQuery : IRequest<Job>
    {
        public GetJobByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
