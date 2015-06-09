using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;

namespace TennisPlugin.Scoring.V1
{
    class TennisScoringStrategyV1 : IScoringStrategy
    {

        public IScoringState InitialState
        {
            get
            {
                return new V1State();
            }
        }

        public IScoringState Process(IScoringState _PreviousState, ScoringStrategyAction Action)
        {
            var PreviousState = (V1State)_PreviousState;

            PlayerState WinningPlayer;
            PlayerState LosingPlayer;
            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                    WinningPlayer = PreviousState.Player1;
                    LosingPlayer = PreviousState.Player2;
                    break;
                case ScoringStrategyAction.Player2Scored:
                    WinningPlayer = PreviousState.Player2;
                    LosingPlayer = PreviousState.Player1;
                    break;
                default:
                    throw new NopeException();
            }

            switch (PreviousState.Player1Point)
            {
                case PointType.Fourty:
                    if (PreviousState.Player2Point == PointType.Advantage)
                    {
                        LosingPlayer.Point = PointType.Fourty;
                    }
                    else
                    {
                        WinningPlayer.Point = PointType.Advantage;
                    }
                    break;
                case PointType.Advantage:
                    WinningPlayer.Point = PointType.Zero;
                    LosingPlayer.Point = PointType.Zero;
                    if (PreviousState.Player1Game >= 6 && PreviousState.Player1Game == PreviousState.Player2Game + 1)
                    {
                        WinningPlayer.Game = 0;
                        LosingPlayer.Game = 0;
                        WinningPlayer.Set = PreviousState.Player1Set + 1;
                    }
                    else
                    {
                        WinningPlayer.Game = PreviousState.Player2Game + 1;
                    }
                    break;
                default:
                    WinningPlayer.Point = PreviousState.Player1Point + 1;
                    break;
            }

            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                    return new V1State(WinningPlayer, LosingPlayer);
                case ScoringStrategyAction.Player2Scored:
                    return new V1State(LosingPlayer, WinningPlayer);
                default:
                    throw new NopeException();
            }
        }

    }
}
