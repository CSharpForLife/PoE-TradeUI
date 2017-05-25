using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PoE_TradeUI.Core.Defs;

namespace PoE_TradeUI.Wpf.ui {
    /// <summary>
    /// Interaction logic for ColourGrid.xaml
    /// </summary>
    public partial class ColourGrid : UserControl {

        private List<ColourDef> _colourDefs = Defs.ColourDefs;

        public string[] GridColours { get; } = new[] {
            Defs.GetColourDefByIndex(0).ToString(),
            Defs.GetColourDefByIndex(1).ToString(),
            Defs.GetColourDefByIndex(2).ToString(),
            Defs.GetColourDefByIndex(3).ToString(),
            Defs.GetColourDefByIndex(4).ToString(),
            Defs.GetColourDefByIndex(5).ToString(),
            Defs.GetColourDefByIndex(6).ToString(),
            Defs.GetColourDefByIndex(7).ToString(),
            Defs.GetColourDefByIndex(8).ToString(),
            Defs.GetColourDefByIndex(9).ToString(),
            Defs.GetColourDefByIndex(10).ToString(),
            Defs.GetColourDefByIndex(11).ToString(),
            Defs.GetColourDefByIndex(12).ToString(),
            Defs.GetColourDefByIndex(13).ToString(),
            Defs.GetColourDefByIndex(14).ToString(),
            Defs.GetColourDefByIndex(15).ToString(),
            Defs.GetColourDefByIndex(16).ToString(),
            Defs.GetColourDefByIndex(17).ToString(),
            Defs.GetColourDefByIndex(18).ToString(),
            Defs.GetColourDefByIndex(19).ToString(),
            Defs.GetColourDefByIndex(20).ToString(),
            Defs.GetColourDefByIndex(21).ToString(),
            Defs.GetColourDefByIndex(22).ToString(),
            Defs.GetColourDefByIndex(23).ToString(),
            Defs.GetColourDefByIndex(24).ToString(),
            Defs.GetColourDefByIndex(25).ToString(),
            Defs.GetColourDefByIndex(26).ToString()
        };


        //  public string[] GricColours => new string[]{ Defs.GetColourDefByIndex(0).ToString() };
       // public string GridC => "#000";

        public ColourGrid() {
            InitializeComponent();
            DataContext = this;
        }

        private void UIElement_OnMouseEnter(object sender, MouseEventArgs e) {
            return;
        }
    }
}
