using System;
using DeskhanTop.Commands;
using DeskhanTop.Models;

namespace DeskhanTop.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Properties and Fields

        private SettingsModel _SettingsModel = null;

        private string _WindowTitle = String.Empty;
        public string WindowTitle
        {
            get
            {
                return _WindowTitle;
            }
            set
            {
                SetField(ref _WindowTitle, value);
            }
        }

        #region Commands

        public RelayCommand<SettingsViewModel> WindowClosingCommand
        {
            get;
            private set;
        }

        #endregion

        #endregion
        
        #region Constructor

        public SettingsViewModel(SettingsModel settingsModel)
        {
            _SettingsModel = settingsModel;
            WindowTitle = "Settings";
            WindowClosingCommand = new RelayCommand<SettingsViewModel>(
                ExecuteWindowClosingCommand,
                () => { return true; });
        }

        #endregion

        #region Command Execution

        private void ExecuteWindowClosingCommand()
        {
            if (MainViewModel.Instance.ApplicationState == ApplicationStates.DisplayingSettings)
            {
                MainViewModel.Instance.ApplicationState = ApplicationStates.Main;
            }
        }

        #endregion
    }
}
