using System.Threading.Tasks;
using CarbonApi.Client.Models;

namespace CarbonApi.Client
{
    public interface ICarbonApiClient
    {
        Task<Metric[]> GetPointsAsync(MetricTagQuery query);
    }
}
