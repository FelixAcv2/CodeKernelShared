using Acv2.Sharedkernel.Domain;
using Acv2.SharedKernel.Application.Core;
using Acv2.SharedKernel.Application.Core.Functionals;
using Acv2.SharedKernel.Crosscutting.Core.Logging;
using Acv2.SharedKernel.Crosscutting.Core.Validator;
using Application.Services.CustomerServices.Commands.Command;
using Infraescructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CustomerServices.Commands.Handler
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Result>
    {
        private readonly CustomerDbContext _customerDbContext;

        public CreateCommandHandler(CustomerDbContext customerDbContext)
        {
            _customerDbContext = customerDbContext;
        }

        public async Task<Result> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var _rowAfect = false;

            try
            {
                if (request.Customer is null)
                {
                    return Result.Fail<string>($"Customer Cannot Be Null");
                }

                var _entity = request.Customer;
                //_entity.Enable();

                await AddCustomer(_entity);
                _rowAfect = true;
                return Result.Ok<bool>(_rowAfect);
            }
            catch (Exception ex)
            {

                return Result.Fail<string>(ex.Message);
            }
        }


        private async Task AddCustomer(Customer customer)
        {
            try
            {
                ////var entityValidator = EntityValidatorFactory.CreateValidator();

               //// if (entityValidator.IsValid(customer)) //if entity is valid save. t
               // {

                     _customerDbContext.Customers.Add(customer);
                    await _customerDbContext.CommitAsync("FelixAcevedo");

               // }
               // else // if not valid throw validation errors
                  //  throw new ApplicationValidationErrorsException(entityValidator.GetInvalidMessages(customer));
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError(ex.Source, ex);
                throw ex;
            }
        }
    
    }
}
