using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable enable

namespace NetCore31.Forms.Forms
{
    /// <summary>
    /// Simple selection form.
    /// Allows double click selection.
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// </summary>
    public partial class SelectForm : Form
    {
        #region Static methods

        /// <summary>
        /// Shows dialog and runs action if it's not cancelled. <br/>
        /// Returns value or <see langword="null"/> if cancelled.
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static string? ShowDialogAndReturnResult(IList<string> strings, Action<string>? action = null)
        {
            strings = strings ?? throw new ArgumentNullException(nameof(strings));

            using var form = new SelectForm(strings);

            var result = form.ShowDialog() == DialogResult.OK
                ? form.SelectedItem
                : null;

            if (result != null)
            {
                action?.Invoke(result);
            }

            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Current selected item.
        /// </summary>
        public string? SelectedItem => ListBox.SelectedItem as string;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new form and sets <see cref="ListBox"/>.DataSource as <paramref name="strings"/>.
        /// </summary>
        /// <param name="strings"></param>
        public SelectForm(IList<string> strings)
        {
            InitializeComponent();

            ListBox.DataSource = strings;
        }

        #endregion

        #region Event Handlers

        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
