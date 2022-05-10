using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Acv2.SharedKernel.Crosscutting.Core.Sms
{
    public class MessageSmsTwilio : IMessageSms
    {
        protected readonly IConfiguration _config;

        public MessageSmsTwilio(IConfiguration config)
        {
            _config = config;
            InitialConnection();
        }

        public async Task<string> SendSmsAsync(string message,string destine, string source = "+16193321063")
        {
            return await Task.Run<string>(async () => {

                var _message = await MessageResource.CreateAsync(
                      body: message,
                      from: new Twilio.Types.PhoneNumber(destine),
                      to: new Twilio.Types.PhoneNumber(source)
                    );

                return _message.Sid;
            });
        }

        public async Task<string> SendSmsAsync(string message, string destine)
        {
            return await Task.Run<string>(async () => {

                var _message = await MessageResource.CreateAsync(
                      body: message,
                      from: new Twilio.Types.PhoneNumber("+16193321063"),
                      to: new Twilio.Types.PhoneNumber(destine)
                    );

                return _message.Sid;
            });
        }

        private void InitialConnection() {


            var _accountid = _config.GetSection("accountId").Value;
            var _authtoken = _config.GetSection("authToken").Value;

            TwilioClient.Init(_accountid, _authtoken);
        
        }

       
    }
}
