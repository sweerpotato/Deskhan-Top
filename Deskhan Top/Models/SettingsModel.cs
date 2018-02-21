using System.Xml.Linq;

namespace DeskhanTop.Models
{
    public class SettingsModel
    {
        private XDocument _SettingsDocument = null;

        public SettingsModel(XDocument settingsDocument = null)
        {
            _SettingsDocument = settingsDocument ?? new XDocument();
        }
    }
}
