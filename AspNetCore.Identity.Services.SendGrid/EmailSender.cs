using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AspNetCore.Identity.Services.SendGrid
{

    /// <summary>
    ///     SendGrid Email sender service
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SendGridEmailProviderOptions> _options;
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options"></param>
        public EmailSender(IOptions<SendGridEmailProviderOptions> options)
        {
            _options = options;
        }

        /// <summary>
        ///     Email response from SendGrid
        /// </summary>
        public Response? Response { get; private set; }

        /// <summary>
        ///     Send email method
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(subject, message, email);
        }

        /// <summary>
        ///     Execute send email method
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="email"></param>
        /// <param name="emailFrom"></param>
        /// <returns></returns>
        private Task Execute(string subject, string message, string email, string? emailFrom = null)
        {
            var client = new SendGridClient(_options.Value.ApiKey);

            var msg = new SendGridMessage
            {
                From = new EmailAddress(emailFrom ?? _options.Value.DefaultFromEmailAddress),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            if (_options.Value.SandboxMode)
            {
                msg.MailSettings.SandboxMode.Enable = true;
            } 
            
            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(true, true);

            Response = client.SendEmailAsync(msg).Result;

            return Task.CompletedTask;
        }
    }
}
