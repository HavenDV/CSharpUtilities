using System;
using System.Collections.Generic;
using System.Web;

#nullable enable

namespace NetStandard20.Extensions
{
    /// <summary>
    /// Extensions that work with <see cref="Uri"/>
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Creates uri with query string using values dictionary <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="dictionary"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static Uri WithQuery(this Uri uri, Dictionary<string, string> dictionary)
        {
            uri = uri ?? throw new ArgumentNullException(nameof(uri));
            dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));

            var builder = new UriBuilder(uri);
            var parameters = HttpUtility.ParseQueryString(uri.Query);

            foreach (var pair in dictionary)
            {
                parameters[pair.Key] = pair.Value;
            }

            builder.Query = parameters.ToString();

            return builder.Uri;
        }
    }
}