using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;
using System.ComponentModel.Composition;

namespace TennisPlugin
{

    [Export(typeof(PluginInterfaces.IPlugin))]
    [ExportMetadata("DisplayName", "Tennis")]
    [ExportMetadata("PluginGuid", "e2067728-d790-4a32-9db8-e771382c4ef5")]
    public class TennisMainPlugin : PluginInterfaces.IPlugin
    {

        private TennisSnapIn SnapIn = null;
        private TennisSettings Settings = null;

        public TennisMainPlugin()
        {
        
        }

        public System.Windows.Controls.UserControl GetSnapIn()
        {
            return SnapIn;
        }

        public void Created()
        {
            Settings = new TennisSettings();
            PluginInterfaces.PublicProviders.PluginSettings.LoadSettings(Settings);
        }

        public void Enabled()
        {
            SnapIn = new TennisSnapIn();
            if (!string.IsNullOrEmpty(Settings.LastTemplateFilter))
            {
                SnapIn.CurrentFilter = Settings.LastTemplateFilter;
            }
        }

        public void Disabled()
        {
            if (SnapIn.CurrentFilter == null)
            {
                Settings.LastTemplateFilter = null;
            }
            else
            {
                Settings.LastTemplateFilter = SnapIn.CurrentFilter;
            }
            SnapIn.Unload();
            SnapIn = null;
        }

        public void Unloaded()
        {
            PluginInterfaces.PublicProviders.PluginSettings.SaveSettings(Settings);
        }

    }
}
