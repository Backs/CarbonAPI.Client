namespace CarbonApi.Client.Models
{
    internal class MetricData
    {
        public string Target { get; set; }

        public decimal?[][] DataPoints { get; set; }
    }
}
