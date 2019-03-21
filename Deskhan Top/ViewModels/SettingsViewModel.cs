using System;
using DeskhanTop.Commands;
using DeskhanTop.Keyboard;
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

        private string _HotkeyText = String.Empty;
        public string HotkeyText
        {
            get
            {
                return _HotkeyText;
            }
            set
            {
                SetField(ref _HotkeyText, value);
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
            : base()
        {
            _SettingsModel = settingsModel;
            WindowTitle = "Settings";
            WindowClosingCommand = new RelayCommand<SettingsViewModel>(
                ExecuteWindowClosingCommand,
                () => { return true; });

            KeyboardListener.KeyDown += OnKeyboardKeyDown;
            KeyboardListener.KeyUp += OnKeyboardKeyUp;
        }

        private void OnKeyboardKeyUp(object sender, RawKeyEventArgs e)
        {
            
        }

        private void OnKeyboardKeyDown(object sender, RawKeyEventArgs e)
        {
            HotkeyText += e.Key.ToString();
        }

        #endregion

        #region Command Execution

        private void ExecuteWindowClosingCommand()
        {
            KeyboardListener.KeyDown -= OnKeyboardKeyDown;
            KeyboardListener.KeyUp -= OnKeyboardKeyUp;

            if (MainViewModel.Instance.ApplicationState == ApplicationStates.DisplayingSettings)
            {
                MainViewModel.Instance.ApplicationState = ApplicationStates.Main;
            }
        }

        #endregion
    }
}
