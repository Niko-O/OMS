using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring
{
    public interface IScoringStrategy
    {

        IScoringState InitialState { get; }
        bool CanProcess(IScoringState PreviousState, ScoringStrategyAction Action);
        IScoringState Process(IScoringState PreviousState, ScoringStrategyAction Action);

    }
}
