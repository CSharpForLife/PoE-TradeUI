using System.Windows;
using System.Windows.Media;
using TradeUI.Utils;

namespace PoE_TradeUI.ui {

    public partial class Background  {

        private readonly WpfImage
        _backgroundPattern = new WpfImage("bg_pattern", 965, 50),
        _banner = new WpfImage("banner", 986, 132),
        _borderLeft = new WpfImage("border_left", 10, 50),
        _borderTop = new WpfImage("border_top", 50, 10),
        _borderRight = new WpfImage("border_right", 10, 50),
        _cornerTl = new WpfImage("corner_tl", 10, 10),
        _cornerTr = new WpfImage("corner_tr", 10, 10);

        private const double BannerMulti = .085;

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {

            var drawingGroup = new DrawingGroup();

            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.Height;
            for (var y = 0; y < bgCount; y++) {
                drawingGroup.AddImage(_backgroundPattern, new Rect(0, y * _backgroundPattern.Height, ActualWidth, _backgroundPattern.Height));
            }

            /*Paint Banner*/
            drawingGroup.AddImage(_banner, new Rect(0, _borderTop.Height, ActualWidth, ActualHeight * BannerMulti));

            /*Paint top border*/
            var topBorderCount = ActualWidth / _borderTop.Width + 1;
            for (var x = 0; x < topBorderCount; x++) {
                drawingGroup.AddImage(_borderTop, new Rect(x * _borderTop.Width - x, 0, _borderTop.Width, _borderTop.Height));
            }

            /*Paint left and right border*/
            var leftBorderCount = ActualHeight / _borderLeft.Height;
            for (var y = 0; y < leftBorderCount; y++) {
                drawingGroup.AddImage(_borderLeft, new Rect(0, y * _borderLeft.Height, _borderLeft.Width, _borderLeft.Height));
                drawingGroup.AddImage(_borderRight, new Rect(ActualWidth - _borderRight.Width, y * _borderRight.Height, _borderRight.Width, _borderRight.Height));
            }

            /*Paint corners*/
            drawingGroup.AddImage(_cornerTl, new Rect(0, 0, _cornerTl.Width, _cornerTl.Height));
            drawingGroup.AddImage(_cornerTr, new Rect(ActualWidth - _cornerTr.Width, 0, _cornerTr.Width, _cornerTr.Height));

            drawingGroup.Freeze();
            context.DrawDrawing(drawingGroup);
        }
    }
}
