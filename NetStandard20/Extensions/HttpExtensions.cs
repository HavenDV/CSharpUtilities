﻿using System;
using System.Collections.Generic;
using System.Web;

#nullable enable

namespace NetStandard20.Extensions
{
    public static class HttpExtensions
    {
        public static Uri WithQuery(this Uri uri, Dictionary<string, string> dictionary)
        {
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