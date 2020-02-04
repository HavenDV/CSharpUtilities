using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class UriExtensionsTests
    {
        [TestMethod]
        public void WithQueryTest()
        {
            Assert.AreEqual(
                new Uri("https://www.unibet.ie/kambi-rest-api/gameLauncher2.json?_=1574752540405&useRealMoney=true&brand=unibet&jurisdiction=IE&locale=en_IE&currency=EUR&deviceGroup=desktop&clientId=polopoly_desktop&deviceOs=&marketLocale=en_IE&loadHTML5client=true&enablePoolBetting=false"),
                new Uri("https://www.unibet.ie/kambi-rest-api/gameLauncher2.json").WithQuery(new Dictionary<string, string>
                {
                    { "_", "1574752540405" },
                    { "useRealMoney", "true" },
                    { "brand", "unibet" },
                    { "jurisdiction", "IE" },
                    { "locale", "en_IE" },
                    { "currency", "EUR" },
                    { "deviceGroup", "desktop" },
                    { "clientId", "polopoly_desktop" },
                    { "deviceOs", "" },
                    { "marketLocale", "en_IE" },
                    { "loadHTML5client", "true" },
                    { "enablePoolBetting", "false" },
                }));
        }

        [TestMethod]
        public void WithQueryTest2()
        {
            Assert.AreEqual(
                new Uri("https://al-auth.kambicdn.org/player/api/v2/ubuk/coupon.json;jsessionid={jsessionid}?lang=en_GB&market={market}&client_id=2&channel_id=1&ncid={ncid}"),
                new Uri("https://al-auth.kambicdn.org/player/api/v2/ubuk/coupon.json;jsessionid={jsessionid}").WithQuery(new Dictionary<string, string>
                {
                    { "lang", "en_GB" },
                    { "market", "{market}" },
                    { "client_id", "2" },
                    { "channel_id", "1" },
                    { "ncid", "{ncid}" },
                }));
        }

        [TestMethod]
        public void WithQueryTest3()
        {
            Assert.AreEqual(
                new Uri("https://www.test.com/betslip/?op=13&ck=bs&betsource=FlashInPLay&streaming=1&fulltext=1&refreshbal=0&isocode=&bc=y&qb=0"),
                new Uri("https://www.test.com/betslip/").WithQuery(new Dictionary<string, string>
                {
                    { "op", "13" },
                    { "ck", "bs" },
                    { "betsource", "FlashInPLay" },
                    { "streaming", "1" },
                    { "fulltext", "1" },
                    { "refreshbal", "0" },
                    { "isocode", "" },
                    { "bc", "y" },
                    { "qb", "0" },
                }));
        }
    }
}
