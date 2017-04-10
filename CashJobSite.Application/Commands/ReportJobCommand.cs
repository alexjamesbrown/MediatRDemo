using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CashJobSite.Application.Commands
{
    public class ReportJobCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }

        public ReportJobCommand(int id, string ipAddress)
        {
            Id = id;
            IpAddress = ipAddress;
        }
    }
}
