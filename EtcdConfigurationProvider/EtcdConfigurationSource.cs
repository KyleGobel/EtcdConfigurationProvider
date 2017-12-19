using Microsoft.Extensions.Configuration;

namespace EtcdConfigurationProvider
{
    public class EtcdConfigurationSource : IConfigurationSource
    {
        public EtcdConnectionOptions Options { get; set; }

        public EtcdConfigurationSource(EtcdConnectionOptions options) => Options = options;
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new EtcdConfigurationProvider(this);
    }
}