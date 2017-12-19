using EtcdNet;
using Microsoft.Extensions.Configuration;

namespace EtcdConfigurationProvider
{
    public class EtcdConfigurationProvider : ConfigurationProvider
    {
        private EtcdConfigurationSource _source;

        public EtcdConfigurationProvider(EtcdConfigurationSource source) => _source = source;

        public override void Load()
        {
            var options = new EtcdClientOpitions
            {
                Urls = _source.Options.Urls,
                IgnoreCertificateError = true,
                Username = _source.Options.Username,
                Password = _source.Options.Password,
                UseProxy = false
            };

            var client = new EtcdClient(options);
            try
            {
                var response = client.GetNodeAsync(_source.Options.RootKey, recursive: true, sorted: true)
                    .GetAwaiter().GetResult();

                if (response.Node.Nodes != null)
                {
                    foreach (var node in response.Node.Nodes)
                    {
                        //child node
                        Data[node.Key] = node.Value;
                    }
                }
            }
            catch (EtcdCommonException.KeyNotFound)
            {
                // mm, would love to log this
                throw;
            }
        }


    }
}