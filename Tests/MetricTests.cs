using System;
using CarbonApi.Client;
using FluentAssertions;
using NUnit.Framework;

namespace CarbonApi.Tests
{
    [TestFixture]
    public class MetricTests
    {
        [Test]
        [TestCase("web_available.https:__mercury_vetrf_ru_hs_", "web_available.https___mercury_vetrf_ru_hs_")]
        [TestCase("https:__mercury_vetrf_ru_hs_", "https___mercury_vetrf_ru_hs_")]
        public void NormalizeNameTest(string value, string expected)
        {
            var metric = new Metric(value, Array.Empty<DataPoint>(), DateTime.Now);

            metric.Name.Should().Be(expected);
        }
    }
}
