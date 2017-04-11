using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace CashJobSite.Application.Features.AddJob
{
    public class AddJobValidationHandler : IRequestPreProcessor<AddJobCommand>
    {
        public Task Process(AddJobCommand request)
        {
            //probably use something like fluent validator here
            if (string.IsNullOrEmpty(request.Job.Title))
            {
                throw new ValidationException("Job title cannot be blank");
            }

            if (request.Job.Title.Length < 5)
            {
                throw new ValidationException("Job title too short");
            }

            return Task.FromResult(0);
        }
    }
}
