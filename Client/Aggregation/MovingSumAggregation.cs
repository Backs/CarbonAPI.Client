using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class MovingSumAggregation : IAggregation
    {
        private readonly string period;

        public MovingSumAggregation(string period)
        {
            if (period == null)
            {
                throw new ArgumentNullException(nameof(period));
            }

            this.period = period;
        }

        public string Apply(string path)
        {
            return $"movingSum({path},\"{this.period}\")";
        }
    }
}
