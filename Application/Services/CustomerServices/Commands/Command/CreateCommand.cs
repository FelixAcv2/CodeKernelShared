using Acv2.Sharedkernel.Domain;
using Acv2.SharedKernel.Application.Core.Functionals;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CustomerServices.Commands.Command
{
    public class CreateCommand: IRequest<Result>
    {
        public Customer Customer { get; set; }
    }
}
