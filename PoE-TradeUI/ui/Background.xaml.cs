using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoE_TradeUI.ui {

    public partial class Background  {

        private readonly Image _backgroundPattern = new Image("bg-pattern");
        private readonly Image _banner = new Image("banner-test");

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {
            var bgCount = ActualHeight / _backgroundPattern.Height;

            for (var y = 0; y < bgCount; y++) {
                context.DrawImage(_backgroundPattern.BitmapImage, new Rect(0, y * _backgroundPattern.Height - y*1, ActualWidth, _backgroundPattern.Height));
            }

            var bannerHeight = ActualHeight * .08;

            context.DrawImage(_banner.BitmapImage, new Rect(0,0, ActualWidth, bannerHeight));
            var tradeUiText = new FormattedText("TradeUI", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("Arial"), 20, Brushes.White);
            
            context.DrawText(tradeUiText, new Point(ActualWidth / 2 - tradeUiText.Width / 2, bannerHeight / 2));
        }
    }
}
