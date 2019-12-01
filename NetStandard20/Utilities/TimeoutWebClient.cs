using System;
using System.Net;

#nullable enable

namespace NetStandard20.Utilities
{
    public class TimeoutWebClient : WebClient
    {
        public TimeSpan Timeout { get; set; }

        public TimeoutWebClient(TimeSpan timeout)
        {
            Timeout = timeout;
        }

        protected override WebRequest? GetWebRequest(Uri uri)
        {
            var request = base.GetWebRequest(uri);
            if (request == null)
            {
                return null;
            }

            var timeoutInMillisecond = (int)Timeout.TotalMilliseconds;

            request.Timeout = timeoutInMillisecond;
            if (request is HttpWebRequest httpWebRequest)
            {
                httpWebRequest.ReadWriteTimeout = timeoutInMillisecond;
            }

            return request;
        }
    }
}