using System;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Criteria;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Topology;
using Vostok.Clusterclient.Transport;
using Vostok.Logging.Abstractions;

namespace CarbonApi.Client
{
    internal class CarbonApiClusterClientFactory : ICarbonApiClusterClientFactory
    {
        private readonly ILog log;
        private readonly string url;
        private readonly string login;
        private readonly string password;

        public CarbonApiClusterClientFactory(ILog log, string url, string login, string password)
        {
            this.url = url;
            this.login = login;
            this.password = password;
            this.log = log;
        }

        public ICarbonApiClient Create()
        {
            return new CarbonApiClient(this.GetClusterClient());
        }

        private IClusterClient GetClusterClient()
        {
            var client = new ClusterClient(
                this.log,
                configuration =>
                {
                    configuration.SetupUniversalTransport();
                    configuration.ResponseCriteria.Add(new AlwaysAcceptCriterion());

                    configuration.ClusterProvider = new FixedClusterProvider(new Uri(url));
                    configuration.AddRequestTransform(t =>
                        t.WithBasicAuthorizationHeader(login, password));
                }
            );

            return client;
        }
    }
}