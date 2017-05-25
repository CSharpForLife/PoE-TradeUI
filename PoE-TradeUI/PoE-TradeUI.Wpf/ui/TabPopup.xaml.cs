using System.Windows.Media;
using PoE_TradeUI.Core;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Wpf.ui {

    public partial class TabPopup {

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

        public TabPopup() {
            InitializeComponent();
            DataContext = this;
        }
    }
}
