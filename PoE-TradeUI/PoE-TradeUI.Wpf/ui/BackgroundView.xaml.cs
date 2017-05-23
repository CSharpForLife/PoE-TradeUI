using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PoE_TradeUI.Core;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Wpf.ui {
    /// <summary>
    /// Interaction logic for BackgroundView.xaml
    /// </summary>
    public partial class BackgroundView : UserControl {

        /*Layer 1 images*/
        private readonly WpfImage _borderTop = Defs.GetImageDefByName("Border Top").ToWpfImage();
        public ImageSource BorderTop => _borderTop.BitmapImage;
        private readonly WpfImage _borderLeft = Defs.GetImageDefByName("Border Left").ToWpfImage();
        public ImageSource BorderLeft => _borderLeft.BitmapImage;
        private readonly WpfImage _borderRight = Defs.GetImageDefByName("Border Right").ToWpfImage();
        public ImageSource BorderRight => _borderRight.BitmapImage;
        private readonly WpfImage _cornerTl = Defs.GetImageDefByName("Corner TL").ToWpfImage();
        public ImageSource CornerTopLeft => _cornerTl.BitmapImage;
        private readonly WpfImage _cornerTr = Defs.GetImageDefByName("Corner TR").ToWpfImage();
        public ImageSource CornerTopRight => _cornerTr.BitmapImage;
        private readonly WpfImage _backgroundPattern = Defs.GetImageDefByName("Background Pattern").ToWpfImage();
        public ImageSource BackgroundPattern => _backgroundPattern.BitmapImage;

        /*Layer 2 images*/
        private readonly WpfImage _banner = Defs.GetImageDefByName("Banner").ToWpfImage();
        public ImageSource Banner => _banner.BitmapImage;

        public BackgroundView() {
            InitializeComponent();
            DataContext = this;
            LayoutUpdated += BackgroundView_LayoutUpdated;
        }

        /*Do any scaling here*/
        private void BackgroundView_LayoutUpdated(object sender, EventArgs e) {
            BannerBrush.Viewport = new Rect(0,0, ActualWidth, ActualHeight * _banner.ScaleY);
            OpacityMask.Viewport = new Rect(0,0, ActualWidth, ActualHeight);
        }
    }
}
