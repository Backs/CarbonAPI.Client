using System;

namespace CarbonApi.Client.Exceptions
{
    internal class ClusterClientException : Exception
    {
        public ClusterClientException(string message)
            : base(message)
        {
        }
    }
}
