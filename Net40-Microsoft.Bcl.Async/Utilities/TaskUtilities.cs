using System;
using System.Collections.Generic;
using System.Threading;
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
        public static TimeSpan InfiniteTimeSpan { get; } = new TimeSpan(0, 0, 0, 0, -1);

        public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
        {
#if NET40
            return TaskEx.WhenAll(tasks);
#else
            return Task.WhenAll(tasks);
#endif
        }

        public static Task Run(Action action, CancellationToken cancellationToken = default)
        {
#if NET40
            return TaskEx.Run(action, cancellationToken);
#else
            return Task.Run(action, cancellationToken);
#endif
        }

        public static Task<T> Run<T>(Func<T> func, CancellationToken cancellationToken = default)
        {
#if NET40
            return TaskEx.Run(func, cancellationToken);
#else
            return Task.Run(func, cancellationToken);
#endif
        }

        public static Task Delay(TimeSpan delay, CancellationToken cancellationToken = default)
        {
#if NET40
            return TaskEx.Delay(delay, cancellationToken);
#else
            return Task.Delay(delay, cancellationToken);
#endif
        }

        public static Task<TResult> FromResult<TResult>(TResult result)
        {
#if NET40
            return TaskEx.FromResult(result);
#else
            return Task.FromResult(result);
#endif
        }

        public static Task CompletedTask => Delay(TimeSpan.FromMilliseconds(0));
    }
}
