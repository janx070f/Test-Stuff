using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Test_Stuff.Models
{
    public static class SendTipMail
    {
            public static void SendEmails(string sender, string reciever, string subject, string name, string message)
            {
                SendEmail("kontakt@greencollaboration.dk", reciever, "Tip modtaget", name, message,
                    "<h2>Hej " + name + "</h2>" +
                    "<br/>" +
                    "<br/>" +
                    "<h3>Dit tip: </h3>" +
                    "<br/>" +
                    "<p>\"" + message + "\"</p>" +
                    "<p>Er blevet modtaget!</p>" +
                    "<br/>" +
                    "<br/>" +
                    "<p>Du vil blive kontaktet på: " + reciever + " hvis vi har spørgsmål med hensyn til dit tip </p> " +
                    "<br/> " +
                    "<p><i>Du kan kontakte os på <a href=\"mailto:kontakt@greencollaboration.dk\"</i></p> " +
                    "<br/>" +
                    "<h2>Med Venlig Hisen<br/>GreenCollaboration</h2>"
                    );

                SendEmail("kontakt@greencollaboration.dk", "janx070f@gmail.com", "Nyt tip fra " + name, name, message,
                    "<h2>Nyt tip fra " + name + "</h2>" +
                    "<br/>" +
                    "<br/>" +
                    "<h3>Deres tip: </h3>" +
                    "<br/>" +
                    "<p>" + message + "</p>" +
                    "<br/>" +
                    "<br/>" +
                    "<p>De har angivet deres kontaktemail til: " + "<a href=\"" + reciever + "\">" + reciever + "</a></p> "
                    );
            }

            public static void SendEmail(string sender, string reciever, string subject, string name, string message, string body)
            {
                bool success = false;
                try
                {
                    using (var mail = new MailMessage(new MailAddress(sender), new MailAddress(reciever)))
                    {
                        SmtpClient smtp = new SmtpClient("10.138.22.47"); //Ip til smtp eller smtp server
                        mail.From = new MailAddress(sender);
                        mail.Subject = subject;
                        mail.IsBodyHtml = true;
                        mail.Body = body;
                        smtp.Send(mail);
                    }
                }
                catch (Exception MailFailed)
                {
                    var dump = MailFailed;
                }
            }
        
    }
}