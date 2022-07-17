using System;
using System.Collections.Generic;
using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client.Models
{
    public sealed class MetricTagQuery
    {
        private readonly List<MetricTag> tags;

        private readonly List<IAggregation> aggregations;

        public MetricTagQuery(List<MetricTag> tags, List<IAggregation> aggregations, string? period)
        {
            this.tags = tags;
            this.aggregations = aggregations;
            this.Period = period;
        }

        public string? Period { get; private set; }

        public MetricTagQuery AddTag(MetricTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            this.tags.Add(tag);
            return this;
        }

        public MetricTagQuery AddTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
        {
            this.tags.Add(new MetricTag(key, value, op));
            return this;
        }

        public MetricTagQuery AddAggregation(IAggregation aggregation)
        {
            if (aggregation == null)
            {
                throw new ArgumentNullException(nameof(aggregation));
            }

            this.aggregations.Add(aggregation);
            return this;
        }

        public MetricTagQuery WithPeriod(string period)
        {
            this.Period = period;
            return this;
        }

        public override string ToString()
        {
            var result = $"seriesByTag({string.Join(", ", this.tags)})";

            if (this.aggregations.Count == 0)
            {
                return result;
            }

            if (this.aggregations.Count == 1)
            {
                return this.aggregations[0].Apply(result);
            }

            return new CompositeAggregation(this.aggregations.AsReadOnly()).Apply(result);
        }
    }
}
