using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoE_TradeUI.ui {

    public partial class Background  {

       private readonly Image _backgroundPattern = new Image("bg-pattern");

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {
            var bgCount = ActualHeight / _backgroundPattern.Height;

            for (var y = 0; y < bgCount; y++) {
                context.DrawImage(_backgroundPattern.BitmapImage, new Rect(0, y * _backgroundPattern.Height - y*1, ActualWidth, _backgroundPattern.Height));
            }
        }
    }
}
