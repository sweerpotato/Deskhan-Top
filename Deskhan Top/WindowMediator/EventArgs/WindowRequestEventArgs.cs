using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskhanTop.Mediator
{
    public class WindowRequestEventArgs : EventArgs
    {
        /// <summary>
        /// The WindowType's DataContext
        /// </summary>
        public object DataContext
        {
            get;
            private set;
        }

        /// <summary>
        /// The type of window requested
        /// </summary>
        public Type WindowType
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of this class with a DataContext and a Window Type to which the DataContext is bound
        /// </summary>
        /// <param name="dataContext">The datacontext to bind to the Window</param>
        /// <param name="windowType">The Type of Window requested</param>
        public WindowRequestEventArgs(object dataContext, Type windowType) :
            base()
        {
            if (dataContext == null || windowType == null)
            {
                throw new ArgumentException("Invalid argument(s) to WindowRequestEventArgs");
            }

            DataContext = dataContext;
            WindowType = windowType;
        }
    }
}
