using System;
using System.Linq;

namespace CarbonApi.Client.Aggregation
{
    public sealed class AliasByNodeAggregation : IAggregation
    {
        private readonly int[] nodeIndices;

        public AliasByNodeAggregation(int[] nodeIndices)
        {
            if (nodeIndices == null || nodeIndices.Length == 0)
            {
                throw new ArgumentNullException(nameof(nodeIndices));
            }

            this.nodeIndices = nodeIndices;
        }
        public string Apply(string path)
        {
            return $"aliasByNode({path},{string.Join(",", nodeIndices)})";
        }
    }
}
