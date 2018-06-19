using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeskhanTop.Keyboard
{
    internal static class KeyInterceptor
    {
        public delegate IntPtr LowLevelKeyboardProcedure(int nCode, IntPtr wParam, IntPtr lParam);

        #region Fields

        public static readonly int KeyboardLowLevel = 13;
        public static readonly int KeyDown = 0x0100;
        public static readonly int KeyUp = 0x0101;

        private static LowLevelKeyboardProcedure _CallbackRef = null;

        #endregion

        public static IntPtr Hook(LowLevelKeyboardProcedure procedure)
        {
            _CallbackRef = procedure;

            using (Process currentProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule currentModule = currentProcess.MainModule)
                {
                    return SetWindowsHookEx(KeyboardLowLevel, procedure, GetModuleHandle(currentModule.ModuleName), 0);
                }
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcedure lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }

    public class KeyboardListener : IDisposable
    {
        #region Fields

        private static IntPtr _HookID = IntPtr.Zero;

        #endregion

        #region Constructor and Finalizer

        public KeyboardListener()
        {
            _HookID = KeyInterceptor.Hook(HookCallback);
        }

        ~KeyboardListener()
        {
            Dispose();
        }

        #endregion

        #region Private Methods

        private IntPtr InnerHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)KeyInterceptor.KeyDown)
                {
                    KeyDown(this, new RawKeyEventArgs(Marshal.ReadInt32(lParam), false));
                }
                else if (wParam == (IntPtr)KeyInterceptor.KeyUp)
                {
                    KeyUp(this, new RawKeyEventArgs(Marshal.ReadInt32(lParam), false));
                }
            }

            return KeyInterceptor.CallNextHookEx(_HookID, nCode, wParam, lParam);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                return InnerHookCallback(nCode, wParam, lParam);
            }
            catch (Exception)
            {
                //Ignore this
            }

            return KeyInterceptor.CallNextHookEx(_HookID, nCode, wParam, lParam);
        }

        #endregion

        #region Events

        public event EventHandler<RawKeyEventArgs> KeyDown = delegate { };
        public event EventHandler<RawKeyEventArgs> KeyUp = delegate { };

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            KeyInterceptor.UnhookWindowsHookEx(_HookID);
        }

        #endregion
    }
}
