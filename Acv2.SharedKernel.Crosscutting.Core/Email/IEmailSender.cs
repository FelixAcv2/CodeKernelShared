using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.SharedKernel.Crosscutting.Core.Email
{
  public  interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);


    }
}
