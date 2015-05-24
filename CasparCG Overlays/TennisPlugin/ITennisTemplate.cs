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
        
        void SetTeamNameOne(string NewName);
        void SetTeamNameTwo(string NewName);
        void SetPointsOne(string NewPoints);
        void SetPointsTwo(string NewPoints);
        void SetGamesOne(int NewGames);
        void SetGamesTwo(int NewGames);
        void SetSetsOne(int NewSets);
        void SetSetsTwo(int NewSets);

        void ShowLowerThird();
        void HideLowerThird();
        void SetLowerThirdText(string Text);
        void SetLowerThirdVisibilityDuration(int Milliseconds);
        void SetLowerThirdEffects(bool IsScrolling);
    }
}
