using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PoE_TradeUI.ui {
    internal class Image {

        public BitmapImage BitmapImage { get; }
        public double Width { get; }
        public double Height { get; }

        public Image(string name, string extension = "png") : this(name, null, null, extension) {}
        public Image(string name, double? width, double? height, string extension = "png") {
            BitmapImage = new BitmapImage(new Uri($"pack://application:,,,/PoE-TradeUI;component/Images/{name}.{extension}"));
            Width = width ?? BitmapImage.Width;
            Height = height ?? BitmapImage.Height;
        }

    }
}