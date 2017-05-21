﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using PoE_TradeUI.Utils;

namespace PoE_TradeUI.Wpf {

    public partial class MainWindow {

        public MainWindow() {
            Defs.Init();

            InitializeComponent();
            Opacity = 0;
            var poeGame = new PoeGame(new WindowInteropHelper(this).EnsureHandle());
            poeGame.WindowStateChanged += PoeGameOnWindowStateChanged;
        }

        private void PoeGameOnWindowStateChanged(object sender, PoeGame.WindowState state) {
            var rect = state.Rect;

            if (!state.Open || !state.TopMost) {
                HideWindow();
                return;
            }

            ShowWindow();

            if (rect != null) SetWindowBounds(rect.Value);
        }

        private void SetWindowBounds(Native.Rect rect) {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;

            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Left = rect.Left + Constants.Wpf.Ui.BorderWidth;
                Top = rect.Top + Constants.Wpf.Ui.CaptionHeight;
                Width = width - Constants.Wpf.Ui.BorderWidth * 2;
                Height = height - Constants.Wpf.Ui.CaptionHeight - Constants.Wpf.Ui.BorderWidth;
                SidePanel.Width = Height / Constants.Wpf.Ui.ScaleRatio;

                ((ImageBrush)Background.OpacityMask).Viewport = new Rect(0,0,SidePanel.Width,Height);
                //TODO use banner values instead for button position
                /*BtnClose.Width = Height * .025;
                BtnClose.Height = BtnClose.Width;
                BtnClose.Margin = new Thickness(0, Height * .05, Height * .02, 0);*/
            }));
        }

        private void ShowWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Opacity = 100;
            }));
        }

        private void HideWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Opacity = 0;
            }));
        }
    }
}