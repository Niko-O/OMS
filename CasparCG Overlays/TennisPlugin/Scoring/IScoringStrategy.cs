using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring
{
    public interface IScoringStrategy
    {

        IScoringState InitialState { get; }
        IScoringState Process(IScoringState PreviousState, ScoringStrategyAction Action);

    }
}
