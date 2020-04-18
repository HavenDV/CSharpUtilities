using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests
{
    [TestClass]
    public class ProcessExtensionsTests
    {
        [TestMethod]
        public async Task WaitTest()
        {
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            using var process = Process.Start(new ProcessStartInfo("timeout", "2")
            {
                UseShellExecute = true,
            });
            if (process == null)
            {
                throw new InvalidOperationException("Process is null");
            }

            await process.WaitForExitAsync(source.Token);
        }
    }
}
