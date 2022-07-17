using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarbonApi.Client
{
    public class Metric : IStatisticKey
    {
        public string Name { get; }

        public DataPoint[] DataPoints { get; }

        public DateTime AcquireDate { get; }

        public Metric(string name, DataPoint[] dataPoints, DateTime acquireDate)
        {
            this.Name = NormalizeName(name);
            this.DataPoints = dataPoints;
            this.AcquireDate = acquireDate;
        }

        public DataPoint? GetLastNotNull()
        {
            return this.DataPoints?
                .Where(x => x.Value.HasValue)
                .OrderByDescending(x => x.DateTime)
                .FirstOrDefault();
        }

        private static readonly Regex CharsRegex = new Regex("[^a-zA-Z0-9.]", RegexOptions.Compiled);

        private static string NormalizeName(string value)
        {
            return CharsRegex.Replace(value, "_");
        }
    }
}
