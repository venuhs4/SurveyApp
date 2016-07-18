using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SurveyApp.Helpers
{
    public class MailHelper
    {
        public static bool SendMail(string toMail,string subject, string text)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("SmartPropz@gmail.com");
                mail.To.Add(toMail);
                mail.Subject = subject;
                mail.Body = text;

                
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("SmartPropz@gmail.com", "SurveyMania77");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}