using System.Diagnostics;
using System.Windows;
using PoE_TradeUI.poe;

namespace PoE_TradeUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            var game = new Game();
            game.WindowStateChanged += GameOnWindowStateChanged;
        }

        private void GameOnWindowStateChanged(object sender, Native.Rect rect) {
            Debug.WriteLine($"Left: {rect.Left} - Top: {rect.Top} - Right: {rect.Right} - Bottom: {rect.Bottom}");
        }
    }
}
