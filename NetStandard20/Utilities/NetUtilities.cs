using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace NetStandard20.Utilities
{
    public static class NetUtilities
    {
        /// <summary>
        /// Returns string with url data
        /// </summary>
        /// <param name="url"></param>
        /// <param name="proxy"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="WebException"></exception>
        /// <returns></returns>
        public static async Task<string> GetString(string url, IWebProxy? proxy = null)
        {
            using var client = new TimeoutWebClient(TimeSpan.FromSeconds(15))
            {
                Proxy = proxy
            };

            return await client.DownloadStringTaskAsync(url);
        }

        /// <summary>
        /// Returns data of GET request to selected uri <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <returns></returns>
        public static async Task<string> ReadGetRequestDataAsync(Uri uri, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
        {
            using var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            if (timeout != null)
            {
                source.CancelAfter(timeout.Value);
            }

            using var client = new HttpClient();
            using var response = await client.GetAsync(uri, source.Token).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
    }
}