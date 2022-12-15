# SendGrid IEmailSender for ASP.NET Core Identity

This is a [SendGrid](https://github.com/sendgrid/sendgrid-csharp) [Email Sender](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=visual-studio#configure-an-email-provider) 
for use with Asp.Net Core Identity (.Net 6 or .Net 7). It installs as a service in your project's 'Program.cs' or 'Startup.cs' file.

## Feedback Welcome!

Please send us feedback regarding this package using our [issues](https://github.com/CosmosSoftware/AspNetCore.Identity.Services.SendGrid/issues)
section of our [project repository](https://github.com/CosmosSoftware/AspNetCore.Identity.Services.SendGrid#sendgrid-iemailsender-for-aspnet-core-identity).

## Installation

To add this provider to your own .Net 6 or .Net 7 web project, add the following [NuGet package](https://www.nuget.org/packages/AspNetCore.Identity.Services.SendGrid)

```shell
PM> Install-Package AspNetCore.Identity.Services.SendGrid
```

## Configure app to support email

Next add the following code to your startup file:

```csharp
// Get SendGrid key
var sendGridApiKey = builder.Configuration.GetValue<string>("SendGridApiKey");
// In the following you're adding your SendGrid API key and the default "From" email address.
var sendGridOptions = new SendGridEmailProviderOptions(sendGridApiKey, "your@emailaddress.com");

// You can put the provider into "Sandbox mode" by adding the following:
sendGridOptions.SandboxMode = true;

// Add the configured service.
builder.Services.AddSendGridEmailProvider(sendGridOptions);
```

## Debugging Email Problems

It is possible to debug email problems by using the `Response` property of the sender.
This returns the SendGrid response object which indicates success or error.  Here is
example code:

```csharp
var response = ((SendGridEmailSender) _emailSender).Response;
```

## Sandbox Mode

Putting the provider into [sandbox mode](https://docs.sendgrid.com/for-developers/sending-email/sandbox-mode) will allow you to validate interoperability
with SendGrid while not actually sending any emails.

## Logging Email Success and Errors

Often in production you may want the email provider to send errors and/or successful
email sends to the ILogger.  You can also cause the provider to throw exceptions
when email send encounters errors.

Here is example code that show how to enable each option:

```csharp
var options = new SendGridEmailProviderOptions(
    "SendGrid_API_Key", // Your SendGrid API Key,
    "foo@foo.com", // Your default 'from' email address
    false, // Sandbox mode true or false
    false, // Send successful sends to ILogger (true or false)
    true // Send errors sends to ILogger (true or false)
);

options.HttpErrorAsException = true; // Throw exceptions upon SendGrid errors
```

## Bug Reports, Featue Requests, and Feedback

Please submit bug reports, feature requests or other feedback to our [issues discussion](https://github.com/CosmosSoftware/AspNetCore.Identity.Services.SendGrid/issues).
