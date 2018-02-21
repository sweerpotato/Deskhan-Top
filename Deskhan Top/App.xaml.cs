using System;
using System.Diagnostics;
using System.Windows;
using DeskhanTop.Mediator;
using System.Windows.Threading;

namespace DeskhanTop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        public static WindowMediator Mediator = new WindowMediator();

        #endregion

        #region Constructor

        public App() :
            base()
        {
            Mediator.WindowRequested += WindowRequested;
        }

        #endregion

        #region Event Handlers

        private void WindowRequested(object sender, WindowRequestEventArgs e)
        {
            Window window = (Window)Activator.CreateInstance(e.WindowType);
            window.DataContext = e.DataContext;
        }

        #endregion
    }
}
