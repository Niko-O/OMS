using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    public class TennisSettings : PluginSettings.SettingsStructure
    {

        private PluginSettings.SettingsProperty<string> _LastTemplateFilter;
        public string LastTemplateFilter
        {
            get
            {
                return _LastTemplateFilter.Value;
            }
            set
            {
                _LastTemplateFilter.Value = value;
            }
        }

        public TennisSettings()
        {
            _LastTemplateFilter = RegisterProperty<string>("LastTemplateFilter");
        }

    }
}
