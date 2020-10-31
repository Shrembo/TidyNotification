using System.Threading.Tasks;

namespace TidyNotification.Emails.Abstractions
{
    public interface IMailSender
    {
        Task SendAsync(IMailContentBuilder contentBuilder);
    }
}
