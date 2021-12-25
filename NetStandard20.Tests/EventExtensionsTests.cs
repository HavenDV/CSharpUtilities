using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard20.Extensions;

namespace NetStandard20.Tests;

[TestClass]
public class EventExtensionsTests
{
    private class TestClass
    {
        public event EventHandler? CommonEvent;
        public event EventHandler? EventThatWillNeverHappen;
        public event EventHandler<int>? ValueTypeEvent;

        public void OnCommonEvent() => CommonEvent?.Invoke(this, EventArgs.Empty);
        public void OnValueTypeEvent() => ValueTypeEvent?.Invoke(this, 777);
    }

    [TestMethod]
    public async Task WaitEventAsyncTest()
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var cancellationToken = cancellationTokenSource.Token;

        var testObject = new TestClass();

        {
            var result = await testObject.WaitEventAsync<EventArgs>(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);

                testObject.OnCommonEvent();
            }, nameof(TestClass.CommonEvent), cancellationTokenSource.Token);

            Assert.IsNotNull(result, nameof(TestClass.CommonEvent));
            Assert.AreEqual(EventArgs.Empty, result, nameof(TestClass.CommonEvent));
        }

        {
            const string eventName = nameof(testObject.ValueTypeEvent);
            var result = await testObject.WaitEventAsync<int>(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);

                testObject.OnValueTypeEvent();
            }, eventName, cancellationTokenSource.Token);

            Assert.IsNotNull(result, eventName);
            Assert.AreEqual(777, result, eventName);
        }
    }

    [TestMethod]
    public async Task WaitAllEventsAsyncTest()
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var cancellationToken = cancellationTokenSource.Token;

        var testObject = new TestClass();

        var results = await testObject.WaitAllEventsAsync<EventArgs>(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);

            testObject.OnCommonEvent();
        }, cancellationTokenSource.Token, nameof(TestClass.CommonEvent), nameof(TestClass.EventThatWillNeverHappen));

        Assert.IsNotNull(results[nameof(TestClass.CommonEvent)], nameof(TestClass.CommonEvent));
        Assert.IsNull(results[nameof(TestClass.EventThatWillNeverHappen)], nameof(TestClass.EventThatWillNeverHappen));
    }

    [TestMethod]
    public async Task WaitAnyEventAsyncTest()
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var cancellationToken = cancellationTokenSource.Token;

        var testObject = new TestClass();

        var results = await testObject.WaitAnyEventAsync<EventArgs>(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1), cancellationToken);

            testObject.OnCommonEvent();
        }, cancellationTokenSource.Token, nameof(TestClass.CommonEvent), nameof(TestClass.EventThatWillNeverHappen));

        Assert.IsNotNull(results[nameof(TestClass.CommonEvent)], nameof(TestClass.CommonEvent));
        Assert.IsNull(results[nameof(TestClass.EventThatWillNeverHappen)], nameof(TestClass.EventThatWillNeverHappen));
    }

    [TestMethod]
    public async Task ExceptionsTest()
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        var cancellationToken = cancellationTokenSource.Token;

        var testObject = new TestClass();
        var nullObject = (TestClass?)null;

        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
                // ReSharper disable once ExpressionIsAlwaysNull
#pragma warning disable CS8604 // Possible null reference argument.
                await nullObject.WaitEventAsync<EventArgs>(nameof(TestClass.CommonEvent), cancellationToken);
#pragma warning restore CS8604 // Possible null reference argument.
            });

        await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                await testObject.WaitEventAsync<EventArgs>(null, cancellationToken);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });

        await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
        {
            await testObject.WaitEventAsync<EventArgs>("ThisEventNameIsNotExists", cancellationToken);
        });
    }
}
