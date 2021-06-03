using System;
using System.Net;
using System.Net.Mail;

namespace Core.CrossCuttingConcerns.Mailing
{
    public static class EmailTool
    {
        public static void Sent(Email email, Credentials credentials)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                message.From = new MailAddress(email.From);
                message.To.Add(new MailAddress(email.To));
                message.Subject = email.Subject;
                message.IsBodyHtml = email.IsBodyHtml;
                message.Body = email.Body;

                smtpClient.Port = (int) credentials.Port;
                smtpClient.Host = credentials.Host;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(credentials.EmailAddress, credentials.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}