using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    interface ITennisTemplate : PluginInterfaces.ITemplate
    {
        void ShowScoreboard();
        void HideScoreboard();
        void SetPlayer1Name(String NewName);

    }
}
