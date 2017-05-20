using System;
using System.Windows;
using System.Windows.Threading;
using PoE_TradeUI.poe;

namespace PoE_TradeUI {

    public partial class MainWindow {

        private const double Ratio = 1.62;

        public Rect BgMargin => new Rect(0,0,0,0);

        public MainWindow() {
            InitializeComponent();
            Visibility = Visibility.Hidden;
            var game = new Game();
            game.WindowStateChanged += GameOnWindowStateChanged;
        }

        private void GameOnWindowStateChanged(object sender, Game.WindowState state) {
            var rect = state.Rect;

            if (!state.Open) {
                HideWindow();
                return;
            }

            if(Visibility == Visibility.Hidden) ShowWindow();

            if (rect != null) SetWindowBounds(rect.Value);
        }

        private void SetWindowBounds(Native.Rect rect) {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;

            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Left = rect.Left + SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left;
                Top = rect.Top + SystemParameters.CaptionHeight + SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left;
                Width = width - (SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left) * 2;
                Height = height - (SystemParameters.CaptionHeight + SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left) - (SystemParameters.WindowResizeBorderThickness.Left + SystemParameters.WindowNonClientFrameThickness.Left);
                SidePanel.Width = Height / Ratio;
                BackgroundTile.Viewport = new Rect(0,0, SidePanel.Width, 47);
            }));
        }

        private void ShowWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Visibility = Visibility.Visible;
            }));
        }

        private void HideWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Visibility = Visibility.Hidden;
            }));
        }

    }
}
