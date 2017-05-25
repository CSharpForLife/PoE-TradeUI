using System.Windows.Media;
using PoE_TradeUI.Core;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Wpf.ui {

    public partial class BrowserTabItem {

        private readonly WpfImage _tabLeft = Defs.GetImageDefByName("Tab Left").ToWpfImage();
        public ImageSource TabLeft => _tabLeft.BitmapImage;
        private readonly WpfImage _tabRight = Defs.GetImageDefByName("Tab Right").ToWpfImage();
        public ImageSource TabRight => _tabRight.BitmapImage;
        private readonly WpfImage _tabMiddle = Defs.GetImageDefByName("Tab Middle").ToWpfImage();
        public ImageSource TabMiddle => _tabMiddle.BitmapImage;

        public BrowserTabItem() {
            InitializeComponent();
            DataContext = this;
            Header = "";
        }

        protected override void OnRender(DrawingContext context) {
            var drawingGroup = new DrawingGroup();
            drawingGroup.AddImage(_tabLeft, 0, 0);
            drawingGroup.AddImage(_tabMiddle, _tabLeft.Width, 0);
            drawingGroup.AddImage(_tabRight, _tabLeft.Width + _tabMiddle.Width, 0);
            drawingGroup.Freeze();
            context.DrawDrawing(drawingGroup);
        }
    }
}
