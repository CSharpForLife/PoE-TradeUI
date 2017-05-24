using System.IO;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Core {
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

        public static T Deserialize<T>(this string path) {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }

    }
}
