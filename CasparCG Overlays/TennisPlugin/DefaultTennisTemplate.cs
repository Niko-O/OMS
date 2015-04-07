using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    class DefaultTennisTemplate : ITennisTemplate
    {

        public String DisplayName
        {
            get { return "Standard Tennis-Template"; }
        }

        public int ChannelId
        {
            get { return 1; }
        }

        public string Clip
        {
            get { return "html"; }
        }

        public int? Layer
        {
            get { return 1; }
        }

        public string Parameters
        {
            get { return "file:///bla/tennis.html"; }
        }

        public void ShowScoreboard()
        {
            throw new NotImplementedException();
        }

        public void HideScoreboard()
        {
            throw new NotImplementedException();
        }

        public void SetPlayer1Name(string NewName)
        {
            //PluginInterfaces.PublicProviders.CasparServer.SendCommand(new CallCommand("setPlayer1Name(" + NewName + ")"));
        }

    }
}
