using System;

namespace CarbonApi.Client
{
    public class DataPoint
    {
        public double? Value { get; }

        public DateTime DateTime { get; }

        public DataPoint(double? value, DateTime dateTime)
        {
            this.Value = value;
            this.DateTime = dateTime;
        }
    }
}
