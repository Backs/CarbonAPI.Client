using System;

namespace CarbonApi.Client.Exceptions
{
    internal static class CarbonApiHttpExceptionExtensions
    {
        public static bool IsIgnoring(this CarbonApiHttpException ex)
        {
            return ex.IsMetricMissing() || ex.IsUnavailable() || ex.IsTimeout() || ex.Is400();
        }

        private static bool IsMetricMissing(this CarbonApiHttpException ex)
        {
            return ex.HttpCode == 500 &&
                   ex.Content.IndexOf("index out of range", StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        private static bool IsUnavailable(this CarbonApiHttpException ex)
        {
            return ex.HttpCode == 502;
        }

        private static bool IsTimeout(this CarbonApiHttpException ex)
        {
            return ex.HttpCode == 504;
        }

        private static bool Is400(this CarbonApiHttpException ex)
        {
            return ex.HttpCode is 450 or 408;
        }
    }
}