using System;
using System.Windows.Media.Imaging;

namespace PoE_TradeUI.Utils {
    public class WpfImage {

        public BitmapImage BitmapImage { get; }
        public string Name { get; }
        public string File { get; }
        public string Format { get; }
        public double Width { get; }
        public double ScaledWidth { get; }
        public double Height { get; }
        public double ScaledHeight { get; }
        public double ScaleX { get; }
        public double ScaleY { get; }

        public WpfImage(ImageDef imageDef, double? width = null, double? height = null) {
            BitmapImage = new BitmapImage(new Uri($"Resources/Images/{imageDef.File}.{imageDef.Format}", UriKind.Relative));
            Name = imageDef.Name;
            File = imageDef.File;
            Format = imageDef.Format;
            Width = width ?? imageDef.Width;
            Height = height ?? imageDef.Height;
            ScaleX = imageDef.ScaleX;
            ScaleY = imageDef.ScaleY;
            ScaledWidth = Width * ScaleX;
            ScaledHeight = Height * ScaleY;
        }
    }
}
