using System.Net.Mail;

namespace EventsAndInvitesLogic
{
    public static class EmailLogic
    {

        public static void SendEmail(string to, string subject, string body)
        {
            MailAddress fromMailAddress = new MailAddress("bnzmarkets_events@bnz.co.nz");

            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Bcc.Add(fromMailAddress);
            mail.From = fromMailAddress;
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient();

            //client.Send(mail);
        }
    }
}
