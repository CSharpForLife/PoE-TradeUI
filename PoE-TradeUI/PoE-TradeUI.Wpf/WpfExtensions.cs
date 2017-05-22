using System.Windows.Controls;
using PoE_TradeUI.Wpf.ui;

namespace PoE_TradeUI.Wpf {
    public static class WpfExtensions {

        public static Tab AddTab(this TabControl tabControl, Tab tab, bool setIndex = true) {
            tabControl.Items.Add(tab.TabItem);
            if (setIndex) tabControl.SelectedIndex = tabControl.Items.Count - 1;
            return tab;
        }
    }
}
