using ragther.business.Concrete.MailUpdate;
using ragther.business.Constants;
using ragther.Core.Utilities.Results;
using System;
using System.Net;
using System.Net.Mail;

namespace ragther.business.Helpers
{
    public static class MailHelper
    {
        public static IResult SendEmail(string emailAdrress, string message, string subject)
        {
            MailMessage msg = new MailMessage(); //Mesaj gövdesini tanımlıyoruz...
            msg.Subject = subject;
            msg.From = new MailAddress("ragtherinfo@gmail.com", "Ragther Info");
            msg.To.Add(new MailAddress(emailAdrress));

            msg.IsBodyHtml = true;
            msg.Body = message;

            msg.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential AccountInfo = new NetworkCredential("ragtherinfo@gmail.com", "123456789.Tr");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(msg);
                return new SuccessResult(Messages.EmailSended);
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
                
            }

        }
    }
}