using Microsoft.Extensions.Configuration;

namespace EtcdConfigurationProvider
{
    public static class EtcdConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddEtcdConfiguration(this IConfigurationBuilder builder,
            EtcdConnectionOptions connectionOptions)
        {
            return builder.Add(new EtcdConfigurationSource(connectionOptions));
        }
    }
}