using System;
using System.Drawing;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Core {
    public class WinFormsImage {
        public Bitmap Bitmap { get; }
        public string Name { get; }
        public string File { get; }
        public string Format { get; }
        public double Width { get; }
        public double ScaledWidth { get; }
        public double Height { get; }
        public double ScaledHeight { get; }
        public double ScaleX { get; }
        public double ScaleY { get; }

        public WinFormsImage(ImageDef imageDef, double? width = null, double? height = null) {
            var path = $"Resources/Images/{imageDef.File}.{imageDef.Format}";

            if (!System.IO.File.Exists(path)) throw new ApplicationException($"File does not exist {path}");

            Bitmap = new Bitmap(Image.FromFile(path));
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