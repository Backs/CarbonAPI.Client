using CarbonApi.Client;
using CarbonApi.Client.Aggregation;
using CarbonApi.Client.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CarbonApi.Tests
{
    [TestFixture]
    public class MetricTagQueryBuilderTests
    {
        [Test]
        public void BuildTest()
        {
            var query = MetricTagQueryBuilder.New()
                .AddTag("event", "operations")
                .AddTag("name", "processingTime")
                .AddTag("application", "(Mercury_Vetis_Proxy)", MetricTagOperator.NotLike)
                .AddTag("_aggregate", "p95")
                .AddTag("applicationType", "(ProcessIncomingConsignment|PrepareOutgoingConsignment|GetStockEntryChangesList|GetVetDocumentChangesList)", MetricTagOperator.Like)
                .AddAggregation(new SummarizeAggregation(Period.FromMinutes(10), SummarizeFunc.Min))
                .AddAggregation(new AliasByTagsAggregation("applicationType"))
                .WithPeriod(Period.FromMinutes(15))
                .Build();

            query.ToString().Should().Be("aliasByTags(summarize(seriesByTag('event=operations', 'name=processingTime', 'application!=~(Mercury_Vetis_Proxy)', '_aggregate=p95', 'applicationType=~(ProcessIncomingConsignment|PrepareOutgoingConsignment|GetStockEntryChangesList|GetVetDocumentChangesList)'),'10m', 'min', true), 'applicationType')");
        }
    }
}
