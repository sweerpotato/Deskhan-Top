using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using DeskhanTop.Commands;
using DeskhanTop.Keyboard;
using DeskhanTop.Models;
using DeskhanTop.Views;

namespace DeskhanTop.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties and Fields

        /// <summary>
        /// Lazy instantiation of this class, triggered by the DataContext binding in xaml
        /// </summary>
        private static readonly Lazy<MainViewModel> _Instance = new Lazy<MainViewModel>(() => new MainViewModel());

        /// <summary>
        /// Retrieves the instance of this class
        /// </summary>
        public static MainViewModel Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        private ApplicationStates _ApplicationState = ApplicationStates.Initializing;
        public ApplicationStates ApplicationState
        {
            get
            {
                return _ApplicationState;
            }
            set
            {
                SetField(ref _ApplicationState, value);
            }
        }

        private KeyboardListener _KeyboardListener = new KeyboardListener();

        /// <summary>
        /// ViewModel of the Taskbar Icon
        /// </summary>
        public TaskbarIconViewModel TaskbarIconVM
        {
            get;
            private set;
        }

        /// <summary>
        /// ViewModel of the Settings
        /// </summary>
        public SettingsViewModel SettingsVM
        {
            get;
            private set;
        } = null;

        private SettingsModel _SettingsModel = new SettingsModel();

        #endregion

        #region Constructor

        private MainViewModel()
            : base()
        {
            TaskbarIconVM = new TaskbarIconViewModel();

            TaskbarIconVM.QuitApplicationRequested += ApplicationExitRequested;
            TaskbarIconVM.ShowSettingsRequested += SettingsWindowRequested;

            HotkeyManager.HotkeyPressed += OnHotkeyPressed;
            HotkeyManager.RegisterHotkey(new[] { Key.LeftCtrl, Key.D4 });

            KeyboardListener.KeyUp += OnKeyboardKeyUp;
            KeyboardListener.KeyDown += OnKeyboardKeyDown;

            ApplicationState = ApplicationStates.Main;
        }

        #endregion

        #region Methods

        #region Command Execution

        private void ExecutePrintScreenCommand()
        {

        }

        #endregion

        #endregion

        #region Event Handlers

        private void OnKeyboardKeyDown(object sender, RawKeyEventArgs e)
        {
            if (ApplicationState != ApplicationStates.Main)
            {
                return;
            }

            HotkeyManager.Press(e.Key);
        }

        private void OnKeyboardKeyUp(object sender, RawKeyEventArgs e)
        {
            if (ApplicationState != ApplicationStates.Main)
            {
                return;
            }

            HotkeyManager.Release(e.Key);
        }

        /// <summary>
        /// Event handler for the HotkeyPressed event from HotkeyManager
        /// </summary>
        private void OnHotkeyPressed(object sender, EventArgs e)
        {
            Console.WriteLine("ACTUALLY WORKED");
        }

        /// <summary>
        /// Event handler which triggers the shutdown of the application
        /// </summary>
        /// <param name="sender">The event's sender</param>
        /// <param name="e">EventArgs, ignored</param>
        private void ApplicationExitRequested(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Event handler which requests the application to create an instance of SettingsView
        /// </summary>
        /// <param name="sender">The event's sender, ignored</param>
        /// <param name="e">EventArgs, ignored</param>
        private void SettingsWindowRequested(object sender, EventArgs e)
        {
            App.Mediator.RequestWindow(new SettingsViewModel(_SettingsModel), typeof(SettingsView));
        }

        #endregion
    }
}
