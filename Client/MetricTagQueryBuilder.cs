using System;
using System.Collections.Generic;
using CarbonApi.Client.Aggregation;
using CarbonApi.Client.Models;

namespace CarbonApi.Client
{
    internal sealed class MetricTagQueryBuilder
    {
        private readonly List<MetricTag> tags = new List<MetricTag>();

        private readonly List<IAggregation> aggregations = new List<IAggregation>();

        private string period;

        private MetricTagQueryBuilder()
        {

        }

        public static MetricTagQueryBuilder New()
        {
            return new MetricTagQueryBuilder();
        }

        public MetricTagQueryBuilder AddTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
        {
            this.tags.Add(new MetricTag(key, value, op));
            return this;
        }

        public MetricTagQueryBuilder AddAggregation(IAggregation aggregation)
        {
            if (aggregation == null)
            {
                throw new ArgumentNullException(nameof(aggregation));
            }

            this.aggregations.Add(aggregation);
            return this;
        }

        public MetricTagQueryBuilder WithPeriod(string period)
        {
            this.period = period;
            return this;
        }

        public MetricTagQuery Build()
        {
            return new MetricTagQuery(this.tags, this.aggregations, this.period);
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
