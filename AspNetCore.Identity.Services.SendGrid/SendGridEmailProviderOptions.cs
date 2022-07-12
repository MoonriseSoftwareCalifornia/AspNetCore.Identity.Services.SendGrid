using SendGrid;

namespace AspNetCore.Identity.Services.SendGrid
{
    /// <inheritdoc/>
    /// <remarks>This object adds properties to indicate default sender email address and if is in debug mode.</remarks>
    public class SendGridEmailProviderOptions : SendGridClientOptions
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SendGridEmailProviderOptions()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">SendGrid API Key</param>
        /// <param name="defaultFromEmailAddress">Default from email address</param>
        /// <param name="sandboxMode">SendGrid sandbox mode (does not actually send email)</param>
        /// <permission cref="logSuccesses">Log when SendGrid indicates success</permission>
        /// <permission cref="logErrors">Log when SendGrid indicates error</permission>
        /// <remarks>Logging is sent to the ILogger interface.</remarks>
        public SendGridEmailProviderOptions(string apiKey, string? defaultFromEmailAddress, bool sandboxMode = false, bool logSuccesses = false, bool logErrors = true)
        {
            ApiKey = apiKey;
            DefaultFromEmailAddress = defaultFromEmailAddress;
            SandboxMode = sandboxMode;
            LogSuccesses = logSuccesses;
            LogErrors = logErrors;  
        }



        /// <summary>
        /// Default 'from' email address if none given at send time.
        /// </summary>
        public string? DefaultFromEmailAddress { get; set; }

        /// <summary>
        /// Set to 'true' if you want'Sand Box Mode' turned on.
        /// </summary>
        public bool SandboxMode { get; set; } = false;

        /// <summary>
        /// Logs errors to the ILogger
        /// </summary>
        public bool LogErrors { get; set; } = true;

        /// <summary>
        /// Logs successful sends to the ILogger
        /// </summary>
        public bool LogSuccesses { get; set; } = false;
    }
}
