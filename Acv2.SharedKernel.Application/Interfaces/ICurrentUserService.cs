using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Application.Interfaces
{
  public  interface ICurrentUserService
    {

        string UserId { get; }

        bool IsAuthenticated { get; }
    }
}
