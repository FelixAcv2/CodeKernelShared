using System;
using System.Collections.Generic;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.Email
{
    public interface IMessageEmail
    {

        string Host { get; set; }
        string From { get; set; }
        string Subject { get; set; }

        bool SendEmail(string user, string name, string email, string comment);

    }

}
