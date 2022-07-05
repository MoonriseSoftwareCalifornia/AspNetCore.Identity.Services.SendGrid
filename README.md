# SendGrid IEmailSender for ASP.NET Core Identity

This is a .Net 6 [SendGrid](https://github.com/sendgrid/sendgrid-csharp) [Email Sender](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=visual-studio#configure-an-email-provider) for use with Asp.Net Core Identity. It installs as a service in your project's 'Program.cs' or 'Startup.cs' file.

## Installation

To add this provider to your own Asp.Net 6 web project, add the following [NuGet package](https://www.nuget.org/packages/AspNetCore.Identity.Services.SendGrid)

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
// Add the configured service.
builder.Services.AddSendGridEmailProvider(sendGridOptions);
```

Feedback regarding this provider is greatly appreciated!
