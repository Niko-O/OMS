using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    class ServerStoredTennisTemplate : ITennisTemplate
    {

        private CasparServerCommands.TlsCommand.TemplatePath _Path;
        public CasparServerCommands.TlsCommand.TemplatePath Path
        {
            get
            {
                return _Path;
            }
        }

        public ServerStoredTennisTemplate(CasparServerCommands.TlsCommand.TemplatePath NewPath)
        {
            _Path = NewPath;
        }
        public string AdditionalParameters
        {
            get { return null; }
        }

        public int ChannelId
        {
            get { return 1; }
        }

        public string Clip
        {
            get { return _Path.FullPath; }
        }

        public string DisplayName
        {
            get { return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_Path.FileNameWithoutExtension.ToLower()) + " (" + (_Path.Directory ?? "<Root>") + ")"; }
        }

        public int? Layer
        {
            get { return 1; }
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
