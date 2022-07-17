using System;
using System.Linq;
using System.Threading.Tasks;
using CarbonApi.Client.Exceptions;
using CarbonApi.Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;

namespace CarbonApi.Client
{
    internal class CarbonApiClient : ICarbonApiClient
    {
        private static readonly DateTime Epoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly IClusterClient clusterClient;
        
        public CarbonApiClient(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }

        public async Task<Metric[]> GetPointsAsync(MetricTagQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var request = Request.Get("render")
                                     .WithAdditionalQueryParameter("target", query.ToString())
                                     .WithAdditionalQueryParameter("format", "json");

            if (query.Period != null)
            {
                request = request.WithAdditionalQueryParameter("from", $"-{query.Period}");
            }

            try
            {
                var response = await clusterClient.GetResponseContentAsync<MetricData[]>(request, settings: JsonSettings)
                    .ConfigureAwait(false);

                return response.Select(
                                   x => new Metric(
                                       x.Target,
                                       x.DataPoints.Select(
                                            p => new DataPoint((double?)p[0], FromUnixTime((long)p[1].Value)))
                                        .ToArray(),
                                       DateTime.UtcNow
                                   )
                               )
                               .ToArray();
            }
            catch (CarbonApiHttpException ex) when (ex.IsIgnoring())
            {
                return Array.Empty<Metric>();
            }
        }

        private static DateTime FromUnixTime(long unixTime)
        {
            return Epoch.AddSeconds(unixTime);
        }
    }
}
