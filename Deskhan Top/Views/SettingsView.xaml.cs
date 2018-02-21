using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DeskhanTop.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr windowHandle, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr windowHandle, int index, int newValue);

        public SettingsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// WinAPI call to remove the maximize button
        /// </summary>
        private void OnWindowSourceInitialized(object sender, EventArgs e)
        {
            //-16 is GWL_STYLE, which retrieves the window style
            int gwlStyle = -16;
            IntPtr handle = new WindowInteropHelper((Window)sender).Handle;
            int value = GetWindowLong(handle, gwlStyle);
            //Flip the WS_MAXIMIZEBOX bit
            SetWindowLong(handle, gwlStyle, value & ~0x10000);
        }
    }
}
