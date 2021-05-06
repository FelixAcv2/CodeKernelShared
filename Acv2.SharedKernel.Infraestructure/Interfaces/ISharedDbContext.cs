using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.SharedKernel.Infraestructure.Interfaces
{
   public interface ISharedDbContext<EntityContext>: IQueryableUnitOfWork
    {

    }
}
