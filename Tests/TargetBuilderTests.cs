using CarbonApi.Client;
using CarbonApi.Client.Aggregation;
using FluentAssertions;
using NUnit.Framework;

namespace CarbonApi.Tests
{
    [TestFixture]
    internal class TargetBuilderTests
    {
        [Test]
        public void Build()
        {
            var builder = new TargetBuilder()
                .WithPathPrefix("Prefix")
                .WithEnvironment("Environment")
                .WithPath("Path")
                .WithAggregation(new CompositeAggregation(new Aggregation1(), new Aggregation2()));

            var result = builder.Build();

            result.Should().Be("aggr2(aggr1(Prefix.Environment.Path,123),\"aaa\",\"bbb\")");
        }

        private class Aggregation1 : IAggregation
        {
            public string Apply(string path)
            {
                return $"aggr1({path},123)";
            }
        }

        private class Aggregation2 : IAggregation
        {
            public string Apply(string path)
            {
                return $"aggr2({path},\"aaa\",\"bbb\")";
            }
        }
    }
}
