using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CO5027
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

            // fetch data from form

            string customerName = txtName.Text;
            string customerEmailAddress = txtEmail.Text;
            string customerMessage = txtMessage.Text;

            // format email for admin

            string emailToAdmin = "Email Sumbitted:" + Environment.NewLine;
            emailToAdmin += "FROM: " + customerName + Environment.NewLine;
            emailToAdmin += "REPLY TO: " + customerEmailAddress + Environment.NewLine;
            emailToAdmin += Environment.NewLine;
            emailToAdmin += "MESSAGE:" + Environment.NewLine;
            emailToAdmin += customerMessage + Environment.NewLine;
            emailToAdmin += Environment.NewLine;
            emailToAdmin += "Message sent though StunningSnaps website";

            string subjectToAdmin = "New message from: " + customerName;

            // Send email to admin
            if (!sendEmail("stunningsnaps@wilk.tech", customerEmailAddress, subjectToAdmin, emailToAdmin))
            {
                litResponseMessage.Text = "Error sending message.  Please wait for a miniute before trying again.";
            }


            // format email for customer

            string emailToCustomer = customerName + " thank you for your recient correspondence with StunningSnaps." + Environment.NewLine;
            emailToCustomer += Environment.NewLine;
            emailToCustomer += "For your records we have included a copy of the message you sent us. We aim to respond to all messages within 48 hours." + Environment.NewLine;
            emailToCustomer += Environment.NewLine;
            emailToCustomer += "Your message:" + Environment.NewLine;
            emailToCustomer += customerMessage + Environment.NewLine;
            emailToCustomer += Environment.NewLine;
            emailToCustomer += "Message sent though StunningSnaps website" + Environment.NewLine;
            emailToCustomer += "http://1417800.studentwebserver.co.uk/CO5027";

            // Send email to customer
            if (!sendEmail(customerEmailAddress, "stunningsnaps@wilk.tech", "Message sent to StunningSnaps", emailToCustomer))
            {
                litResponseMessage.Text = "Message sent to StunningSnaps.  Unable to copy message for your records, please make a note of your message.";
            }

            // Update page to reflect successful sending of email
            pnlContactForm.Visible = false;
            litResponseMessage.Text = "Message successfully sent.  You should recieve a copy in your inbox for your records.";

        }

        private bool sendEmail(string recipient, string replyTo, string emailSubject, string emailBody)
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