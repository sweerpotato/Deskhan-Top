using System;

namespace DeskhanTop.Mediator
{
    public class WindowMediator
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of this class, should only be used by the App class
        /// </summary>
        public WindowMediator()
        {
            if (App.Mediator != null)
            {
                throw new InvalidOperationException("Please use the static Mediator in the App class");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Requests a Window to be opened
        /// </summary>
        /// <param name="dataContext">The DataContext for the Window</param>
        /// <param name="windowType">The Type of Window to be opened</param>
        public void RequestWindow(object dataContext, Type windowType)
        {
            WindowRequested(this, new WindowRequestEventArgs(dataContext, windowType));
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raised when a Window is requested from the RequestWindow method
        /// </summary>
        public event EventHandler<WindowRequestEventArgs> WindowRequested = delegate { };

        #endregion
    }
}
