using TidyNotification.Emails.Models;

namespace TidyNotification.Emails.Options
{
    public class SmtpOptions
    {
        public MailAddress Sender { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
