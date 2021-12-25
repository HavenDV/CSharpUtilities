using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable enable

namespace NetCore31.Forms.Extensions;

/// <summary>
/// Extensions that works with <see cref="Form"/>.
/// <![CDATA[Version: 1.0.0.0]]> <br/>
/// </summary>
public static class FormExtensions
{
    /// <summary>
    /// Shows form and waits when it has been closed.
    /// </summary>
    /// <param name="form"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static async Task ShowAsync(this Form form)
    {
        form = form ?? throw new ArgumentNullException(nameof(form));

        using var source = new CancellationTokenSource();

        // ReSharper disable once AccessToDisposedClosure
        void OnFormOnClosed(object? _, EventArgs args) => source.Cancel();

        try
        {
            form.Closed += OnFormOnClosed;

            form.Show();

            await Task.Delay(Timeout.InfiniteTimeSpan, source.Token);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            form.Closed -= OnFormOnClosed;
        }
    }
}
