using System.Drawing;
using System.Windows.Forms;
using PoE_TradeUI.Core;

namespace PoE_TradeUI.WinForms {
    public sealed class BackgroundPanel : Panel {
        private WinFormsImage
            _backgroundPattern,
            _banner,
            _borderLeft,
            _borderRight,
            _borderTop,
            _cornerTl,
            _cornerTr;

        public bool Initialized { get; private set; } = false;

        public BackgroundPanel() {
            DoubleBuffered = true;
            Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e) {
            if (!Initialized) {
                return;
            }
            var bmp = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(bmp)) {
                /*Paint background*/
                var bgCount = Height / _backgroundPattern.ScaledHeight;
                for (var y = 0; y < bgCount; y++) {
                    g.DrawImg(_backgroundPattern, 0, y * _backgroundPattern.Height, Width * _backgroundPattern.ScaleX,
                        _backgroundPattern.ScaledHeight);
                }

                /*Paint Banner*/
                g.DrawImg(_banner, 0, _borderTop.Height, Width * _banner.ScaleX, Height * _banner.ScaleY);

                /*Paint top border*/
                var topBorderCount = Width / _borderTop.ScaledWidth + 1;
                for (var x = 0; x < topBorderCount; x++) {
                    g.DrawImg(_borderTop, x * _borderTop.Width - x, 0, _borderTop.ScaledWidth, _borderTop.ScaledHeight);
                }

                /*Paint left and right border*/
                var leftBorderCount = Height / _borderLeft.ScaledHeight;
                for (var y = 0; y < leftBorderCount; y++) {
                    g.DrawImg(_borderLeft, 0, y * _borderLeft.Height, _borderLeft.ScaledWidth, _borderLeft.ScaledHeight);
                    g.DrawImg(_borderRight, Width - _borderRight.Width, y * _borderRight.Height,
                        _borderRight.ScaledWidth, _borderRight.ScaledHeight);
                }

                /*Paint corners*/
                g.DrawImg(_cornerTl, 0, 0, _cornerTl.ScaledWidth, _cornerTl.ScaledHeight);
                g.DrawImg(_cornerTr, Width - _cornerTr.Width, 0, _cornerTr.ScaledWidth, _cornerTr.ScaledHeight);
            }

            e.Graphics.DrawImage(bmp, new PointF(0, 0));
        }

        public void InitDefs() {
            Defs.Init();
            _backgroundPattern = Defs.GetImageDefByName("Background Pattern").ToWinFormsImage();
            _banner = Defs.GetImageDefByName("Banner").ToWinFormsImage();
            _borderLeft = Defs.GetImageDefByName("Border Left").ToWinFormsImage();
            _borderRight = Defs.GetImageDefByName("Border Right").ToWinFormsImage();
            _borderTop = Defs.GetImageDefByName("Border Top").ToWinFormsImage();
            _cornerTl = Defs.GetImageDefByName("Corner TL").ToWinFormsImage();
            _cornerTr = Defs.GetImageDefByName("Corner TR").ToWinFormsImage();
            Initialized = true;
            Visible = true;
        }
    }
}