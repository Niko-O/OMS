using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring
{

    public interface IScoringState
    {
        
        int Player1Set { get; }
        int Player1Game { get; }
        PointType Player1Point { get; }
        int Player2Set { get; }
        int Player2Game { get; }
        PointType Player2Point { get; }

        bool IsInTieBreak { get; }
        bool IsTieBreakEnabled { get; }

    }
}
