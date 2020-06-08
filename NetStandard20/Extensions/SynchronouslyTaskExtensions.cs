using System.Threading.Tasks;

namespace NetStandard20.Extensions
{
    /// <summary>
    /// Synchronously task extensions. <br/>
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    public static class SynchronouslyTaskExtensions
    {
        /// <summary>
        /// Synchronously waits task to complete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T WaitSynchronously<T>(this Task<T> task)
        {
            return Task.Run(async () => await task).Result;
        }

        /// <summary>
        /// Synchronously waits task to complete.
        /// </summary>
        /// <param name="task"></param>
        public static void WaitSynchronously(this Task task)
        {
            Task.Run(async () => await task).Wait();
        }
    }
}
