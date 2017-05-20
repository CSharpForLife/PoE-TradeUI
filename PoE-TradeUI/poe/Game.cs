using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PoE_TradeUI.poe {

    public class Game {

        private Process _poeProcess;
        private IntPtr _poeHandle = IntPtr.Zero;
        private Thread _thread;

        public event EventHandler<Native.Rect> WindowStateChanged; 

        public Game() {
            _thread = new Thread(GameThread) {IsBackground = true};
            _thread.Start();
        }

        private static Process FindGame() {
            return (from process in Process.GetProcesses() let lower = process.ProcessName.ToLower() where lower.Contains("path") && lower.Contains("exile") select process).FirstOrDefault();
        }

        private void GameThread() {
            Native.Rect oldRect = new Native.Rect();
            while (_thread.IsAlive) {
                if (_poeProcess == null) {
                    _poeProcess = FindGame();
                    if (_poeProcess != null) {
                        _poeProcess.EnableRaisingEvents = true;
                        _poeProcess.Exited += (sender, args) => {
                            _poeProcess = null;
                            _poeHandle = IntPtr.Zero;
                        };
                    }
                }
                if (_poeProcess == null) {
                    Thread.Sleep(100);
                    continue;
                }
                if (_poeHandle == IntPtr.Zero) _poeHandle = _poeProcess.MainWindowHandle;
                if(_poeHandle == IntPtr.Zero) continue;

                if(Native.GetWindowRect(_poeHandle, out Native.Rect rect) && !rect.Equals(oldRect)) {
                    WindowStateChanged?.Invoke(this, rect);
                    oldRect = rect;
                }
                Thread.Sleep(1);
            }
        }
    }
}
