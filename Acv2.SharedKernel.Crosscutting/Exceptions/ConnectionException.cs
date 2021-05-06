using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException()
        {

        }

        public ConnectionException(string message)
            : base(message)
        { }

        public ConnectionException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

}
