using System;
using System.Collections.Generic;

namespace CarbonApi.Client.Models
{
    public sealed class MetricTag
    {
        public MetricTag(string key, string value, MetricTagOperator op = MetricTagOperator.Equal)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = op;
        }

        public string Key { get; }

        public string Value { get; }

        public MetricTagOperator Operator { get; }

        public override string ToString()
        {
            var op = Operators[Operator];
            return $"'{Key}{op}{Value}'";
        }

        private static readonly Dictionary<MetricTagOperator, string> Operators = new()
        {
            { MetricTagOperator.Equal, "=" },
            { MetricTagOperator.Like, "=~" },
            { MetricTagOperator.NotEqual, "!=" },
            { MetricTagOperator.NotLike, "!=~" }
        };
    }
}
