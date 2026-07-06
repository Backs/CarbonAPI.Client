using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client;

public static class AggregationExtensions
{
    extension<T>(T container) where T : IAggregationContainer<T>
    {
        public T Alias(string name) => container.AddAggregation(new AliasAggregation(name));
        public T AliasByNode(params int[] nodeIndices) => container.AddAggregation(new AliasByNodeAggregation(nodeIndices));
        public T AliasByTags(params string[] tags) => container.AddAggregation(new AliasByTagsAggregation(tags));
        public T AliasSub(string searchPattern, string replacePattern) => container.AddAggregation(new AliasSubAggregation(searchPattern, replacePattern));
        public T AsPercent(string totalMetricPath) => container.AddAggregation(new AsPercentAggregation(totalMetricPath));
        public T GroupByTags(string func, params string[] tags) => container.AddAggregation(new GroupByTagsAggregation(func, tags));
        public T MaxSeries() => container.AddAggregation(new MaxSeriesAggregation());
        public T MovingAverage(string period) => container.AddAggregation(new MovingAverageAggregation(period));
        public T MovingSum(string period) => container.AddAggregation(new MovingSumAggregation(period));
        public T Sum() => container.AddAggregation(new SumAggregation());
        public T SumSeriesWithWildcards(params int[] nodeIndices) => container.AddAggregation(new SumSeriesWithWildCadsAggregation(nodeIndices));
        public T Summarize(string summarizePeriod, string summarizeFunc) => container.AddAggregation(new SummarizeAggregation(summarizePeriod, summarizeFunc));
    }
}