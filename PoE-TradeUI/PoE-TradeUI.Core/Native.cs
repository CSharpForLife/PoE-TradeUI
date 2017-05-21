using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PoE_TradeUI.Core {
    public static class Native {

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType,
            IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr
                hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,
            uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        public static string GetActiveWindowTitle() {
            var buffer = new StringBuilder(256);
            return GetWindowText(GetForegroundWindow(), buffer, 256) > 0 ? buffer.ToString() : null;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public const uint WINEVENT_OUTOFCONTEXT = 0x0000;
        public const uint WINEVENT_SKIPOWNTHREAD = 0x0001;
        public const uint WINEVENT_SKIPOWNPROCESS = 0x0002;
        public const uint WINEVENT_INCONTEXT = 0x0004;
        public const uint EVENT_MIN = 0x00000001;
        public const uint EVENT_MAX = 0x7FFFFFFF;
        public const uint EVENT_SYSTEM_SOUND = 0x0001;
        public const uint EVENT_SYSTEM_ALERT = 0x0002;
        public const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
        public const uint EVENT_SYSTEM_MENUSTART = 0x0004;
        public const uint EVENT_SYSTEM_MENUEND = 0x0005;
        public const uint EVENT_SYSTEM_MENUPOPUPSTART = 0x0006;
        public const uint EVENT_SYSTEM_MENUPOPUPEND = 0x0007;
        public const uint EVENT_SYSTEM_CAPTURESTART = 0x0008;
        public const uint EVENT_SYSTEM_CAPTUREEND = 0x0009;
        public const uint EVENT_SYSTEM_MOVESIZESTART = 0x000A;
        public const uint EVENT_SYSTEM_MOVESIZEEND = 0x000B;
        public const uint EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C;
        public const uint EVENT_SYSTEM_CONTEXTHELPEND = 0x000D;
        public const uint EVENT_SYSTEM_DRAGDROPSTART = 0x000E;
        public const uint EVENT_SYSTEM_DRAGDROPEND = 0x000F;
        public const uint EVENT_SYSTEM_DIALOGSTART = 0x0010;
        public const uint EVENT_SYSTEM_DIALOGEND = 0x0011;
        public const uint EVENT_SYSTEM_SCROLLINGSTART = 0x0012;
        public const uint EVENT_SYSTEM_SCROLLINGEND = 0x0013;
        public const uint EVENT_SYSTEM_SWITCHSTART = 0x0014;
        public const uint EVENT_SYSTEM_SWITCHEND = 0x0015;
        public const uint EVENT_SYSTEM_MINIMIZESTART = 0x0016;
        public const uint EVENT_SYSTEM_MINIMIZEEND = 0x0017;
        public const uint EVENT_SYSTEM_DESKTOPSWITCH = 0x0020;
        public const uint EVENT_SYSTEM_END = 0x00FF;
        public const uint EVENT_OEM_DEFINED_START = 0x0101;
        public const uint EVENT_OEM_DEFINED_END = 0x01FF;
        public const uint EVENT_UIA_EVENTID_START = 0x4E00;
        public const uint EVENT_UIA_EVENTID_END = 0x4EFF;
        public const uint EVENT_UIA_PROPID_START = 0x7500;
        public const uint EVENT_UIA_PROPID_END = 0x75FF;
        public const uint EVENT_CONSOLE_CARET = 0x4001;
        public const uint EVENT_CONSOLE_UPDATE_REGION = 0x4002;
        public const uint EVENT_CONSOLE_UPDATE_SIMPLE = 0x4003;
        public const uint EVENT_CONSOLE_UPDATE_SCROLL = 0x4004;
        public const uint EVENT_CONSOLE_LAYOUT = 0x4005;
        public const uint EVENT_CONSOLE_START_APPLICATION = 0x4006;
        public const uint EVENT_CONSOLE_END_APPLICATION = 0x4007;
        public const uint EVENT_CONSOLE_END = 0x40FF;
        public const uint EVENT_OBJECT_CREATE = 0x8000; 
        public const uint EVENT_OBJECT_DESTROY = 0x8001;
        public const uint EVENT_OBJECT_SHOW = 0x8002;
        public const uint EVENT_OBJECT_HIDE = 0x8003;
        public const uint EVENT_OBJECT_REORDER = 0x8004;
        public const uint EVENT_OBJECT_FOCUS = 0x8005;
        public const uint EVENT_OBJECT_SELECTION = 0x8006;
        public const uint EVENT_OBJECT_SELECTIONADD = 0x8007;
        public const uint EVENT_OBJECT_SELECTIONREMOVE = 0x8008;
        public const uint EVENT_OBJECT_SELECTIONWITHIN = 0x8009;
        public const uint EVENT_OBJECT_STATECHANGE = 0x800A;
        public const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;
        public const uint EVENT_OBJECT_NAMECHANGE = 0x800C;
        public const uint EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D;
        public const uint EVENT_OBJECT_VALUECHANGE = 0x800E;
        public const uint EVENT_OBJECT_PARENTCHANGE = 0x800F;
        public const uint EVENT_OBJECT_HELPCHANGE = 0x8010;
        public const uint EVENT_OBJECT_DEFACTIONCHANGE = 0x8011;
        public const uint EVENT_OBJECT_ACCELERATORCHANGE = 0x8012;
        public const uint EVENT_OBJECT_INVOKED = 0x8013;
        public const uint EVENT_OBJECT_TEXTSELECTIONCHANGED = 0x8014;
        public const uint EVENT_OBJECT_CONTENTSCROLLED = 0x8015;
        public const uint EVENT_SYSTEM_ARRANGMENTPREVIEW = 0x8016;
        public const uint EVENT_OBJECT_END = 0x80FF;
        public const uint EVENT_AIA_START = 0xA000;
        public const uint EVENT_AIA_END = 0xAFFF;
    }
}
