using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using PoE_TradeUI.Core.Enums;

namespace PoE_TradeUI.Core {
    public class PoeGame {
        private Process _poeProcess;
        private IntPtr _poeHandle = IntPtr.Zero;
        private readonly IntPtr _windowHandle;
        private WindowState _windowState;
        private Native.Rect _windowSize;
        private bool _visible;
        public GlobalHook Hook { get; }
        public KeyboardHook KeyboardHook { get; }

        public struct WindowState {
            public bool Open;
            public bool TopMost;
            public bool Minimized;
            public bool Visible;
        }

        public event EventHandler<Native.Rect?> WindowSizeChanged;
        public event EventHandler<WindowState> WindowStateChanged;

        public PoeGame(IntPtr windowHandle) {
            
            _windowHandle = windowHandle;

            _poeProcess = FindGame();
            if (_poeProcess != null) {
                _poeHandle = _poeProcess.MainWindowHandle;
                SetWindowState(false, true, true);
            }
            //TODO INITIAL WINDOW STATE


            KeyboardHook = new KeyboardHook();
            KeyboardHook.OnKeyPressed += (sender, e) => {
                if (e.KeyPressed != Constants.HotKey) return;
                if (_windowState.Minimized || !_windowState.Open || !_windowState.TopMost) return;
                _visible = !_visible;
                SetWindowState(_windowState.Minimized, _windowState.Open, _windowState.TopMost);
            };
            KeyboardHook.Hook();

            Hook = new GlobalHook(HookEvent.EVENT_SYSTEM_FOREGROUND);

            Hook.InitHook();
            Hook.OnHookEvent += Hook_OnHookEvent;
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MINIMIZESTART);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_LOCATIONCHANGE);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_FOREGROUND);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MOVESIZESTART);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MOVESIZEEND);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_DESTROY);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_CREATE);
        }

        private void SetWindowState(bool minimized, bool open, bool topmost) {
            _windowState = new WindowState() { Minimized = minimized, Open = open, TopMost = topmost, Visible = _visible};
            WindowStateChanged?.Invoke(this, _windowState);
        }

        private void SetWindowSize() {
            if (!Native.GetWindowRect(_poeHandle, out Native.Rect r) || r.Equals(_windowSize)) return;
            if (r.Left < 0 && r.Top < 0 && r.Right < 0 && r.Bottom < 0) return;
            _windowSize = r;
            WindowSizeChanged?.Invoke(this, _windowSize);
        }

        private void Hook_OnHookEvent(object sender, GlobalHook.HookEventArgs e) {

            //if (!_windowState.Open || _poeProcess == null) return;

            switch (e.EventType) {
                case HookEvent.EVENT_SYSTEM_FOREGROUND:
                    if (!_windowState.Open) break;
                    //If we're minimized and foreground is not poe window then ignore
                    if (e.HWnd != _poeHandle && _windowState.Minimized) break;

                    //Something else is topmost
                    if (e.HWnd != _poeHandle && e.HWnd != _windowHandle && _windowState.TopMost) {
                        SetWindowState(false, true, false);
                        break;
                    }

                    //We're topmost
                    SetWindowState(false, true, true);
                    break;
                case HookEvent.EVENT_SYSTEM_MINIMIZESTART:
                    if (!_windowState.Open) break;
                    if (e.HWnd != _poeHandle) break;
                    if (_windowState.Minimized) break;
                    SetWindowState(true, true, false);
                    break;
                case HookEvent.EVENT_SYSTEM_MOVESIZEEND:
                    if (!_windowState.Open) break;
                    if (e.HWnd != _poeHandle) break;
                    SetWindowSize();
                    break;
                case HookEvent.EVENT_OBJECT_LOCATIONCHANGE:
                    if (!_windowState.Open) break;
                    if (e.HWnd != _poeHandle || _windowState.Minimized) break;
                    SetWindowSize();
                    break;
                case HookEvent.EVENT_OBJECT_DESTROY:
                    if (!_windowState.Open) break;
                    if(e.HWnd == _poeHandle) SetWindowState(false, false, false);
                    break;
                case HookEvent.EVENT_OBJECT_CREATE:
                    if (_windowState.Open) break;
                    uint pid;
                    Native.GetWindowThreadProcessId(e.HWnd, out pid);
                    var p = Process.GetProcessById((int)pid);

                    if (!TitleCheck(p.ProcessName.ToLower())) break;
                    if (!TitleCheck(p.MainWindowTitle.ToLower())) break;

                    _poeProcess = p;
                    _poeHandle = _poeProcess.MainWindowHandle;
                    SetWindowState(false, true, true);

                    break;
            }
        }

        private static bool TitleCheck(string title) => title.Contains("path") && title.Contains("of") && title.Contains("exile");

        private static Process FindGame() => (from process in Process.GetProcesses()
            let lower = process.ProcessName.ToLower()
            where lower.Contains("path") && lower.Contains("of") && lower.Contains("exile")
            let title = process.MainWindowTitle
            where !string.IsNullOrEmpty(title)
            let titleLower = title.ToLower()
            where titleLower.Contains("path") && titleLower.Contains("of") && titleLower.Contains("exile")
            select process).FirstOrDefault();
    }
}