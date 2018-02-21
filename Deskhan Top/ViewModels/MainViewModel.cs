using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using DeskhanTop.Models;
using DeskhanTop.Views;
using MVMBase;

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
        }

        #endregion

        #region Constructor

        private MainViewModel() :
            base()
        {
            TaskbarIconVM = new TaskbarIconViewModel();
            SettingsVM = new SettingsViewModel(new SettingsModel());
            TaskbarIconVM.QuitApplicationRequested += ApplicationExitRequested;
            TaskbarIconVM.ShowSettingsRequested += SettingsWindowRequested;
        }

        #endregion

        #region Event Handlers

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
            App.Mediator.RequestWindow(SettingsVM, typeof(SettingsView));
        }

        #endregion
    }
}
