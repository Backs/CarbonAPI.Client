using System;
using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client.Models
{
    public class MetricQuery
    {
        public DateTime? From { get; set; }
        
        public DateTime? To { get; set; }

        public string Period { get; set; }

        public IAggregation Aggregation { get; set; }
    }
}
