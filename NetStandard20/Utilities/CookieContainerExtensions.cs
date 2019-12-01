using System;
using System.Linq;
using System.Net;

namespace NetStandard20.Utilities
{
    public static class CookieContainerExtensions
    {
        public static bool ContainsCookieWithName(this CookieContainer container, string url, string name)
        {
            var cookieCollection = container.GetCookies(new Uri(url));

            return cookieCollection
                .Cast<Cookie>()
                .Any(cookie => cookie.Name == name);
        }

        public static void CopyCookies(this CookieContainer container, string from, string to)
        {
            var cookieCollection = container.GetCookies(new Uri(from));
            var uri = new Uri(to);
            foreach (var cookie in cookieCollection.Cast<Cookie>())
            {
                container.Add(uri, new Cookie(cookie.Name, cookie.Value));
            }
        }
    }
}
