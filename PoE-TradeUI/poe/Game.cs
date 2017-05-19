using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace PoE_TradeUI.poe {

    public class Game {

        private Process _poeProcess;
        private IntPtr _poeHandle = IntPtr.Zero;
        private Thread _thread;

        public event EventHandler<Native.Rect> WindowStateChanged; 

        public Game() {
            _poeProcess = FindGame();
            if(_poeProcess != null) _poeHandle = _poeProcess.MainWindowHandle;
            _thread = new Thread(GameThread) {IsBackground = true};
            _thread.Start();
        }

        private static Process FindGame() {
            return (from process in Process.GetProcesses() let lower = process.ProcessName.ToLower() where lower.Contains("path") && lower.Contains("exile") select process).FirstOrDefault();
        }

        private void GameThread() {
            while (true) {
                if (_poeProcess == null) _poeProcess = FindGame();
                if (_poeProcess == null) {
                    Thread.Sleep(100);
                    continue;
                }
                if (_poeHandle == IntPtr.Zero) _poeHandle = _poeProcess.MainWindowHandle;
                if(_poeHandle == IntPtr.Zero) continue;

                Native.Rect rect;
                if(Native.GetWindowRect(_poeHandle, out rect)) {
                    WindowStateChanged?.Invoke(this, rect);
                }
                Thread.Sleep(1);
            }
        }
    }
}
