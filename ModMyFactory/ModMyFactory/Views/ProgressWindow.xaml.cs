﻿using System;
using System.Windows.Interop;
using System.Windows.Shell;
using ModMyFactory.Win32;

namespace ModMyFactory.Views
{
    partial class ProgressWindow
    {
        public ProgressWindow()
        {
            InitializeComponent();

            Loaded += LoadedHandler;
            Closing += ClosingHandler;
        }

        private void LoadedHandler(object sender, EventArgs e)
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            IntPtr windowLong = User32.GetWindowLong(handle, WindowLongIndex.Style);
            windowLong = (IntPtr)(windowLong.ToInt64() & (long)~WindowStyles.SystemMenu);
            User32.SetWindowLong(handle, WindowLongIndex.Style, windowLong);

            TaskbarItemInfo = new TaskbarItemInfo
            {
                ProgressState = ViewModel.IsIndeterminate ? TaskbarItemProgressState.Indeterminate : TaskbarItemProgressState.Normal,
                ProgressValue = ViewModel.Progress,
            };
        }

        private void ClosingHandler(object sender, EventArgs e)
        {
            TaskbarItemInfo.ProgressValue = 0;
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
            TaskbarItemInfo = null;
        }
    }
}
