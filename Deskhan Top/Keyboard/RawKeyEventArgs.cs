using System;
using System.Windows.Input;

namespace DeskhanTop.Keyboard
{
    public class RawKeyEventArgs : EventArgs
    {
        public int VKCode = -1;
        public Key Key = 0;
        public bool IsSystemKey = false;

        public RawKeyEventArgs(int vkCode, bool isSystemKey)
        {
            VKCode = vkCode;
            IsSystemKey = isSystemKey;
            Key = KeyInterop.KeyFromVirtualKey(VKCode);
        }
    }
}
