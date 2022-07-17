namespace CarbonApi.Client.Aggregation
{
    public sealed class MaxSeriesAggregation : IAggregation
    {
        public string Apply(string path)
        {
            return $"maxSeries({path})";
        }
    }
}
