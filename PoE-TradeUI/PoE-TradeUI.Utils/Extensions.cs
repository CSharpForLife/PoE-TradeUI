using System.Windows;
using System.Windows.Media;

namespace PoE_TradeUI.Utils {
    public static class Extensions {

        public static void AddImage(this DrawingGroup drawingGroup, WpfImage image, double x, double y, bool scale = true) {
            AddImage(drawingGroup, image, x,y,scale ? image.ScaledWidth : image.Width, scale ? image.ScaledHeight : image.Height);
        }
        public static void AddImage(this DrawingGroup drawingGroup, WpfImage image, double x, double y, double width, double height) {
            drawingGroup.Children.Add(new ImageDrawing(image.BitmapImage, new Rect(x, y, width, height)));
        }

        public static WpfImage ToWpfImage(this ImageDef def) {
            return new WpfImage(def);
        }

        public static WinFormsImage ToWinFormsImage(this ImageDef def) {
            return new WinFormsImage(def);
        }

    }
}
