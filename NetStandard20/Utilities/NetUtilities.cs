using System;
using System.Net;
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
    }
}