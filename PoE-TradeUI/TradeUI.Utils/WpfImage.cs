using System;
using System.Windows.Media.Imaging;

namespace TradeUI.Utils {
    public class WpfImage {

        public BitmapImage BitmapImage { get; }
        public double Width { get; }
        public double Height { get; }

        public WpfImage(string name, string extension = "png") : this(name, null, null, extension) { }
        public WpfImage(string name, double? width, double? height, string extension = "png") {
            BitmapImage = new BitmapImage(new Uri($"pack://application:,,,/TradeUI.Utils;component/Resources/Images/{name}.{extension}"));
            Width = width ?? BitmapImage.Width;
            Height = height ?? BitmapImage.Height;
        }

    }
}
