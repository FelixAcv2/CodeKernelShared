using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Acv2.SharedKernel.Crosscutting.Core.Email
{
    public class MessageEmail : IMessageEmail
    {
        SmtpClient _client;
        private MailMessage MailMessage { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public MessageEmail(SmtpClient client)
        {
            _client = client; // new SmtpClient();
        }

        public string Host { get; set; }

        public bool Email(string from, string to, string subject, string body)
        {
            bool _IsSend = false;
            MailMessage _mailMessage = new MailMessage();
            try
            {

                _mailMessage.From = new MailAddress(from);
                _mailMessage.To.Add(to);
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = body;
                _mailMessage.Subject = subject;
                _mailMessage.Priority = MailPriority.High;
                _client.Send(_mailMessage);
                return this.Send(_mailMessage);
            }
            catch (Exception)
            {
                _IsSend = false;
                throw;
            }
            finally
            {
                _mailMessage.Dispose();

            }



        }




        private bool Send(MailMessage mailMessage)
        {

            bool _IsSend = false;

            try
            {
                _client.Host = Host;

                MailMessage = mailMessage;
                _client.Send(mailMessage);

                _IsSend = true;
            }
            catch (Exception)
            {
                _IsSend = false;
                throw;
            }
            finally
            {
                mailMessage.Dispose();

            }

            return _IsSend;

        }


        public bool SendEmail(string user, string name, string email, string comment)
        {
            bool _IsSend = false;
            MailMessage _mailMessage = new MailMessage();
            try
            {
                string _style = string.Format("Style='Width:80%; border-style:solid; border-color:Green; border-radius:15px; padding:10px;'");
                string _body = string.Format("<div {0}> <b>Solicitud: {1}</b>{2} Socio: {3}{4}{5}{6}Enviado por: {7}</div>", _style, user, "<br />", name, "<br /><br />", comment, "<br /><br />", user);



                _mailMessage.From = new MailAddress(From);
                _mailMessage.To.Add(email);
                _mailMessage.IsBodyHtml = true;
                _mailMessage.Body = _body;
                _mailMessage.Subject = Subject;
                _mailMessage.Priority = MailPriority.High;
                //_client.Send(_mailMessage);
                return this.Send(_mailMessage);
            }
            catch (Exception)
            {
                _IsSend = false;
                throw;
            }
            finally
            {
                _mailMessage.Dispose();

            }
            
        }


        
    }

}
