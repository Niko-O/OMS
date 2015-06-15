using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    public interface ITennisTemplate : PluginInterfaces.ITemplate
    {
        void ShowScoreboard();
        void HideScoreboard();
        void SetPlayerNames(string NameOne, string NameTwo);
        void SetPlayerServe(TennisTemplateServe PlayerServe);
        void SetStandings(string SetsOne, string GamesOne, string PointsOne, string SetsTwo, string GamesTwo, string PointsTwo);

        void ShowLowerThird();
        void HideLowerThird();
        void SetLowerThirdText(string Title, string Subtitle);
        
        void ShowCrawlerTop();
        void HideCrawlerTop();
        void SetCrawlerTextTop(string Text);

        void ShowCrawlerBottom();
        void HideCrawlerBottom();
        void SetCrawlerTextBottom(string Text);
    }
}
