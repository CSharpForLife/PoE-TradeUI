using System;
using System.Runtime.InteropServices;

namespace PoE_TradeUI {

    public static class Native {

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

    }
}