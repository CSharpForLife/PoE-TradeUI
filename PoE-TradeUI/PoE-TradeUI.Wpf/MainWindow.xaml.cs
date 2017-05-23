using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using CefSharp;
using CefSharp.Wpf;
using PoE_TradeUI.Core;
using PoE_TradeUI.Core.Defs;
using PoE_TradeUI.Core.Enums;
using PoE_TradeUI.Wpf.ui;
using Cursor = System.Windows.Input.Cursor;
using Rect = System.Windows.Rect;

namespace PoE_TradeUI.Wpf {

    public partial class MainWindow {

       // private readonly WpfImage _banner = Defs.GetImageDefByName("Banner").ToWpfImage();

        private ImageSource Test;

        private bool _visible = false;

        private PoeGame.WindowState? _windowState = null;
        // private readonly string _css = File.ReadAllText("g:/cef.css");

        private PoeGame _poeGame;

        private ObservableCollection<Tab> _tabs;

        public MainWindow() {
            Defs.Init();
            KeyboardHook kh = new KeyboardHook();
            // kh.KeyDown += delegate(Keys key) { Debug.WriteLine("KEYDOWN!"); };
            kh.OnKeyPressed += delegate(object sender, KeyboardHook.KeyPressedArgs e) {
                if (_windowState == null) return;
                if (_windowState.Value.Minimized || !_windowState.Value.Open) return;
                if (e.KeyPressed == Key.T) {
                    _visible = !_visible;
                    Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                        WindowState = (!_visible) ? WindowState.Minimized : WindowState.Normal;
                    }));
                }
            };
            kh.Hook();

            InitializeComponent();
            ShowInTaskbar = false;
            WindowState = WindowState.Minimized;
            Cursor = new Cursor(Defs.GetCursorDefByName("Poe Cursor").Path());

            Closing += MainWindow_Closing;

          //  Cef.BrowserSettings = new BrowserSettings() {WebSecurity = CefState.Disabled};
          //  Cef.Initialized += Cef_Initialized;
            Loaded += MainWindow_Loaded;
         //   Cef.FrameLoadEnd += Cef_FrameLoadEnd;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            Debug.WriteLine("CLOSING!");
           // _poeGame.UnHook();
        }

        private void Cef_FrameLoadEnd(object sender, FrameLoadEndEventArgs e) {
            Debug.WriteLine("FRAMELOADEND");
           // var browser = (ChromiumWebBrowser)sender;
           // browser.ExecuteScriptAsync(_css.StyleScript("poe-tradeui"));
            //browser.InjectStyleSheet("poe-tradeui", _css);
            //browser.ExecuteScriptAsync("var link = document.createElement('link'); link.rel = 'text/css'; link.href = 'file:///g:/cef.css'; document.head.append(link);");
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            //Opacity = 0;
            _poeGame = new PoeGame(new WindowInteropHelper(this).EnsureHandle());
            _poeGame.WindowSizeChanged += PoeWindowSizeChanged;
            _poeGame.WindowStateChanged += PoeWindowStateChanged;
            _tabs = new ObservableCollection<Tab>();
        }

        private void CreateNewTab(string title = null) {
            title = title ?? (_tabs.Count + 1).ToString();
            _tabs.Add(BrowserTabs.AddTab(new Tab(title)));
        }

        private int _eventCounter = 0;
        private void PoeWindowStateChanged(object sender, PoeGame.WindowState state) {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Topmost = state.TopMost;
                _windowState = state;
                WindowState = (!state.Open || state.Minimized || !_visible) ? WindowState.Minimized : WindowState.Normal;
            }));
        }

        private void Cef_Initialized(object sender, EventArgs e) {
         
        }

        private void PoeWindowSizeChanged(object sender, Native.Rect? rect) {
            if (rect != null) SetWindowBounds(rect.Value);
        }

        private void SetWindowBounds(Native.Rect rect) {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;

            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Left = rect.Left + Constants.Wpf.Ui.BorderWidth;
                Top = rect.Top + Constants.Wpf.Ui.CaptionHeight;
                Width = width - Constants.Wpf.Ui.BorderWidth * 2;
                Height = height - Constants.Wpf.Ui.CaptionHeight - Constants.Wpf.Ui.BorderWidth;
                SidePanel.Width = Height / Constants.Wpf.Ui.ScaleRatio;

                //((ImageBrush)Background.OpacityMask).Viewport = new Rect(0,0,SidePanel.Width,Height);
                //TODO use banner values instead for button position
                /*BtnClose.Width = Height * .025;
                BtnClose.Height = BtnClose.Width;
                BtnClose.Margin = new Thickness(0, Height * .05, Height * .02, 0);*/
            }));
        }

        private void ShowWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Opacity = 100;
            }));
        }

        private void HideWindow() {
            Dispatcher.Invoke(DispatcherPriority.Send, new Action(() => {
                Opacity = 0;
            }));
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Dispatcher.Invoke(() => {
                _tabs[0].Navigate("https://google.com");
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Dispatcher.Invoke(() => {
            //    Cef.ZoomLevel = Cef.ZoomLevel + .1;
            });
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            var sw = sender as ScrollViewer;
            if (sw == null) return;

            if (e.Delta == 0) return;
            var flipDelta = e.Delta * -1;
            var newOffset = sw.HorizontalOffset + flipDelta;

            if (newOffset >= sw.ExtentWidth) {
                sw.ScrollToRightEnd();
                return;
            }

            if (newOffset <= 0) {
                sw.ScrollToLeftEnd();
                return;
            }

            sw.ScrollToHorizontalOffset(newOffset);
        }
    }
}
