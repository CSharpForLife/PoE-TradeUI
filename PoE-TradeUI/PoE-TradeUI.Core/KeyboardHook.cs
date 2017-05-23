using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace PoE_TradeUI.Core {
    public class KeyboardHook : IDisposable {

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        public delegate IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<KeyPressedArgs> OnKeyPressed;

        private static KeyboardProc _keyboardProc;
        private IntPtr _hookId = IntPtr.Zero;

        public KeyboardHook() {
            _keyboardProc = Callback;
        }

        public void Hook() {
            _hookId = Native.SetWindowsHookEx(WH_KEYBOARD_LL, _keyboardProc, IntPtr.Zero, 0);
        }

        private IntPtr Callback(int nCode, IntPtr wParam, IntPtr lParam) {

            if (nCode >= 0 && wParam == (IntPtr) WM_KEYDOWN) {
                var vkCode = Marshal.ReadInt32(lParam);
                OnKeyPressed?.Invoke(this, new KeyPressedArgs(KeyInterop.KeyFromVirtualKey(vkCode)));
            }

            return Native.CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void Dispose() {
            if (_hookId == IntPtr.Zero) return;
            Native.UnhookWindowsHookEx(_hookId);
        }

        public class KeyPressedArgs : EventArgs {
            public Key KeyPressed { get; }

            public KeyPressedArgs(Key key) {
                KeyPressed = key;
            }
        }
    }
}
