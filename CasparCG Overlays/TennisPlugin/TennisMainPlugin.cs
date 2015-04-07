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
    class TennisMainPlugin : PluginInterfaces.IPlugin
    {

        public System.Windows.Controls.UserControl GetSnapIn()
        {
            return Singleton<TennisSnapIn>.Instance;
        }

        public void Created()
        {

        }

        public void Enabled()
        {
            
        }

        public void Disabled()
        {
            
        }

        public void Unloaded()
        {
            
        }

    }
}
