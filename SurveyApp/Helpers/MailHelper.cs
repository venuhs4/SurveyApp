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
                SmtpClient SmtpServer = new SmtpClient(); // "smtp.gmail.com");
                SmtpServer.Host = "relay-hosting.secureserver.net";
                SmtpServer.Port = 25;

                mail.From = new MailAddress("survey@primum.mobi");
                mail.To.Add(toMail);
                mail.Subject = subject;
                mail.Body = text;

                //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                //SmtpServer.Port = 587;
                //SmtpServer.Timeout = 120;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("SmartPropz@gmail.com", "SurveyMania77");
                //SmtpServer.UseDefaultCredentials = true;
                //SmtpServer.EnableSsl = true;

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