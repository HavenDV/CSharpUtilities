using System;
using System.ComponentModel;

#nullable enable

namespace NetCore31.Forms.Extensions
{
    /// <summary>
    /// Extensions that allow you to access the UI thread, if necessary.
    /// <![CDATA[Version: 1.0.0.1]]> <br/>
    /// </summary>
    public static class SynchronizeInvokeExtensions
    {
        /// <summary>
        /// Executes a method in a UI thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired<T>(this T? obj, Action<T> action)
            where T : class, ISynchronizeInvoke
        {
            obj = obj ?? throw new ArgumentNullException(nameof(obj));

            if (!obj.InvokeRequired)
            {
                action(obj);
                return;
            }

            obj.Invoke(action, new object[] { obj });
        }

        /// <summary>
        /// Executes a method in a UI thread and returns value.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TOut InvokeIfRequired<TIn, TOut>(this TIn? obj, Func<TIn, TOut> func)
            where TIn : class, ISynchronizeInvoke
        {
            obj = obj ?? throw new ArgumentNullException(nameof(obj));

            return obj.InvokeRequired
                ? (TOut)obj.Invoke(func, new object[] { obj })
                : func(obj);
        }
    }
}