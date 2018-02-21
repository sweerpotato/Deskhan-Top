using System;
using System.Diagnostics;
using System.Windows.Threading;
using MVMBase;
using MVMBase.Commands;

namespace DeskhanTop.ViewModels
{
    public class TaskbarIconViewModel : ViewModelBase
    {
        #region Commands

        /// <summary>
        /// Command which requests the shutdown of the Application
        /// </summary>
        public RelayCommand QuitCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Command which requests the settings Window
        /// </summary>
        public RelayCommand SettingsCommand
        {
            get;
            private set;
        }

        #endregion

        public TaskbarIconViewModel() :
            base()
        {
            QuitCommand = new RelayCommand(ExecuteQuitCommand);
            SettingsCommand = new RelayCommand(ExecuteShowSettingsCommand);
        }

        #region Methods

        #region Command Execution

        /// <summary>
        /// Requests the shutdown of the application
        /// </summary>
        private void ExecuteQuitCommand()
        {
            QuitApplicationRequested(this, EventArgs.Empty);
        }

        /// <summary>
        /// Requests the settings Window to be shown
        /// </summary>
        private void ExecuteShowSettingsCommand()
        {
            ShowSettingsRequested(this, EventArgs.Empty);
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Event raised when the shutdown of the application is requested
        /// </summary>
        public event EventHandler QuitApplicationRequested = delegate { };

        /// <summary>
        /// Event raised when the settings Window is requested to be shown
        /// </summary>
        public event EventHandler ShowSettingsRequested = delegate { };

        #endregion
    }
}
