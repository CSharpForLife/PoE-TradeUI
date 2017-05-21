using System;
using System.Windows.Forms;
using PoE_TradeUI.Utils;

namespace PoE_TradeUI.WinForms
{
    public partial class FormMain : Form
    {
        private PoeGame PoeWin { get; }

        public FormMain()
        {
            InitializeComponent();
            Visible = false;
            PoeWin = new PoeGame();
            PoeWin.WindowStateChanged += PoEWinOnWindowStateChanged;
        }

        private void PoEWinOnWindowStateChanged(object sender, PoeGame.WindowState windowState)
        {
            var rect = windowState.Rect;

            if (!windowState.Open)
            {
                HideWindow();
                return;
            }

            if (Visible) ShowWindow();

            if (rect != null) SetWindowBounds(rect.Value);
        }

        private void SetWindowBounds(Native.Rect rect)
        {
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

        private void ShowWindow()
        {
            Invoke(new Action(() => { Visible = true; }));
        }

        private void HideWindow()
        {
            Invoke(new Action(() => { Visible = false; }));
        }
    }
}
