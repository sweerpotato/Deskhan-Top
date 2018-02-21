using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeskhanTop.Mediator
{
    public class WindowRequestEventArgs : EventArgs
    {
        public object DataContext
        {
            get;
            private set;
        }

        public Type WindowType
        {
            get;
            private set;
        }

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
