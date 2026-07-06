namespace CarbonApi.Client.Aggregation;

public interface IAggregationContainer<out T>
{
    T AddAggregation(IAggregation aggregation);
}