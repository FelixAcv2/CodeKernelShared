using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Application.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
