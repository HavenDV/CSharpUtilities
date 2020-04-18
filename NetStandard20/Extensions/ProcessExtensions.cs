using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NetStandard20.Extensions
{
    public static class ProcessExtensions
    {
        /// <summary>
        /// Waits to complete the process.
        /// </summary>
        /// <param name="process"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <returns></returns>
        public static async Task<int> WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            process = process ?? throw new ArgumentNullException(nameof(process));
            process.EnableRaisingEvents = true;

            var completionSource = new TaskCompletionSource<int>();

            process.Exited += (sender, args) =>
            {
                completionSource.TrySetResult(process.ExitCode);
            };
            if (process.HasExited)
            {
                return process.ExitCode;
            }

            using var registration = cancellationToken.Register(
                () => completionSource.TrySetCanceled(cancellationToken));

            return await completionSource.Task.ConfigureAwait(false);
        }
    }
}
