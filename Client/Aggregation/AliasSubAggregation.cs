using System;

namespace CarbonApi.Client.Aggregation
{
    public sealed class AliasSubAggregation : IAggregation
    {
        private readonly string searchPattern;
        private readonly string replacePattern;

        public AliasSubAggregation(string searchPattern, string replacePattern)
        {
            if (string.IsNullOrWhiteSpace(searchPattern))
            {
                throw new ArgumentNullException(nameof(searchPattern));
            }

            if (string.IsNullOrWhiteSpace(replacePattern))
            {
                throw new ArgumentNullException(nameof(replacePattern));
            }

            this.searchPattern = searchPattern;
            this.replacePattern = replacePattern;
        }

        public string Apply(string path)
        {
            return $"aliasSub({path},'{searchPattern}','{replacePattern}')";
        }
    }
}
