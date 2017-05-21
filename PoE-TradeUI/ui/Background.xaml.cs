using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace PoE_TradeUI.ui {

    public partial class Background  {

        private readonly Image _backgroundPattern = new Image("bg-pattern-2", 965, 50);
        private readonly Image _banner = new Image("banner", 986, 132);
        private readonly Image _borderLeft = new Image("border-left-hr3", 10, 50);
        private readonly Image _borderTop = new Image("border-top-hr3", 50, 10);
        private readonly Image _borderRight = new Image("border-right-hr3", 10, 50);
        private readonly Image _cornerTl = new Image("corner-tl-hr3", 10, 10);
        private readonly Image _cornerTr = new Image("corner-tr-hr3", 10, 10);

        private const double BannerMulti = .08;

        public Background() {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext context) {

            var drawingGroup = new DrawingGroup();

            /*Paint background*/
            var bgCount = ActualHeight / _backgroundPattern.Height;
            for (var y = 0; y < bgCount; y++) {
                drawingGroup.Children.Add(new ImageDrawing(_backgroundPattern.BitmapImage, new Rect(0, y * _backgroundPattern.Height, ActualWidth, _backgroundPattern.Height)));
            }

            /*Paint Banner*/
            drawingGroup.Children.Add(new ImageDrawing(_banner.BitmapImage, new Rect(0, _borderTop.Height, ActualWidth, ActualHeight * BannerMulti)));

            /*Paint top border*/
            var topBorderCount = ActualWidth / _borderTop.Width;
            for (var x = 0; x < topBorderCount; x++) {
                drawingGroup.Children.Add(new ImageDrawing(_borderTop.BitmapImage, new Rect(x * _borderTop.Width - x, 0, _borderTop.Width, _borderTop.Height)));
            }

            /*Paint left and right border*/
            var leftBorderCount = ActualHeight / _borderLeft.Height;
            for (var y = 0; y < leftBorderCount; y++) {
                drawingGroup.Children.Add(new ImageDrawing(_borderLeft.BitmapImage, new Rect(0, y * _borderLeft.Height, _borderLeft.Width, _borderLeft.Height)));
                drawingGroup.Children.Add(new ImageDrawing(_borderRight.BitmapImage, new Rect(ActualWidth - _borderRight.Width, y * _borderRight.Height, _borderRight.Width, _borderRight.Height)));
            }

            /*Paint corners*/
            drawingGroup.Children.Add(new ImageDrawing(_cornerTl.BitmapImage, new Rect(0, 0, _cornerTl.Width, _cornerTl.Height)));
            drawingGroup.Children.Add(new ImageDrawing(_cornerTr.BitmapImage, new Rect(ActualWidth - _cornerTr.Width, 0, _cornerTr.Width, _cornerTr.Height)));

            drawingGroup.Freeze();
            context.DrawDrawing(drawingGroup);
        }
    }
}
