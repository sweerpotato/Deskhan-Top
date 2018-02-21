using System;

namespace DeskhanTop.Mediator
{
    public class WindowMediator
    {
        #region Constructor

        public WindowMediator()
        {
            if (App.Mediator != null)
            {
                throw new InvalidOperationException("Please use the static Mediator in the App class");
            }
        }

        #endregion

        #region Methods

        public void RequestWindow(object dataContext, Type windowType)
        {
            WindowRequested(this, new WindowRequestEventArgs(dataContext, windowType));
        }

        #endregion

        #region Events

        public event EventHandler<WindowRequestEventArgs> WindowRequested = delegate { };

        #endregion
    }
}
