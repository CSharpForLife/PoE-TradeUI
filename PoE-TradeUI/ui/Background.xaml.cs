using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoE_TradeUI.ui {

    public partial class Background  {

        private readonly Image _backgroundPattern = new Image("bg-pattern-2");
        private readonly Image _banner = new Image("banner");
        private readonly Image _borderLeft = new Image("border-left-2");
        private readonly Image _borderTop = new Image("border-top");

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {


            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.Height;
            for (var y = 0; y < bgCount; y++) {
                context.DrawImage(_backgroundPattern.BitmapImage, new Rect(0, y * _backgroundPattern.Height - y, ActualWidth, _backgroundPattern.Height));
            }

            /*Paint banner*/
            var bannerHeight = ActualHeight * .08;
            context.DrawImage(_banner.BitmapImage, new Rect(0, _borderTop.Height, ActualWidth, bannerHeight));

            /*Paint left border*/
            var leftBorderCount = ActualHeight / _borderLeft.Height;
            for (var y = 0; y < leftBorderCount; y++) {
                context.DrawImage(_borderLeft.BitmapImage, new Rect(0, y * _borderLeft.Height - y, _borderLeft.Width, _borderLeft.Height));
            }

            /*Paint top border TODO: Corners*/
            var topBorderCount = ActualWidth / _borderTop.Width;
            for (var x = 0; x < topBorderCount; x++) {
                context.DrawImage(_borderTop.BitmapImage, new Rect(x * _borderTop.Width - x, 0, _borderTop.Width, _borderTop.Height));
            }

            
            /*var tradeUiText = new FormattedText("TradeUI", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("Arial"), 20, Brushes.Black);
            
            context.DrawText(tradeUiText, new Point(ActualWidth / 2 - tradeUiText.Width / 2, bannerHeight / 2 + _borderTop.Height));*/

            
        }
    }
}
