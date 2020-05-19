using System;
using System.ComponentModel;

namespace NetCore31.Forms.Extensions
{
    /// <summary>
    /// Extensions that allow you to access the UI stream, if necessary.
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    public static class SynchronizeInvokeExtensions
    {
        /// <summary>
        /// Executes a method in a UI thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired<T>(this T obj, Action<T> action)
            where T : ISynchronizeInvoke
        {
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
        public static TOut InvokeIfRequired<TIn, TOut>(this TIn obj, Func<TIn, TOut> func)
            where TIn : ISynchronizeInvoke
        {
            return obj.InvokeRequired
                ? (TOut)obj.Invoke(func, new object[] { obj })
                : func(obj);
        }
    }
}