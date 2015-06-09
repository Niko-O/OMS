using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    public class DefaultTennisTemplate : ITennisTemplate
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
            get { return "[html] file:///C:/CasparCG/CCGS_2.0.7/templates/oms/tennis/sommer/main.html"; }
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
            ExecuteCallCommand("showScoreboard");
        }

        public void HideScoreboard()
        {
            ExecuteCallCommand("hideScoreboard");
        }

        public void SetTeamNameOne(string NewName)
        {
            ExecuteCallCommand("setTeamNameOne", NewName);
        }

        public void SetTeamNameTwo(string NewName)
        {
            ExecuteCallCommand("setTeamNameTwo", NewName);
        }

        public void SetPointsOne(string NewPoints)
        {
            ExecuteCallCommand("setPointsOne", NewPoints);
        }

        public void SetPointsTwo(string NewPoints)
        {
            ExecuteCallCommand("setPointsTwo", NewPoints);
        }

        public void SetGamesOne(int NewGames)
        {
            ExecuteCallCommand("setGamesOne", NewGames);
        }

        public void SetGamesTwo(int NewGames)
        {
            ExecuteCallCommand("setGamesTwo", NewGames);
        }

        public void SetSetsOne(int NewSets)
        {
            ExecuteCallCommand("setSetsOne", NewSets);
        }

        public void SetSetsTwo(int NewSets)
        {
            ExecuteCallCommand("setSetsTwo", NewSets);
        }

        public void ShowLowerThird()
        {
            ExecuteCallCommand("showLowerThird");
        }

        public void HideLowerThird()
        {
            ExecuteCallCommand("hideLowerThird");
        }

        public void SetLowerThirdTitleText(string Text)
        {
            ExecuteCallCommand("setLowerThirdTitleText", Text);
        }

        public void SetLowerThirdSubtitleText(string Text)
        {
            ExecuteCallCommand("setLowerThirdSubtitleText", Text);
        }

        public void SetLowerThirdEffects(bool IsScrolling)
        {
            ExecuteCallCommand("setLowerThirdEffects", IsScrolling);
        }

        private void ExecuteCallCommand(string MethodName, params object[] Parameters)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, MethodName, Parameters));
        }

    }
}
