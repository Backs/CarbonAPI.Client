namespace CarbonApi.Client.Aggregation
{
    public sealed class SumAggregation : IAggregation
    {
        public string Apply(string path)
        {
            return $"sum({path})";
        }
    }
}
