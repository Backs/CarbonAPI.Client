using System;
using System.Text;
using System.Threading.Tasks;
using CarbonApi.Client.Exceptions;
using Newtonsoft.Json;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Clusterclient.Core.Strategies;

namespace CarbonApi.Client
{
    internal static class ClusterClientExtensions
    {
        public static void EnsureSuccess(this ClusterResult result)
        {
            if (result.Status != ClusterResultStatus.Success)
            {
                throw new Exceptions.ClusterClientException($"Error occured while processing request [{result.Request}] with status [{result.Status}] with response [{(int)result.Response.Code}-{result.Response.Content}]");
            }

            if (!result.Response.IsSuccessful)
            {
                throw new CarbonApiHttpException((int)result.Response.Code, result.Response.Content.ToString());
            }
        }

        public static async Task<TResponse> GetResponseContentAsync<TResponse>(
            this IClusterClient client,
            Request request,
            IRequestStrategy? strategy = null,
            TimeSpan? timeout = null,
            JsonSerializerSettings? settings = null)
        {
            using var result = await client.SendAsync(request, strategy: strategy, timeout: timeout).ConfigureAwait(false);
            result.EnsureSuccess();
            var jsonString = Encoding.UTF8.GetString(result.Response.Content.Buffer);
            return JsonConvert.DeserializeObject<TResponse>(jsonString, settings);
        }
    }
}
