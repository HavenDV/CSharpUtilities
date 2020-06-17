using System;
using System.Windows.Threading;

#nullable enable

namespace NetCore31.Wpf.Extensions
{
    /// <summary>
    /// Extensions that allow you to access the UI thread, if necessary.
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        /// Executes a method in a UI thread.
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired(this Dispatcher dispatcher, Action action)
        {
            dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            action = action ?? throw new ArgumentNullException(nameof(action));

            // CheckAccess returns true if you're on the dispatcher thread
            if (dispatcher.CheckAccess())
            {
                action();
                return;
            }

            dispatcher.Invoke(action);
        }

        /// <summary>
        /// Executes a method in a UI thread and returns value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dispatcher"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T InvokeIfRequired<T>(this Dispatcher dispatcher, Func<T> func)
        {
            dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            func = func ?? throw new ArgumentNullException(nameof(func));

            // CheckAccess returns true if you're on the dispatcher thread
            return dispatcher.CheckAccess()
                ? func()
                : dispatcher.Invoke(func);
        }
    }
}