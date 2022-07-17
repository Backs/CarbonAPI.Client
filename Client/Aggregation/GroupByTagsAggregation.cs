using System;
using System.Linq;

namespace CarbonApi.Client.Aggregation
{
    public sealed class GroupByTagsAggregation : IAggregation
    {
        private readonly string func;
        private readonly string[] tags;

        public GroupByTagsAggregation(string func, params string[] tags)
        {
            if (string.IsNullOrWhiteSpace(func))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(func));
            }

            this.func = func;
            this.tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }
        public string Apply(string path)
        {
            return $"groupByTags({path}, '{func}', {string.Join(", ", tags.Select(o => $"'{o}'"))})";
        }
    }
}
