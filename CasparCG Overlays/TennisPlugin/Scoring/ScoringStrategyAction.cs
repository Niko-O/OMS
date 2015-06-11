using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring
{
    public enum ScoringStrategyAction : int
    {
        Player1Scored = 1,
        Player2Scored = 2,
        Player1Reduced = 3,
        Player2Reduced = 4,
        EnableTieBreak = 5,
        DisableTieBreak = 6
    }
}
