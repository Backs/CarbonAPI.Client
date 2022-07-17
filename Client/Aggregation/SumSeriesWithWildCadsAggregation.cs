using System;
using System.Linq;

namespace CarbonApi.Client.Aggregation
{
    public sealed class SumSeriesWithWildCadsAggregation : IAggregation
    {
        private readonly int[] nodeIndices;

        public SumSeriesWithWildCadsAggregation(int[] nodeIndices)
        {
            if (nodeIndices == null || nodeIndices.Length == 0)
            {
                throw new ArgumentNullException(nameof(nodeIndices));
            }

            this.nodeIndices = nodeIndices;
        }
        public string Apply(string path)
        {
            return $"sumSeriesWithWildcards({path},{string.Join(",", nodeIndices)})";
        }
    }
}
