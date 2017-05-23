using System;
using System.Diagnostics;
using System.Linq;
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

                Native.GetWindowRect(_poeHandle, out Native.Rect r);
                if (r.Left < 0 && r.Top < 0 && r.Right < 0 && r.Bottom < 0)
                    _visible = false;
                else _visible = true;
            }

            KeyboardHook = new KeyboardHook();
            KeyboardHook.OnKeyPressed += (sender, e) => {
                if (!_windowState.TopMost || _poeHandle == IntPtr.Zero || e.KeyPressed != Config.UserConfig.Hotkey) return;
                _visible = !_visible;
                SetWindowState(_windowState.Minimized, _windowState.TopMost);
                SetWindowSize();
            };
            KeyboardHook.Hook();

            Hook = new GlobalHook();
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MINIMIZESTART);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MINIMIZEEND);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_LOCATIONCHANGE);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_FOREGROUND);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_DESTROY);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_CREATE);
            Hook.OnHookEvent += Hook_OnHookEvent;
            Hook.InitHook();
        }

        private void SetWindowState(bool minimized, bool topmost) {
            _windowState = new WindowState {Minimized = minimized, TopMost = topmost, Visible = _visible};
            WindowStateChanged?.Invoke(this, _windowState);
        }

        private void SetWindowSize() {
            if (!Native.GetWindowRect(_poeHandle, out Native.Rect r) || r.Equals(_windowSize)) return;
            if (r.Left < 0 && r.Top < 0 && r.Right < 0 && r.Bottom < 0) return;
            _windowSize = r;
            WindowSizeChanged?.Invoke(this, _windowSize);
        }

        private void Hook_OnHookEvent(object sender, GlobalHook.HookEventArgs e) {
            switch (e.EventType) {
                case HookEvent.EVENT_SYSTEM_FOREGROUND:
                    if (e.HWnd == _poeHandle) {
                        //poe is topmost
                        SetWindowState(false, true);
                        break;
                    }
                    if (e.HWnd == _windowHandle && !_windowState.Minimized) {
                        SetWindowState(false, true);
                        break;
                    } 

                    //something else is topmost
                    SetWindowState(_windowState.Minimized, false);
                    break;
                case HookEvent.EVENT_SYSTEM_MINIMIZESTART:
                    if (e.HWnd != _poeHandle) break;
                    //poe is minimized
                    SetWindowState(true, false);
                    break;
                case HookEvent.EVENT_SYSTEM_MINIMIZEEND:
                    if (e.HWnd != _poeHandle) break;
                    //poe is maximized
                    SetWindowState(false, true);
                    break;
                case HookEvent.EVENT_OBJECT_LOCATIONCHANGE:
                    if (e.HWnd != _poeHandle) break;
                    SetWindowSize();
                    break;
                case HookEvent.EVENT_OBJECT_DESTROY:
                    if (e.HWnd != _poeHandle) break;
                    SetWindowState(true, false);
                    break;
                case HookEvent.EVENT_OBJECT_CREATE:
#if DEBUG
                    var testsProcess = Process.GetProcessesByName("PoE-TradeUI.Tests");
                    if (testsProcess.Length > 0) {
                        _poeProcess = testsProcess[0];
                        _poeHandle = _poeProcess.MainWindowHandle;
                        break;
                    }

#endif
                    Native.GetWindowThreadProcessId(e.HWnd, out uint pid);
                    var p = Process.GetProcessById((int) pid);
                    if (!TitleCheck(p.ProcessName.ToLower())) break;
                    if (!TitleCheck(p.MainWindowTitle.ToLower())) break;

                    _poeProcess = p;
                    _poeHandle = _poeProcess.MainWindowHandle;
                    SetWindowState(false, true);
                    break;
            }
        }

        private static bool TitleCheck(string title) => title.Contains("path") && title.Contains("of") && title.Contains("exile");

        private static Process FindGame() {
#if DEBUG
            var testsProcess = Process.GetProcessesByName("PoE-TradeUI.Tests");
            if (testsProcess.Length > 0) {
                return testsProcess[0];
            }
#endif
            return (from process in Process.GetProcesses()
                let lower = process.ProcessName.ToLower()
                where lower.Contains("path") && lower.Contains("of") && lower.Contains("exile")
                let title = process.MainWindowTitle
                where !string.IsNullOrEmpty(title)
                let titleLower = title.ToLower()
                where titleLower.Contains("path") && titleLower.Contains("of") && titleLower.Contains("exile")
                select process).FirstOrDefault();
        }
    }
}