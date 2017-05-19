using System;
using System.Windows;
using System.Windows.Threading;
using PoE_TradeUI.poe;

namespace PoE_TradeUI {

    public partial class MainWindow {

        public MainWindow() {
            InitializeComponent();
            var game = new Game();
            game.WindowStateChanged += GameOnWindowStateChanged;
        }

        private void GameOnWindowStateChanged(object sender, Native.Rect rect) {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;

            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {

                var x = rect.Left + SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left;
                var y = rect.Top + SystemParameters.CaptionHeight + SystemParameters.WindowResizeBorderThickness.Left +               SystemParameters.WindowNonClientFrameThickness.Left;
                var w = width - (SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left) * 2;
                var h = height - (SystemParameters.CaptionHeight + SystemParameters.WindowResizeBorderThickness.Left +
                                   SystemParameters.WindowNonClientFrameThickness.Left) - (SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left);

                if (Left != x) Left = x;
                if (Top != y) Top = y;
                if (Width != w) Width = w;
                if (Height != h) Height = h;

            }));
        }
    }
}
