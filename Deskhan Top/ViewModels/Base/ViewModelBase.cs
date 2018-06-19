using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DeskhanTop.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        private readonly Dispatcher _Dispatcher;

        #endregion

        #region Constructor

        protected ViewModelBase()
        {
            if (Thread.CurrentThread != Application.Current.Dispatcher.Thread)
            {
                throw new InvalidOperationException("View models must be created on the GUI thread");
            }

            _Dispatcher = Application.Current.Dispatcher;
        }

        #endregion

        #region Methods

        protected void InvokeOnGUIThread(Action action)
        {
            if (Thread.CurrentThread == _Dispatcher.Thread)
            {
                //We're executing on the GUI thread
                action();
            }
            else
            {
                //Invoke on the GUI thread
                _Dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// Invokes the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The name of the property or name to invoke the PropertyChanged event for</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null)
            {
                InvokeOnGUIThread(() =>
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }

        /// <summary>
        /// Protected method which sets a field to a value and optionally invokes the PropertyChanged event with the name of the property.<para/>
        /// Returns true if the field was changed and the PropertyChanged event was invoked successfully. False if not.
        /// </summary>
        /// <typeparam name="T">The type of the field</typeparam>
        /// <param name="field">A reference to the field</param>
        /// <param name="value">The value to assign to the field</param>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>True if the field was changed and the PropertyChanged event was invoked successfully. False if not.</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
}
