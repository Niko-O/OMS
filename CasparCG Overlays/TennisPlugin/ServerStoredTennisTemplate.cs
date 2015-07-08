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

        public void SetPlayerNames(string NameOne, string NameTwo)
        {
            ExecuteCallCommand("setPlayerNames", NameOne, NameTwo);
        }

        public void SetPlayerServe(TennisTemplateServe PlayerServe)
        {
            ExecuteCallCommand("serve", (int)PlayerServe);
        }

        public void SetStandings(string SetsOne, string GamesOne, string PointsOne, string SetsTwo, string GamesTwo, string PointsTwo)
        {
            ExecuteCallCommand("setStandings", SetsOne, GamesOne, PointsOne, SetsTwo, GamesTwo, PointsTwo);
        }


        public void ShowLowerThird()
        {
            ExecuteCallCommand("showLowerThird");
        }

        public void HideLowerThird()
        {
            ExecuteCallCommand("hideLowerThird");
        }

        public void SetLowerThirdText(string Title, string Subtitle)
        {
            ExecuteCallCommand("setLowerThirdText", Title, Subtitle);
        }


        public void ShowCrawlerTop()
        {
            ExecuteCallCommand("showCrawlerTop");
        }

        public void HideCrawlerTop()
        {
            ExecuteCallCommand("hideCrawlerTop");
        }
        

        public void ShowCrawlerBottom()
        {
            ExecuteCallCommand("showCrawlerBottom");
        }

        public void HideCrawlerBottom()
        {
            ExecuteCallCommand("hideCrawlerBottom");
        }

        public void SetCrawlerText(string Text)
        {
            ExecuteCallCommand("setCrawlerText", Text);
        }


        private void ExecuteCallCommand(string MethodName, params object[] Parameters)
        {
            PluginInterfaces.PublicProviders.CasparServer.ExecuteCommand(new CasparServerCommands.CallCommand(ChannelId, Layer, MethodName, Parameters));
        }

    }
}
