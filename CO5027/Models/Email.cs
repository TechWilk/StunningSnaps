using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CO5027.Models
{
    public class Email
    {
        public static bool sendEmail(string recipient, string replyTo, string emailSubject, string emailBody)
        {
            SmtpClient client = new SmtpClient();

            client.EnableSsl = true;
            client.Host = "petyrbaelish.asoshared.com";
            client.Port = 587;

            // Webmail can be accessed using the credentials below at https://wilk.tech/mail
            NetworkCredential credentials = new NetworkCredential("stunningsnaps@wilk.tech", "h$GCA@Vp1(v#");
            client.Credentials = credentials;

            MailMessage msg = new MailMessage("stunningsnaps@wilk.tech", recipient);

            MailAddressCollection replyToAddresses = new MailAddressCollection();
            MailAddress replyToMailAddress = new MailAddress(replyTo);

            msg.ReplyToList.Add(replyToMailAddress);

            msg.Body = emailBody;
            msg.Subject = emailSubject;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return false;
            }


            return true;
        }
    }
}