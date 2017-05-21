using System.Windows;
using System.Windows.Media;

namespace PoE_TradeUI.Utils {
    public static class Extensions {

        public static void AddImage(this DrawingGroup drawingGroup, WpfImage image, Rect rect) {
            drawingGroup.Children.Add(new ImageDrawing(image.BitmapImage, rect));
        }

    }
}
