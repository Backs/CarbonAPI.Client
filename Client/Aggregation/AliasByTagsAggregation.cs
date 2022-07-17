using System.Linq;

namespace CarbonApi.Client.Aggregation
{
    public sealed class AliasByTagsAggregation : IAggregation
    {
        private readonly string[] tags;

        public AliasByTagsAggregation(params string[] tags)
        {
            this.tags = tags;
        }

        public string Apply(string path)
        {
            return $"aliasByTags({path}, {string.Join(", ", tags.Select(o => $"'{o}'"))})";
        }
    }
}
