using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acv2.SharedKernel.Crosscutting.Core.Sms
{
  public  interface IMessageSms
    {  
        Task<string> SendSmsAsync(string message, string destine, string source = "+16193321063");

        Task<string> SendSmsAsync(string message, string destine);


    }
}
