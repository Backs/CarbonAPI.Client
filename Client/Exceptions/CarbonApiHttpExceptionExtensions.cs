using System;

namespace CarbonApi.Client.Exceptions;

internal static class CarbonApiHttpExceptionExtensions
{
    extension(CarbonApiHttpException ex)
    {
        public bool IsIgnoring()
        {
            return ex.IsMetricMissing() || ex.IsUnavailable() || ex.IsTimeout() || ex.Is400();
        }

        private bool IsMetricMissing()
        {
            return ex.HttpCode == 500 &&
                   ex.Content.IndexOf("index out of range", StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        private bool IsUnavailable()
        {
            return ex.HttpCode == 502;
        }

        private bool IsTimeout()
        {
            return ex.HttpCode == 504;
        }

        private bool Is400()
        {
            return ex.HttpCode is 450 or 408;
        }
    }
}