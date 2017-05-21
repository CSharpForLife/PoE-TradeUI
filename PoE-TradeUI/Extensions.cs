using System.Windows;
using System.Windows.Media;
using PoE_TradeUI.ui;

namespace PoE_TradeUI {
    public static class Extensions {

        public static void AddImage(this DrawingGroup drawingGroup, Image image, Rect rect) {
            drawingGroup.Children.Add(new ImageDrawing(image.BitmapImage, rect));
        }

    }
}
