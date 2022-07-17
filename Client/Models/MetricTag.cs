using System;
using System.Collections.Generic;

namespace CarbonApi.Client.Models
{
    public sealed class MetricTag
    {
        public MetricTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Operator = op;
        }

        public string Key { get; }

        public string Value { get; }

        public MetricTagOperator Operator { get; }

        public override string ToString()
        {
            var op = Operators[this.Operator];
            return $"'{this.Key}{op}{this.Value}'";
        }

        private static readonly Dictionary<MetricTagOperator, string> Operators = new Dictionary<MetricTagOperator, string>
        {
            { MetricTagOperator.Equal, "=" },
            { MetricTagOperator.Like, "=~" },
            { MetricTagOperator.NotEqual, "!=" },
            { MetricTagOperator.NotLike, "!=~" }
        };
    }
}
