using System;
using System.Diagnostics;
using System.Windows.Forms;
using PoE_TradeUI.Core;

namespace PoE_TradeUI.WinForms {
    public partial class FormMain : Form {
        private PoeGame PoeWin { get; }

        public FormMain() {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
            PoeWin = new PoeGame(Handle);
            PoeWin.WindowSizeChanged += PoEWinOnWindowSizeChanged;
            PoeWin.WindowStateChanged += PoEWinOnWindowStateChanged;
        }

        private void PoEWinOnWindowSizeChanged(object sender, Native.Rect? rect) {
            if (rect != null) SetWindowBounds(rect.Value);
        }

        private void PoEWinOnWindowStateChanged(object sender, PoeGame.WindowState state) {
            Invoke(new Action(() => {
                TopMost = state.TopMost;
                WindowState = state.Minimized
                    ? FormWindowState.Minimized
                    : FormWindowState.Normal;
            }));
        }

        private void SetWindowBounds(Native.Rect rect) {
            var width = rect.Right - rect.Left;
            var height = rect.Bottom - rect.Top;

            Invoke(new Action(() => {
                Left = rect.Left + Constants.WinForms.Ui.WindowBorderLeft;
                Top = rect.Top + Constants.WinForms.Ui.WindowBorderTop;
                Width = width - Constants.WinForms.Ui.WindowBorderWidth * 2;
                Height = height - Constants.WinForms.Ui.WindowBorderHeight;
                SidePanel.Width = Convert.ToInt32(Height / Constants.WinForms.Ui.ScaleRatio);
            }));
        }

        private void FormMain_Load(object sender, EventArgs e) {
            if (!SidePanel.Initialized) SidePanel.InitDefs();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            PoeWin.Hook.RemoveHook();
        }
    }
}