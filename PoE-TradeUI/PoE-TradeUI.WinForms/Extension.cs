using System;
using System.Drawing;
using PoE_TradeUI.Core;

namespace PoE_TradeUI.WinForms {
    public static class Extension {
        public static void DrawImg(this Graphics g, WinFormsImage img, double x, double y, double w, double h) {
            var p = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
            var s = new Size(Convert.ToInt32(w), Convert.ToInt32(h));
            g.DrawImage(img.Bitmap, new Rectangle(p, s));
        }
    }
}