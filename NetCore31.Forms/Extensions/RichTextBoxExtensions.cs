using System;
using System.Windows.Forms;

#nullable enable

namespace NetCore31.Forms.Extensions;

/// <summary>
/// <see cref="RichTextBox"/> Extensions.
/// <![CDATA[Version: 1.0.0.0]]> <br/>
/// </summary>
public static class RichTextBoxExtensions
{
    /// <summary>
    /// Scrolls to end.
    /// </summary>
    /// <param name="richTextBox"></param>
    public static void ScrollToEnd(this RichTextBox richTextBox)
    {
        richTextBox = richTextBox ?? throw new ArgumentNullException(nameof(richTextBox));

        richTextBox.SelectionStart = richTextBox.Text.Length;
        richTextBox.ScrollToCaret();
    }
}
