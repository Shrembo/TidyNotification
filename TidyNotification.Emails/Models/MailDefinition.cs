using System.Collections.Generic;

namespace TidyNotification.Emails.Models
{
    public class MailDefinition
    {
        public MailDefinition()
        {
            ToAddresses = new List<MailAddress>();
        }

        public List<MailAddress> ToAddresses { get; set; }
        public List<MailAddress> CcAddresses { get; set; }
        public List<MailAddress> BccAddresses { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
