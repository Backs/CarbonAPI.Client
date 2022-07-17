# CarbonAPI.Client
[![MIT](https://img.shields.io/github/license/Backs/CarbonAPI.Client)](LICENSE)
[![AppVeyor](https://img.shields.io/appveyor/build/Backs/carbonapi-client)](https://ci.appveyor.com/project/Backs/carbonapi-client)
[![Nuget](https://img.shields.io/nuget/v/CarbonAPI.Client)](https://www.nuget.org/packages/CarbonAPI.Client/)

### How to use

```
var factory = new CarbonApiClusterClientFactory(new SilentLog(), "http://url", "login", "password");

var client = factory.Create();

// Create query by tags with aggregations, alias for the last 15 min
var query = MetricTagQueryBuilder.New()
	.AddTag("event", "operations")
	.AddTag("name", "processingTime")
	.AddTag("application", "(Mercury_Vetis_Proxy)", MetricTagOperator.NotLike)
	.AddTag("_aggregate", "p95")
	.AddTag("applicationType", "(ProcessIncomingConsignment|PrepareOutgoingConsignment|GetStockEntryChangesList|GetVetDocumentChangesList)", MetricTagOperator.Like)
	.AddAggregation(new SummarizeAggregation(Period.FromMinutes((int)this.slowProcessingMovingAverage.TotalMinutes), SummarizeFunc.Min))
	.AddAggregation(new AliasByTagsAggregation("applicationType"))
	.WithPeriod(Period.FromMinutes(15))
	.Build();

// aliasByTags(summarize(seriesByTag('event=operations', 'name=processingTime', 'application!=~(Mercury_Vetis_Proxy)', '_aggregate=p95', 'applicationType=~(ProcessIncomingConsignment|PrepareOutgoingConsignment|GetStockEntryChangesList|GetVetDocumentChangesList)'),'10m', 'min', true), 'applicationType')

var result = await client.GetPointsAsync(query);
```