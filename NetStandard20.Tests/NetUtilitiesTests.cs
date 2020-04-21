using System;
using System.Threading;
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
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var cancellationToken = cancellationTokenSource.Token;

            Assert.IsNotNull(
                await NetUtilities.ReadGetRequestDataAsync(
                    new Uri("https://postman-echo.com/get"), 
                    cancellationToken: cancellationToken));
        }

        [TestMethod]
        public async Task ReadGetRequestDataAsyncTimeoutTest()
        {
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var cancellationToken = cancellationTokenSource.Token;

            await Assert.ThrowsExceptionAsync<TaskCanceledException>(
                async () => await NetUtilities.ReadGetRequestDataAsync(
                    new Uri("https://postman-echo.com/get"), 
                    TimeSpan.FromMilliseconds(1), 
                    cancellationToken));
        }
    }
}
