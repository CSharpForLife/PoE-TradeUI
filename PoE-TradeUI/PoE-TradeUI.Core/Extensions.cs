using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Core {
    public static class Extensions {

        public static void AddImage(this DrawingGroup drawingGroup, WpfImage image, double x, double y, bool scale = true) => AddImage(drawingGroup, image, x,y,scale ? image.ScaledWidth : image.Width, scale ? image.ScaledHeight : image.Height);

        public static void AddImage(this DrawingGroup drawingGroup, WpfImage image, double x, double y, double width, double height) => drawingGroup.Children.Add(new ImageDrawing(image.BitmapImage, new Rect(x, y, width, height)));

        public static WpfImage ToWpfImage(this ImageDef def) => new WpfImage(def);

        public static WinFormsImage ToWinFormsImage(this ImageDef def) => new WinFormsImage(def);

        public static T Deserialize<T>(this string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

        public static InteropBitmap Multiply(this BitmapImage bitmapImage, byte r, byte g, byte b, System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppArgb) => bitmapImage.ToBitmap().Multiply(r, g, b, format).ToBitmapImage();
        public static Bitmap Multiply(this Bitmap bitmap, byte r, byte g, byte b, System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppArgb) {
            var size = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var bitmapData = bitmap.LockBits(size, ImageLockMode.ReadOnly, format);

            var buffer = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, buffer, 0, buffer.Length);
            bitmap.UnlockBits(bitmapData);

            byte Calc(byte c1, byte c2)
            {
                var cr = c1 / 255d * c2 / 255d * 255d;
                return (byte)(cr > 255 ? 255 : cr);
            }

            for (var i = 0; i < buffer.Length; i += 4) {
                buffer[i] = Calc(buffer[i], b);
                buffer[i + 1] = Calc(buffer[i + 1], g);
                buffer[i + 2] = Calc(buffer[i + 2], r);
            }

            var result = new Bitmap(bitmap.Width, bitmap.Height);
            var resultData = result.LockBits(size, ImageLockMode.WriteOnly, format);

            Marshal.Copy(buffer, 0, resultData.Scan0, buffer.Length);
            result.UnlockBits(resultData);

            return result;
        }

        public static InteropBitmap ToBitmapImage(this Bitmap bitmap) {
            var handle = bitmap.GetHbitmap();
            try {
                return (InteropBitmap)Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero,Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally {
                if (handle != IntPtr.Zero) {
                    Native.DeleteObject(handle);
                }
            }
        }

        public static Bitmap ToBitmap(this BitmapImage bitmapImage) {
            using (var memoryStream = new MemoryStream()) {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);
                return new Bitmap(memoryStream);
            }
        }

    }
}
