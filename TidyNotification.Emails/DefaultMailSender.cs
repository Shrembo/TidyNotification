using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using TidyNotification.Emails.Abstractions;
using TidyNotification.Emails.Options;

namespace TidyNotification.Emails
{
    public class DefaultMailSender : IMailSender
    {
        private readonly SmtpOptions options;

        public DefaultMailSender(IOptions<SmtpOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendAsync(IMailContentBuilder contentBuilder)
        {
            var mailDefinition = contentBuilder.Build();
            var message = new MimeMessage();

            if (mailDefinition.ToAddresses == null || !mailDefinition.ToAddresses.Any())
                throw new System.Exception("Email must contains one destination at least!");

            //Add mail source
            message.From.Add(new MailboxAddress(options.Sender.Name, options.Sender.Address));

            //Add mail destination 
            message.To.AddRange(mailDefinition.ToAddresses.Select(a => new MailboxAddress(a.Name, a.Address)));

            //add CC if exists
            if (mailDefinition.CcAddresses != null && mailDefinition.CcAddresses.Any())
                message.Cc.AddRange(mailDefinition.CcAddresses.Select(a => new MailboxAddress(a.Name, a.Address)));


            //add BCC if exists
            if (mailDefinition.BccAddresses != null && mailDefinition.BccAddresses.Any())
                message.Bcc.AddRange(mailDefinition.BccAddresses.Select(a => new MailboxAddress(a.Name, a.Address)));


            message.Subject = mailDefinition.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = mailDefinition.Content };

            using var client = new SmtpClient();
            await client.ConnectAsync(options.Server, options.Port, options.EnableSsl);
            await AuthenticatedData(client);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        public virtual async Task AuthenticatedData(SmtpClient smtpClient)
        {
            if (string.IsNullOrWhiteSpace(options.Username) || string.IsNullOrWhiteSpace(options.Password))
                throw new System.Exception("Email Sender. must provide username and password for SSL connection!");

            smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
            await smtpClient.AuthenticateAsync(options.Username, options.Password);
        }
    }
}
