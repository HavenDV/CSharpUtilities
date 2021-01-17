﻿using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace NetCore31.Wpf.Extensions
{
    /// <summary>
    /// <see cref="Window"/> extensions.
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Moves the window to the center of the current screen, also considering dpi.
        /// </summary>
        /// <param name="window"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void MoveToCenter(this Window window)
        {
            window = window ?? throw new ArgumentNullException(nameof(window));

            var helper = new WindowInteropHelper(window);
            var screen = Screen.FromHandle(helper.Handle);
            var area = screen.WorkingArea;

            var source = PresentationSource.FromVisual(window);
            var dpi = source?.CompositionTarget?.TransformFromDevice.M11 ?? 1.0;

            window.Left = dpi * area.Left + (dpi * area.Width - window.Width) / 2;
            window.Top = dpi * area.Top + (dpi * area.Height - window.Height) / 2;
        }
    }
}
