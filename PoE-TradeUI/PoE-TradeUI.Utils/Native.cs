using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PoE_TradeUI.Utils {
    public static class Native {
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}
