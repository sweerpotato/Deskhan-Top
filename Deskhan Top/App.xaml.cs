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

        /// <summary>
        /// Mediator which can request new Windows to be opened, separating them from the ViewModel layer
        /// </summary>
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

        /// <summary>
        /// Event handler which listens to the Mediator's WindowRequested event, creates a new Window of the specified type with
        /// the DataContext provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowRequested(object sender, WindowRequestEventArgs e)
        {
            Window window = (Window)Activator.CreateInstance(e.WindowType);
            window.DataContext = e.DataContext;
        }

        #endregion
    }
}
