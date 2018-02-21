using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeskhanTop.Models;
using MVMBase;

namespace DeskhanTop.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
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

        public SettingsViewModel(SettingsModel settingsModel)
        {
            _SettingsModel = settingsModel;
            WindowTitle = "This is Window Test";
        }
    }
}
