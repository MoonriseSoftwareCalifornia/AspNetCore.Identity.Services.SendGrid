
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
        /// <summary>
        /// Adds the SendGrid Email Provider to the services collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="sendGridOptions"></param>
        public static void AddSendGridEmailProvider(this IServiceCollection services, SendGridEmailProviderOptions sendGridOptions)
        {
            services.TryAddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();

            services.Configure<SendGridEmailProviderOptions>(
                configureOptions: options =>
                {
                    options.ApiKey = sendGridOptions.ApiKey;
                    options.Auth = sendGridOptions.Auth;
                    options.UrlPath = sendGridOptions.UrlPath;
                    options.DefaultFromEmailAddress = sendGridOptions.DefaultFromEmailAddress;
                    options.HttpErrorAsException = sendGridOptions.HttpErrorAsException;
                    options.ReliabilitySettings = sendGridOptions.ReliabilitySettings;
                    options.RequestHeaders = sendGridOptions.RequestHeaders;
                    options.SandboxMode = sendGridOptions.SandboxMode;
                    options.Version = sendGridOptions.Version;
                });
        }

    }
}