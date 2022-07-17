using CarbonApi.Client.Aggregation;
using CarbonApi.Client.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CarbonApi.Tests
{
    [TestFixture]
    internal class CompositeAggregationTests
    {
        [Test]
        public void ApplyTest()
        {
            var aggregation = new CompositeAggregation(
                new SummarizeAggregation(Period.FromHours(3), SummarizeFunc.Avg),
                new SumSeriesWithWildCadsAggregation(new[] {1, 5, 9}),
                new AliasSubAggregation("search1", "replace1"),
                new AliasAggregation("new_name"),
                new AliasByNodeAggregation(new[] {2, 3}),
                new AsPercentAggregation("total.metric.path")
            );

            var result = aggregation.Apply("some.metric.path");

            result.Should().Be("asPercent(aliasByNode(alias(aliasSub(sumSeriesWithWildcards(summarize(some.metric.path,'3h', 'avg', true),1,5,9),'search1','replace1'),'new_name'),2,3),total.metric.path)");
        }
    }
}
