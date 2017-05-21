using System.Windows;
using System.Windows.Media;
using PoE_TradeUI.Utils;

namespace PoE_TradeUI.Wpf.ui {

    public partial class Background  {

        private readonly WpfImage
        _backgroundPattern = new WpfImage(Defs.GetImageDefByName("Background Pattern")),
        _banner = new WpfImage(Defs.GetImageDefByName("Banner")),
        _borderLeft = new WpfImage(Defs.GetImageDefByName("Border Left")),
        _borderRight = new WpfImage(Defs.GetImageDefByName("Border Right")),
        _borderTop = new WpfImage(Defs.GetImageDefByName("Border Top")),
        _cornerTl = new WpfImage(Defs.GetImageDefByName("Corner TL")),
        _cornerTr = new WpfImage(Defs.GetImageDefByName("Corner TR"));

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {

            var drawingGroup = new DrawingGroup();

            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.ScaledHeight;
            for (var y = 0; y < bgCount; y++) {
                drawingGroup.AddImage(_backgroundPattern, new Rect(0, y * _backgroundPattern.Height, ActualWidth * _backgroundPattern.ScaleX, _backgroundPattern.ScaledHeight));
            }

            /*Paint Banner*/
            drawingGroup.AddImage(_banner, new Rect(0, _borderTop.Height, ActualWidth * _banner.ScaleX, ActualHeight * _banner.ScaleY));

            /*Paint top border*/
            var topBorderCount = ActualWidth / _borderTop.ScaledWidth + 1;
            for (var x = 0; x < topBorderCount; x++) {
                drawingGroup.AddImage(_borderTop, new Rect(x * _borderTop.Width - x, 0, _borderTop.ScaledWidth, _borderTop.ScaledHeight));
            }

            /*Paint left and right border*/
            var leftBorderCount = ActualHeight / _borderLeft.ScaledHeight;
            for (var y = 0; y < leftBorderCount; y++) {
                drawingGroup.AddImage(_borderLeft, new Rect(0, y * _borderLeft.Height, _borderLeft.ScaledWidth, _borderLeft.ScaledHeight));
                drawingGroup.AddImage(_borderRight, new Rect(ActualWidth - _borderRight.Width, y * _borderRight.Height, _borderRight.ScaledWidth, _borderRight.ScaledHeight));
            }

            /*Paint corners*/
            drawingGroup.AddImage(_cornerTl, new Rect(0, 0, _cornerTl.ScaledWidth, _cornerTl.ScaledHeight));
            drawingGroup.AddImage(_cornerTr, new Rect(ActualWidth - _cornerTr.Width, 0, _cornerTr.ScaledWidth, _cornerTr.ScaledHeight));

            drawingGroup.Freeze();
            context.DrawDrawing(drawingGroup);
        }
    }
}
