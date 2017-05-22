using System.Windows.Controls;

namespace PoE_TradeUI.Wpf.ui {
    internal class Tab {
        public TabItem TabItem { get; set; }
        public string Url { get; set; }
        public TabView TabView { get; set; }

        public Tab(string header, string url = "http://poe.trade") {
            Url = url;
            TabView = new TabView(url);
            TabItem = new TabItem() {
                Header = header,
                Content = TabView
            };
        }

        public void Navigate(string url = "http://poe.trade") {
            Url = url;
            TabView.Browser.Address = url;
        }

    }
}