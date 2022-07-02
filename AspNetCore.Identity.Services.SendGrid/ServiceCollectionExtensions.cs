
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SendGrid;
using System;

namespace AspNetCore.Identity.Services.SendGrid.Extensions
{
    /// <summary>
    /// extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void AddSendGridEmailProvider(this IServiceCollection services, Action<IServiceProvider, SendGridEmailProviderOptions> configureOptions)
        {
            services.AddOptions<SendGridEmailProviderOptions>().Configure<IServiceProvider>((options, resolver) => configureOptions(resolver, options))
                .PostConfigure(options =>
                {
                    // validation
                    if (string.IsNullOrWhiteSpace(options.ApiKey))
                    {
                        throw new ArgumentNullException(nameof(options.ApiKey));
                    }
                });

            services.TryAddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
        }

    }
}