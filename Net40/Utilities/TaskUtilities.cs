using System;
using System.Threading.Tasks;

#nullable enable

namespace Net40.Utilities
{
    /// <summary>
    /// Full Task support for net40.
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    internal static class TaskUtilities
    {
        public static Task<TResult> FromResult<TResult>(TResult result)
        {
#if NET40
            var source = new TaskCompletionSource<TResult>(result);
            source.TrySetResult(result);

            return source.Task;
#else
            return Task.FromResult(result);
#endif
        }

        public static TimeSpan InfiniteTimeSpan { get; } = new TimeSpan(0, 0, 0, 0, -1);
    }
}
