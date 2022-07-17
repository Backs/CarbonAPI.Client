using CarbonApi.Client.Aggregation;

namespace CarbonApi.Client
{
    public class TargetBuilder
    {
        private string pathPrefix;

        private string path;

        private string environment;

        private IAggregation? aggregation;

        public TargetBuilder WithPath(string path)
        {
            this.path = path;
            return this;
        }

        public TargetBuilder WithPathPrefix(string pathPrefix)
        {
            this.pathPrefix = pathPrefix;
            return this;
        }

        public TargetBuilder WithEnvironment(string environment)
        {
            this.environment = environment;
            return this;
        }

        public TargetBuilder WithAggregation(IAggregation aggregation)
        {
            this.aggregation = aggregation;

            return this;
        }

        public string Build()
        {
            var fullPath = $"{pathPrefix}.{environment}.{path}";

            if (aggregation != null)
            {
                fullPath = aggregation.Apply(fullPath);
            }

            return fullPath;
        }
    }
}
