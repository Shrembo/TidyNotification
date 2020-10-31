using Microsoft.Extensions.Configuration;
using TidyNotification.Emails;
using TidyNotification.Emails.Abstractions;
using TidyNotification.Emails.Options;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpOptions>(options => configuration.GetSection(nameof(SmtpOptions)).Bind(options));
            services.AddScoped<IMailSender, DefaultMailSender>();

            return services;
        }
    }
}
