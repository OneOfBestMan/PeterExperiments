using FluentEmail.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentEmailAliyunBuilderExtensions
    {
        public static FluentEmailServicesBuilder AddMailGunSender(this FluentEmailServicesBuilder builder, string domainName, string apiKey, MailGunRegion mailGunRegion = MailGunRegion.USA)
        {
            builder.Services.TryAdd(ServiceDescriptor.Scoped<ISender>(x => new MailgunSender(domainName, apiKey, mailGunRegion)));
            return builder;
        }
    }
}
