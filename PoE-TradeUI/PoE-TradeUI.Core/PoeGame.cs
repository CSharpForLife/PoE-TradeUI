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
        private readonly Thread _processPollThread;
        private WindowState _windowState;
        private Native.Rect _windowSize;
        public GlobalHook Hook { get; }

        public struct WindowState {
            public bool Open;
            public bool TopMost;
            public bool Minimized;
        }

        public event EventHandler<Native.Rect?> WindowSizeChanged;
        public event EventHandler<WindowState> WindowStateChanged;

        public PoeGame(IntPtr windowHandle) {
            
            _windowHandle = windowHandle;

            _processPollThread = new Thread(ProcessPoll) {IsBackground = true};
            _processPollThread.Start();

            Hook = new GlobalHook(HookEvent.EVENT_SYSTEM_FOREGROUND);

            Hook.InitHook();
            Hook.OnHookEvent += Hook_OnHookEvent;
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MINIMIZESTART);
            Hook.Subscribe(HookEvent.EVENT_OBJECT_LOCATIONCHANGE);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_FOREGROUND);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MOVESIZESTART);
            Hook.Subscribe(HookEvent.EVENT_SYSTEM_MOVESIZEEND);
        }

        private void ProcessPoll() {
            while (_processPollThread.IsAlive) {

                if (!_windowState.Open) {
                    _poeProcess = FindGame();
                    if(_poeProcess != null) _poeHandle = _poeProcess.MainWindowHandle;
                }

                if (_poeProcess == null) {
                    Thread.Sleep(100);
                    continue;
                }

                if (_poeProcess.HasExited) {
                    _poeProcess = null;
                    SetWindowState(false, false, false);
                    continue;
                }

                if(_poeHandle == IntPtr.Zero) continue;

                if (_windowState.Open) {
                    Thread.Sleep(1000);
                    continue;
                }

                SetWindowState(_windowState.Minimized, true, _windowState.TopMost);
                SetWindowSize();
                Thread.Sleep(1);
            }
        }

        private void SetWindowState(bool minimized, bool open, bool topmost) {
            _windowState = new WindowState() { Minimized = minimized, Open = open, TopMost = topmost };
            WindowStateChanged?.Invoke(this, _windowState);
        }

        private void SetWindowSize() {
            if (!Native.GetWindowRect(_poeHandle, out Native.Rect r) || r.Equals(_windowSize)) return;
            if (r.Left < 0 && r.Top < 0 && r.Right < 0 && r.Bottom < 0) return;
            _windowSize = r;
            WindowSizeChanged?.Invoke(this, _windowSize);
        }

        private void Hook_OnHookEvent(object sender, GlobalHook.HookEventArgs e) {

            if (!_windowState.Open || _poeProcess == null) return;

            switch (e.EventType) {
                case HookEvent.EVENT_SYSTEM_FOREGROUND:
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
                    if (e.HWnd != _poeHandle) break;
                    if (_windowState.Minimized) break;
                    SetWindowState(true, true, false);
                    break;
                case HookEvent.EVENT_SYSTEM_MOVESIZEEND:
                    if (e.HWnd != _poeHandle) break;
                    SetWindowSize();
                    break;
                case HookEvent.EVENT_OBJECT_LOCATIONCHANGE:
                    if (e.HWnd != _poeHandle || _windowState.Minimized) break;
                    SetWindowSize();
                    break;
            }
        }

        private static Process FindGame() => (from process in Process.GetProcesses()
            let lower = process.ProcessName.ToLower()
            where lower.Contains("path") && lower.Contains("of") && lower.Contains("exile")
            let title = process.MainWindowTitle
            where !string.IsNullOrEmpty(title)
            let titleLower = title.ToLower()
            where titleLower.Contains("path") && titleLower.Contains("of") && titleLower.Contains("exile")
            select process).FirstOrDefault();

        private void AddExitHandler() {
            if (_poeProcess == null) return;
            _poeProcess.EnableRaisingEvents = true;
            _poeProcess.Exited += (sender, args) => {
                _poeProcess = null;
                _poeHandle = IntPtr.Zero;
                WindowStateChanged?.Invoke(this, new WindowState() {Open = false, TopMost = false});
            };
        }
    }
}