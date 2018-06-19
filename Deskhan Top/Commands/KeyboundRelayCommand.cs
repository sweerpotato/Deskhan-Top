using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DeskhanTop.Commands
{
    public class KeyboundRelayCommand<T> : RelayCommand<T> where T : INotifyPropertyChanged
    {
        #region Properties and Fields

        public KeyGesture KeyGesture
        {
            get;
            private set;
        }

        #endregion

        #region Constructors

        public KeyboundRelayCommand(Action action, KeyGesture keyGesture, Func<bool> canExecute) :
            base(action, canExecute)
        {
            KeyGesture = keyGesture;
        }

        public KeyboundRelayCommand(Action<object> paramAction, KeyGesture keyGesture, Func<bool> canExecute)
            : base(paramAction, canExecute)
        {
            KeyGesture = keyGesture;
        }

        public KeyboundRelayCommand(T viewModel, Action action, KeyGesture keyGesture, Func<bool> canExecute,
            Expression<Func<T, object>> propertySelector)
            : base(viewModel, action, canExecute, propertySelector)
        {
            KeyGesture = keyGesture;
        }

        #endregion
    }
}
