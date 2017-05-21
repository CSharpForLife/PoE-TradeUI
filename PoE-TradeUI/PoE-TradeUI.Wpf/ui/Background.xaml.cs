using System.Windows;
using System.Windows.Media;
using PoE_TradeUI.Utils;

namespace PoE_TradeUI.Wpf.ui {

    public partial class Background  {

        private readonly WpfImage
        _backgroundPattern = Defs.GetImageDefByName("Background Pattern").ToWpfImage(),
        _banner = Defs.GetImageDefByName("Banner").ToWpfImage(),
        _borderLeft = Defs.GetImageDefByName("Border Left").ToWpfImage(),
        _borderRight = Defs.GetImageDefByName("Border Right").ToWpfImage(),
        _borderTop = Defs.GetImageDefByName("Border Top").ToWpfImage(),
        _cornerTl = Defs.GetImageDefByName("Corner TL").ToWpfImage(),
        _cornerTr = Defs.GetImageDefByName("Corner TR").ToWpfImage();

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {

            var drawingGroup = new DrawingGroup();
            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.ScaledHeight;
            for (var y = 0; y < bgCount; y++) {
                drawingGroup.AddImage(_backgroundPattern, 0, y * _backgroundPattern.Height, ActualWidth * _backgroundPattern.ScaleX, _backgroundPattern.ScaledHeight);
            }

            /*Paint Banner*/
            drawingGroup.AddImage(_banner, 0, _borderTop.Height, ActualWidth * _banner.ScaleX, ActualHeight * _banner.ScaleY);

            /*Paint top border*/
            var topBorderCount = ActualWidth / _borderTop.ScaledWidth + 1;
            for (var x = 0; x < topBorderCount; x++) {
                drawingGroup.AddImage(_borderTop, x * _borderTop.Width - x, 0);
            }

            /*Paint left and right border*/
            var leftBorderCount = ActualHeight / _borderLeft.ScaledHeight;
            for (var y = 0; y < leftBorderCount; y++) {
                drawingGroup.AddImage(_borderLeft, 0, y * _borderLeft.Height);
                drawingGroup.AddImage(_borderRight, ActualWidth - _borderRight.Width, y * _borderRight.Height);
            }

            /*Paint corners*/
            drawingGroup.AddImage(_cornerTl, 0, 0);
            drawingGroup.AddImage(_cornerTr, ActualWidth - _cornerTr.Width, 0);

            drawingGroup.Freeze();
            context.DrawDrawing(drawingGroup);
        }
    }
}
