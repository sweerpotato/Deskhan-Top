using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DeskhanTop.Keyboard
{
    public static class HotkeyManager
    {
        #region Fields

        /// <summary>
        /// Currently pressed keys
        /// </summary>
        private static HashSet<Key> _PressedKeys = new HashSet<Key>();

        /// <summary>
        /// Registered hotkeys
        /// </summary>
        private static HashSet<Key> _Hotkeys = new HashSet<Key>();

        #endregion

        #region Methods

        /// <summary>
        /// Registers the supplied keys as the hotkeys
        /// </summary>
        /// <param name="keys">The keys to register</param>
        public static void RegisterHotkey(Key[] keys)
        {
            lock (_Hotkeys)
            {
                _Hotkeys.Clear();

                foreach (Key key in keys)
                {
                    _Hotkeys.Add(key);
                }
            }
        }

        /// <summary>
        /// Marks a key as pressed and fires the hotkey event if the required keys are pressed
        /// </summary>
        /// <param name="key">The key to mark</param>
        public static void Press(Key key)
        {
            lock (_PressedKeys)
            {
                if (!_PressedKeys.Contains(key))
                {
                    _PressedKeys.Add(key);

                    if (_PressedKeys.SetEquals(_Hotkeys))
                    {
                        HotkeyPressed(null, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Unmarks a key as pressed
        /// </summary>
        /// <param name="key">The key to unmark</param>
        public static void Release(Key key)
        {
            lock (_PressedKeys)
            {
                if (_PressedKeys.Contains(key))
                {
                    _PressedKeys.Remove(key);
                }
            }
        }

        /// <summary>
        /// Event fired when the registered hotkey is pressed
        /// </summary>
        public static event EventHandler<EventArgs> HotkeyPressed = delegate { };

        #endregion
    }
}
