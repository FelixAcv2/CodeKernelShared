using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Mail;

namespace Acv2.SharedKernel.Crosscutting.Core.Email
{
   public class Message
    {

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => MailboxAddress.Parse(x))); //new MailboxAddress(x)
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }


    }
}
