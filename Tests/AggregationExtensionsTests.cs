using CarbonApi.Client;
using FluentAssertions;
using NUnit.Framework;

namespace CarbonApi.Tests;

[TestFixture]
public class AggregationExtensionsTests
{
    [Test]
    public void Should_Add_Aggregations_To_Builder()
    {
        var query = MetricTagQueryBuilder.New()
            .AddTag("app", "test")
            .Alias("newName")
            .MaxSeries()
            .Build();

        query.ToString().Should().Be("maxSeries(alias(seriesByTag('app=test'),'newName'))");
    }

    [Test]
    public void Should_Add_Aggregations_To_Query()
    {
        var query = MetricTagQueryBuilder.New()
            .AddTag("app", "test")
            .Build();

        var updatedQuery = query
            .Alias("newName")
            .MaxSeries();

        updatedQuery.ToString().Should().Be("maxSeries(alias(seriesByTag('app=test'),'newName'))");
    }

    [Test]
    public void Should_Support_All_Extensions()
    {
        var builder = MetricTagQueryBuilder.New().AddTag("app", "test");
            
        builder.AliasByNode(1, 2)
            .AliasByTags("tag1", "tag2")
            .AliasSub("search", "replace")
            .AsPercent("total")
            .GroupByTags("sum", "tag3")
            .MovingAverage("5m")
            .MovingSum("10m")
            .Sum()
            .SumSeriesWithWildcards(3)
            .Summarize("1h", "avg");

        var query = builder.Build();
        var str = query.ToString();

        str.Should().Contain("aliasByNode");
        str.Should().Contain("aliasByTags");
        str.Should().Contain("aliasSub");
        str.Should().Contain("asPercent");
        str.Should().Contain("groupByTags");
        str.Should().Contain("movingAverage");
        str.Should().Contain("movingSum");
        str.Should().Contain("sum(");
        str.Should().Contain("sumSeriesWithWildcards");
        str.Should().Contain("summarize");
    }
}