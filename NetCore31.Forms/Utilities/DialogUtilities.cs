using System.Windows.Forms;

#nullable enable

namespace NetCore31.Forms.Utilities
{
    /// <summary>
    /// Simple dialog utilities.
    /// </summary>
    public static class DialogUtilities
    {
        /// <summary>
        /// Opens <see cref="SaveFileDialog"/>. Returns <see langword="null"/> if cancelled.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string? Save(string? extension = null, string? fileName = null)
        {
            using var dialog = new SaveFileDialog
            {
                DefaultExt = extension ?? string.Empty,
                FileName = fileName ?? string.Empty,
            };

            // Check write access

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.FileName
                : null;

        }

        /// <summary>
        /// Opens <see cref="OpenFileDialog"/>. Returns <see langword="null"/> if cancelled.
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string? Open(string? extension = null, string ? fileName = null)
        {
            using var dialog = new OpenFileDialog
            {
                DefaultExt = extension ?? string.Empty,
                FileName = fileName ?? string.Empty,
            };

            return dialog.ShowDialog() == DialogResult.OK
                ? dialog.FileName
                : null;
        }
    }
}
