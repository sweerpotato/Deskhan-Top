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

        public RelayCommand QuitCommand
        {
            get;
            private set;
        }

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

        private void ExecuteQuitCommand()
        {
            QuitApplicationRequested(this, EventArgs.Empty);
        }

        private void ExecuteShowSettingsCommand()
        {
            ShowSettingsRequested(this, EventArgs.Empty);
        }

        #endregion

        #endregion

        #region Events

        public event EventHandler QuitApplicationRequested = delegate { };

        public event EventHandler ShowSettingsRequested = delegate { };

        #endregion
    }
}
