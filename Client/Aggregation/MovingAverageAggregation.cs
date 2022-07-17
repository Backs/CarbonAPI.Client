using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class MovingAverageAggregation : IAggregation
    {
        private readonly string period;

        public MovingAverageAggregation(string period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            this.period = period;
        }

        public string Apply(string path)
        {
            return $"movingAverage({path},\"{this.period}\")";
        }
    }
}
