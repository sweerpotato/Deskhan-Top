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

        private static readonly Lazy<MainViewModel> _Instance = new Lazy<MainViewModel>(() => new MainViewModel());

        public static MainViewModel Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        public TaskbarIconViewModel TaskbarIconVM
        {
            get;
            private set;
        }

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

        private void ApplicationExitRequested(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SettingsWindowRequested(object sender, EventArgs e)
        {
            App.Mediator.RequestWindow(SettingsVM, typeof(SettingsView));
        }

        #endregion
    }
}
