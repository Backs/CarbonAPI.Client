using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class AsPercentAggregation : IAggregation
    {
        private readonly string totalMetricPath;

        public AsPercentAggregation(string totalMetricPath)
        {
            if (string.IsNullOrWhiteSpace(totalMetricPath))
            {
                throw new ArgumentNullException(nameof(totalMetricPath));
            }

            this.totalMetricPath = totalMetricPath;
        }
        public string Apply(string path)
        {
            return $"asPercent({path},{totalMetricPath})";
        }
    }
}
