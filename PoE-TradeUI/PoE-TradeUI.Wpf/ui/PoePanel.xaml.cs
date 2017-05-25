using PoE_TradeUI.Core;
using PoE_TradeUI.Core.Defs;
using System.Windows.Media;

namespace PoE_TradeUI.Wpf.ui {

    public partial class PoePanel {

        private readonly WpfImage _bordertl = Defs.GetImageDefByName("Border Top Left").ToWpfImage();
        public ImageSource BorderTopLeft => _bordertl.BitmapImage;

        private readonly WpfImage _borderTop = Defs.GetImageDefByName("Border Top").ToWpfImage();
        public ImageSource BorderTop => _borderTop.BitmapImage;

        private readonly WpfImage _bordertr = Defs.GetImageDefByName("Border Top Right").ToWpfImage();
        public ImageSource BorderTopRight => _bordertr.BitmapImage;

        private readonly WpfImage _borderRight = Defs.GetImageDefByName("Border Right").ToWpfImage();
        public ImageSource BorderRight => _borderRight.BitmapImage;

        private readonly WpfImage _borderbr = Defs.GetImageDefByName("Border Bottom Right").ToWpfImage();
        public ImageSource BorderBottomRight => _borderbr.BitmapImage;

        private readonly WpfImage _borderBottom = Defs.GetImageDefByName("Border Bottom").ToWpfImage();
        public ImageSource BorderBottom => _borderBottom.BitmapImage;

        private readonly WpfImage _borderbl = Defs.GetImageDefByName("Border Bottom Left").ToWpfImage();
        public ImageSource BorderBottomLeft => _borderbl.BitmapImage;

        private readonly WpfImage _borderLeft = Defs.GetImageDefByName("Border Left").ToWpfImage();
        public ImageSource BorderLeft => _borderLeft.BitmapImage;

        public PoePanel() {
            InitializeComponent();
            DataContext = this;
        }
    }
}
