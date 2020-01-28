﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NetStandard20.Extensions
{
    /// <summary>
    /// Extensions that work with <see langword="event"/> <br/>
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    public static class EventExtensions
    {
        private class WaitObject
        {
            public TaskCompletionSource<EventArgs?>? Source { get; set; }

            // ReSharper disable UnusedParameter.Local
            public void HandleEvent(object sender, EventArgs e)
            {
                Source?.TrySetResult(e);
            }
        }

        /// <summary>
        /// Asynchronously expects <see langword="event"/> until they occur or until canceled <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// <![CDATA[Dependency: WaitObject]]> <br/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="eventName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<EventArgs?> WaitEventAsync(this object value, string eventName, CancellationToken cancellationToken = default)
        {
            var taskCompletionSource = new TaskCompletionSource<EventArgs?>();
            using var cancellationSource = new CancellationTokenSource();

            cancellationSource.Token.Register(() => taskCompletionSource.TrySetCanceled());
            cancellationToken.Register(() => taskCompletionSource.TrySetCanceled());

            var waitObject = new WaitObject
            {
                Source = taskCompletionSource,
            };
            var method = typeof(WaitObject).GetMethod(nameof(WaitObject.HandleEvent)) ?? throw new InvalidOperationException("Method not found");
            var eventInfo = value.GetType().GetEvent(eventName) ?? throw new InvalidOperationException("Event info not found");
            // ReSharper disable once ConstantNullCoalescingCondition
            var eventHandlerType = eventInfo.EventHandlerType ?? throw new InvalidOperationException("Event Handler Type not found");
            var delegateObject = Delegate.CreateDelegate(eventHandlerType, waitObject, method, true);

            try
            {
                eventInfo.AddEventHandler(value, delegateObject);

                return await taskCompletionSource.Task.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            finally
            {
                eventInfo.RemoveEventHandler(value, delegateObject);
            }
        }

        /// <summary>
        /// Asynchronously expects <see langword="event"/> until they occur or until canceled <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// <![CDATA[Dependency: WaitEventAsync(this object value, string eventName, CancellationToken cancellationToken = default)]]> <br/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <param name="eventName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<EventArgs?> WaitEventAsync(this object value, Func<CancellationToken, Task> func, string eventName, CancellationToken cancellationToken = default)
        {
            try
            {
                var task = value.WaitEventAsync(eventName, cancellationToken);

                await func(cancellationToken).ConfigureAwait(false);

                return await task.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                return null;
            }
        }

        /// <summary>
        /// Asynchronously expects all <see langword="event"/>'s until they occur or until canceled <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// <![CDATA[Dependency: WaitEventAsync(this object value, string eventName, CancellationToken cancellationToken = default)]]> <br/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="eventNames"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, EventArgs?>> WaitAllEventsAsync(this object value, Func<CancellationToken, Task> func, CancellationToken cancellationToken = default, params string[] eventNames)
        {
            var tasks = eventNames
                .Select(name => value.WaitEventAsync(name, cancellationToken))
                .ToList();

            try
            {
                await func(cancellationToken).ConfigureAwait(false);

                await Task.WhenAll(tasks).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }

            return eventNames
                .Zip(tasks, (name, task) => (name, task))
                .ToDictionary(i => i.name, i => i.task.IsCompleted && !i.task.IsCanceled ? i.task.Result : null);
        }

        /// <summary>
        /// Asynchronously expects any <see langword="event"/> until it occurs or until canceled <br/>
        /// <![CDATA[Version: 1.0.0.0]]> <br/>
        /// <![CDATA[Dependency: WaitEventAsync(this object value, string eventName, CancellationToken cancellationToken = default)]]> <br/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="eventNames"></param>
        /// <returns></returns>
        public static async Task<Dictionary<string, EventArgs?>> WaitAnyEventAsync(this object value, Func<CancellationToken, Task> func, CancellationToken cancellationToken = default, params string[] eventNames)
        {
            var tasks = eventNames
                .Select(name => value.WaitEventAsync(name, cancellationToken))
                .ToList();

            try
            {
                await func(cancellationToken).ConfigureAwait(false);

                await Task.WhenAny(tasks).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }

            return eventNames
                .Zip(tasks, (name, task) => (name, task))
                .ToDictionary(i => i.name, i => i.task.IsCompleted && !i.task.IsCanceled ? i.task.Result : null);
        }
    }
}
