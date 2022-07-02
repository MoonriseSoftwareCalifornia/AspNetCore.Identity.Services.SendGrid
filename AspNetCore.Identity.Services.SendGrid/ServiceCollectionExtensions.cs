
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
        public static void AddSendGridEmailProvider(this IServiceCollection services, SendGridEmailProviderOptions sendGridOptions)
        {
            services.AddOptions<SendGridEmailProviderOptions>().Configure(
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

            services.TryAddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
        }

    }
}