using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class AliasAggregation : IAggregation
    {
        private readonly string name;

        public AliasAggregation(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.name = name;
        }
        public string Apply(string path)
        {
            return $"alias({path},'{this.name}')";
        }
    }
}
