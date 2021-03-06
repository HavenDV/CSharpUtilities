﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetStandard21.Utilities;

namespace NetStandard21.Tests
{
    [TestClass]
    public class TaskWorkerTests
    {
        #region Properties

        public static int WorkersCount { get; } = 32;

        #endregion

        #region Dispose

        [TestMethod]
        public async Task NotStartedDisposeAsyncTest()
        {
            await using var worker = new TaskWorker();
        }

        [TestMethod]
        public void NotStartedDisposeTest()
        {
            using var worker = new TaskWorker();
        }

        [TestMethod]
        public async Task CompletedTaskDisposeAsyncTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.CompletedTask);
        }

        [TestMethod]
        public void CompletedTaskDisposeTest()
        {
            using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.CompletedTask);
        }

        [TestMethod]
        public async Task OneSecondDelayTaskDisposeAsyncTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.Delay(TimeSpan.FromSeconds(1), cancellationToken));
        }

        [TestMethod]
        public void OneSecondDelayTaskDisposeTest()
        {
            using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.Delay(TimeSpan.FromSeconds(1), cancellationToken));
        }

        [TestMethod]
        public async Task OneSecondDelayTaskWithoutTokenDisposeAsyncTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.Delay(TimeSpan.FromSeconds(1), cancellationToken));
        }

        [TestMethod]
        public void OneSecondDelayTaskWithoutTokenDisposeTest()
        {
            using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.Delay(TimeSpan.FromSeconds(1), cancellationToken));
        }

        [TestMethod]
        public void SleepTaskDisposeTest()
        {
            using var worker = new TaskWorker();

            worker.Start(cancellationToken =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                return Task.CompletedTask;
            });
        }

        [TestMethod]
        public async Task SleepTaskDisposeAsyncTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                return Task.CompletedTask;
            });
        }

        [TestMethod]
        public async Task MultiSleepTaskDisposeAsyncTest()
        {
            var workers = Enumerable
                .Range(0, WorkersCount)
                .Select(_ => new TaskWorker())
                .ToArray();

            Parallel.ForEach(workers, worker =>
            {
                worker.Start(cancellationToken =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    return Task.CompletedTask;
                });
            });

            await Task.WhenAll(workers
                .Select(worker => worker.DisposeAsync().AsTask()));
        }

        [TestMethod]
        public async Task MultiCompletedTaskDisposeAsyncTest()
        {
            var workers = Enumerable
                .Range(0, WorkersCount)
                .Select(_ => new TaskWorker())
                .ToArray();

            Parallel.ForEach(workers, worker =>
            {
                worker.Start(cancellationToken => Task.CompletedTask);
            });

            await Task.WhenAll(workers
                .Select(worker => worker.DisposeAsync().AsTask()));
        }

        #endregion

        [TestMethod]
        public async Task OneSecondDelayTaskTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken => Task.Delay(TimeSpan.FromSeconds(1), cancellationToken));

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public async Task SleepTaskAsyncTest()
        {
            await using var worker = new TaskWorker();

            worker.Start(cancellationToken =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                return Task.CompletedTask;
            });

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void SleepTaskTest()
        {
            using var worker = new TaskWorker();

            worker.Start(cancellationToken =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));

                return Task.CompletedTask;
            });

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void SleepTaskMultiTest()
        {
            var workers = Enumerable
                .Range(0, WorkersCount)
                .Select(_ => new TaskWorker())
                .ToArray();

            Parallel.ForEach(workers, worker =>
            {
                worker.Start(cancellationToken =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    return Task.CompletedTask;
                });
            });

            Thread.Sleep(TimeSpan.FromSeconds(1));

            foreach (var worker in workers)
            {
                worker.Dispose();
            }
        }

        [TestMethod]
        public async Task SleepTaskMultiAsyncTest()
        {
            var workers = Enumerable
                .Range(0, WorkersCount)
                .Select(_ => new TaskWorker())
                .ToArray();

            Parallel.ForEach(workers, worker =>
            {
                worker.Start(cancellationToken =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    return Task.CompletedTask;
                });
            });

            await Task.Delay(TimeSpan.FromSeconds(1));

            await Task.WhenAll(workers
                .Select(worker => worker.DisposeAsync().AsTask()));
        }
    }
}
