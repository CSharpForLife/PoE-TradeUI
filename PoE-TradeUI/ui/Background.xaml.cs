using System.Windows;
using System.Windows.Media;

namespace PoE_TradeUI.ui {

    public partial class Background  {

        private readonly Image _backgroundPattern = new Image("bg-pattern-2");
        private readonly Image _banner = new Image("banner");
        private readonly Image _borderLeft = new Image("border-left-hr");
        private readonly Image _borderTop = new Image("border-top-hr");
        private readonly Image _borderRight = new Image("border-right-hr");
        private readonly Image _cornerTl = new Image("corner-tl-hr");
        private readonly Image _cornerTr = new Image("corner-tr-hr");

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {


            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.Height;
            for (var y = 0; y < bgCount; y++) {
                context.DrawImage(_backgroundPattern.BitmapImage, new Rect(0, y * _backgroundPattern.Height - y + 1, ActualWidth, _backgroundPattern.Height));
            }

            /*Paint banner*/
            var bannerHeight = ActualHeight * .08;
            context.DrawImage(_banner.BitmapImage, new Rect(0, _borderTop.Height, ActualWidth, bannerHeight));

            /*Paint left and right border*/
            var leftBorderCount = ActualHeight / _borderLeft.Height;
            for (var y = 0; y < leftBorderCount; y++) {
                context.DrawImage(_borderLeft.BitmapImage, new Rect(0, y * _borderLeft.Height - y + 1, _borderLeft.Width, _borderLeft.Height));
                context.DrawImage(_borderRight.BitmapImage, new Rect(ActualWidth - _borderRight.Width, y * _borderRight.Height - y + 1, _borderRight.Width, _borderRight.Height));
            }

            /*Paint top border*/
            var topBorderCount = ActualWidth / _borderTop.Width;
            for (var x = 0; x < topBorderCount; x++) {
                context.DrawImage(_borderTop.BitmapImage, new Rect(x * _borderTop.Width - x, 0, _borderTop.Width, _borderTop.Height));
            }

            /*Paint corners*/
            context.DrawImage(_cornerTl.BitmapImage, new Rect(1, 0, _cornerTl.Width, _cornerTl.Height));
            context.DrawImage(_cornerTr.BitmapImage, new Rect(ActualWidth - _cornerTr.Width, 0, _cornerTr.Width, _cornerTr.Height));

            
            /*var tradeUiText = new FormattedText("TradeUI", CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("Arial"), 20, Brushes.Black);
            
            context.DrawText(tradeUiText, new Point(ActualWidth / 2 - tradeUiText.Width / 2, bannerHeight / 2 + _borderTop.Height));*/

            
        }
    }
}
