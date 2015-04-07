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
            get { return "[html] file:///C:/CasparCG/templates_tennis/tennis.html"; }
        }

        public int? Layer
        {
            get { return 1; }
        }

        public string AdditionalParameters
        {
            get { return null; }
        }

        public void ShowScoreboard()
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "showScoreboard"));
        }

        public void HideScoreboard()
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "hideScoreboard"));
        }

        public void SetTeamNameOne(string NewName)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setTeamNameOne", NewName));
        }

        public void SetTeamNameTwo(string NewName)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setTeamNameTwo", NewName));
        }

        public void SetPointsOne(string NewPoints)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setPointsOne", NewPoints));
        }

        public void SetPointsTwo(string NewPoints)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setPointsTwo", NewPoints));
        }

        public void SetGamesOne(int NewGames)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setGamesOne", NewGames));
        }

        public void SetGamesTwo(int NewGames)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setGamesTwo", NewGames));
        }

        public void SetSetsOne(int NewSets)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setSetsOne", NewSets));
        }

        public void SetSetsTwo(int NewSets)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, "setSetsTwo", NewSets));
        }
    }
}
