using TidyNotification.Emails.Models;

namespace TidyNotification.Emails.Abstractions
{
    public interface IMailContentBuilder
    {
        MailDefinition Build();
    }
}
