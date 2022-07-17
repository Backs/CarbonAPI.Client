using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class SummarizeAggregation : IAggregation
    {
        private readonly string summarizePeriod;
        private readonly string summarizeFunc;

        public SummarizeAggregation(string summarizePeriod, string summarizeFunc)
        {
            if (string.IsNullOrWhiteSpace(summarizeFunc))
            {
                throw new ArgumentNullException(nameof(summarizeFunc));
            }

            if (string.IsNullOrWhiteSpace(summarizePeriod))
            {
                throw new ArgumentNullException(nameof(summarizePeriod));
            }

            this.summarizePeriod = summarizePeriod;
            this.summarizeFunc = summarizeFunc;
        }

        public string Apply(string path)
        {
            return $"summarize({path},'{summarizePeriod}', '{summarizeFunc}', true)";//alignToFrom = true
        }
    }
}
