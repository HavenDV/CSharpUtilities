using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Utilities;

namespace NetStandard20.Tests
{
    [TestClass]
    public class NetUtilitiesTests
    {
        [TestMethod]
        public async Task ReadGetRequestDataAsyncTest()
        {
            Assert.IsNotNull(await NetUtilities.ReadGetRequestDataAsync(new Uri("https://postman-echo.com/get")));

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(
                async () => await NetUtilities.ReadGetRequestDataAsync(new Uri("https://postman-echo.com/get"), TimeSpan.FromMilliseconds(1)));
        }
    }
}
