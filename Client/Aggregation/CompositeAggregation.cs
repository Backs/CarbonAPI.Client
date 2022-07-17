using System.Collections.Generic;

namespace CarbonApi.Client.Aggregation
{
    public class CompositeAggregation : IAggregation
    {
        private readonly IReadOnlyCollection<IAggregation> aggregations;

        public CompositeAggregation(params IAggregation[] aggregations)
        {
            this.aggregations = aggregations;
        }

        public CompositeAggregation(IReadOnlyCollection<IAggregation> aggregations)
        {
            this.aggregations = aggregations;
        }

        public string Apply(string path)
        {
            var result = path;

            foreach (var aggregation in aggregations)
            {
                result = aggregation.Apply(result);
            }

            return result;
        }
    }
}
