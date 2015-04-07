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
        void SetTeamNameOne(String NewName);
        void SetTeamNameTwo(String NewName);
        void SetPointsOne(String NewPoints);
        void SetPointsTwo(String NewPoints);
        void SetGamesOne(int NewGames);
        void SetGamesTwo(int NewGames);
        void SetSetsOne(int NewSets);
        void SetSetsTwo(int NewSets);
    }
}
