using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AspNetCore.Identity.Services.SendGrid
{

    /// <summary>
    ///     SendGrid Email sender service
    /// </summary>
    public class SendGridEmailSender : IEmailSender
    {
        private readonly IOptions<SendGridEmailProviderOptions> _options;
        private readonly ILogger<SendGridEmailSender> _logger;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options"></param>
        public SendGridEmailSender(IOptions<SendGridEmailProviderOptions> options, ILogger<SendGridEmailSender> logger)
        {
            _options = options;
            _logger = logger;
        }

        /// <summary>
        /// Indicates if client is in SendGrid <see href="https://docs.sendgrid.com/for-developers/sending-email/sandbox-mode">sandbox</see> mode.
        /// </summary>
        public bool SandboxMode { get { return _options.Value.SandboxMode; } }

        /// <inheritdoc/>
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
            var client = new SendGridClient(_options.Value);

            var msg = new SendGridMessage
            {
                From = new EmailAddress(emailFrom ?? _options.Value.DefaultFromEmailAddress),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(true, true);

            // Set the Sandbox mode if on.
            if (_options.Value.SandboxMode)
            {
                msg.SetSandBoxMode(true);
            }

            try
            {
                Response = client.SendEmailAsync(msg).Result;

                if (Response.IsSuccessStatusCode && _options.Value.LogSuccesses)
                {
                    _logger.LogInformation($"Email successfully sent to: {email}; Subject: {subject};");
                }

                if (!Response.IsSuccessStatusCode && _options.Value.LogErrors)
                {
                    _logger.LogError(new Exception($"SendGrid status code: {Response.StatusCode}"), Response.Headers.ToString());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            

            return Task.CompletedTask;
        }
    }
}
