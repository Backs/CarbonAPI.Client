using System;

namespace CarbonApi.Client.Exceptions
{
    public class CarbonApiHttpException : Exception
    {
        public int HttpCode { get; }

        public string Content { get; }

        public CarbonApiHttpException(int httpCode, string content)
        : base($"{httpCode} - {content}")
        {
            HttpCode = httpCode;
            Content = content;
        }
    }
}
