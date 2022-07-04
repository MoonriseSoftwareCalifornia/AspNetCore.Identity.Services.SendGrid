# SendGrid Email Provider for ASP.NET Identity

This is an Email Provider for use with Asp.Net Identity. It is built for .Net 6 and installs as a service in either the 'Program.cs' or 'Startup.cs' file f your project.

# Installation

To add this provider to your own Asp.Net 6 web project, add the following [NuGet package](https://www.nuget.org/packages/AspNetCore.Identity.Services.SendGrid

```shell
PM> Install-Package AspNetCore.Identity.Services.SendGrid
```
## Configure app to support email

Next add the following code to your startup file:

```csharp
var 
var sendGridApiKey = builder.Configuration.GetValue<string>("SendGridApiKey");
var sendGridOptions = new SendGridEmailProviderOptions(sendGridApiKey, "your@emailaddress.com");
builder.Services.AddSendGridEmailProvider(sendGridOptions);
```

Feedback regarding this provider s greatly appreciated!
