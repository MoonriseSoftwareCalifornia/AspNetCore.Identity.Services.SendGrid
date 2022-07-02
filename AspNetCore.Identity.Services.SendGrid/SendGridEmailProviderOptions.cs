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
        /// <param name="apiKey"></param>
        /// <param name="defaultFromEmailAddress"></param>
        /// <param name="sandboxMode"></param>
        public SendGridEmailProviderOptions(string apiKey, string? defaultFromEmailAddress, bool sandboxMode = false)
        {
            ApiKey = apiKey;
            DefaultFromEmailAddress = defaultFromEmailAddress;
            SandboxMode = sandboxMode;
        }



        /// <summary>
        /// Default 'from' email address if none given at send time.
        /// </summary>
        public string? DefaultFromEmailAddress { get; set; }

        /// <summary>
        /// Set to 'true' if you want'Sand Box Mode' turned on.
        /// </summary>
        public bool SandboxMode { get; set; } = false;
    }
}
