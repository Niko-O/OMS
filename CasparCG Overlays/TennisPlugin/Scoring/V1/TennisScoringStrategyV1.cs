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

        public bool CanProcess(IScoringState _PreviousState, ScoringStrategyAction Action)
        {
            var PreviousState = (V1State)_PreviousState;

            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player2Scored:
                    return true;
                case ScoringStrategyAction.Player1Reduced:
                    return PreviousState.Player1Point.Value > 0;
                case ScoringStrategyAction.Player2Reduced:
                    return PreviousState.Player2Point.Value > 0;
                case ScoringStrategyAction.EnableTieBreak:
                    if (PreviousState.IsTieBreakEnabled) return false;
                    if (PreviousState.IsInTieBreak) return false;
                    if (PreviousState.Player1.Point.Value != 0) return false;
                    if (PreviousState.Player2.Point.Value != 0) return false;
                    if (IsTieBreakConflict(PreviousState)) return false;
                    return true;
                case ScoringStrategyAction.DisableTieBreak:
                    if (!PreviousState.IsTieBreakEnabled) return false;
                    if (PreviousState.IsInTieBreak) return false;
                    if (PreviousState.Player1.Point.Value != 0) return false;
                    if (PreviousState.Player2.Point.Value != 0) return false;
                    return true;
                default:
                    throw new NopeException();
            }
        }

        private bool IsTieBreakConflict(V1State PreviousState)
        {
            return PreviousState.Player1.Game > 6 || PreviousState.Player2.Game > 6;
        }

        public IScoringState Process(IScoringState _PreviousState, ScoringStrategyAction Action)
        {
            var PreviousState = (V1State)_PreviousState;

            if (!CanProcess(PreviousState, Action))
            {
                throw new InvalidOperationException();
            }

            switch (Action)
            {
                case ScoringStrategyAction.EnableTieBreak:
                    return new V1State(PreviousState.Player1, PreviousState.Player2, PreviousState.IsInTieBreak, true);
                case ScoringStrategyAction.DisableTieBreak:
                    return new V1State(PreviousState.Player1, PreviousState.Player2, PreviousState.IsInTieBreak, false);
            }

            PlayerState AffectedPlayer;
            PlayerState OtherPlayer;
            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player1Reduced:
                    AffectedPlayer = PreviousState.Player1;
                    OtherPlayer = PreviousState.Player2;
                    break;
                case ScoringStrategyAction.Player2Scored:
                case ScoringStrategyAction.Player2Reduced:
                    AffectedPlayer = PreviousState.Player2;
                    OtherPlayer = PreviousState.Player1;
                    break;
                default:
                    throw new NopeException();
            }
            bool IsInTieBreak = PreviousState.IsInTieBreak;
            bool IsTieBreaEnabled = PreviousState.IsTieBreakEnabled;


            Action SetCompleted = delegate
            {
                AffectedPlayer.Game = 0;
                OtherPlayer.Game = 0;
                AffectedPlayer.Set = AffectedPlayer.Set + 1;
            };

            Action GameCompleted = delegate
            {
                IsInTieBreak = false;
                AffectedPlayer.Point = new PointType(false, PointType.Zero);
                OtherPlayer.Point = new PointType(false, PointType.Zero);

                if (PreviousState.IsInTieBreak)
                {
                    SetCompleted();
                }
                else
                {
                    if (PreviousState.IsTieBreakEnabled)
                    {
                        if (AffectedPlayer.Game == 6 && OtherPlayer.Game == 6)
                        {
                            SetCompleted();
                        }
                        else
                        {
                            AffectedPlayer.Game = AffectedPlayer.Game + 1;
                        }
                    }
                    else
                    {
                        if (AffectedPlayer.Game >= 6 && AffectedPlayer.Game >= OtherPlayer.Game + 1)
                        {
                            SetCompleted();
                        }
                        else
                        {
                            AffectedPlayer.Game = AffectedPlayer.Game + 1;
                        }
                    }
                }

            };

            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player2Scored:
                    if (PreviousState.IsInTieBreak)
                    {
                        if (AffectedPlayer.Point.Value >= 6 && AffectedPlayer.Point.Value >= OtherPlayer.Point.Value + 1)
                        {
                            GameCompleted();
                        }
                        else
                        {
                            AffectedPlayer.Point = new PointType(true, AffectedPlayer.Point.Value + 1);
                        }
                    }
                    else
                    {
                        switch (AffectedPlayer.Point.Value)
                        {
                            case PointType.Fourty:
                                switch (OtherPlayer.Point.Value)
                                {
                                    case PointType.Advantage:
                                        OtherPlayer.Point = new PointType(false, PointType.Fourty);
                                        break;
                                    case PointType.Fourty:
                                        AffectedPlayer.Point = new PointType(false, PointType.Advantage);
                                        break;
                                    default:
                                        GameCompleted();
                                        break;
                                }
                                break;
                            case PointType.Advantage:
                                GameCompleted();
                                break;
                            default:
                                if (PreviousState.IsTieBreakEnabled && AffectedPlayer.Game == 6 && OtherPlayer.Game == 6)
                                {
                                    IsInTieBreak = true;
                                    AffectedPlayer.Point = new PointType(true, AffectedPlayer.Point.Value + 1);
                                }
                                else
                                {
                                    AffectedPlayer.Point = new PointType(false, AffectedPlayer.Point.Value + 1);
                                }
                                break;
                        }
                    }
                    break;

                case ScoringStrategyAction.Player1Reduced:
                case ScoringStrategyAction.Player2Reduced:
                    AffectedPlayer.Point = new PointType(false, Math.Max(AffectedPlayer.Point.Value - 1, 0));
                    break;
            }


            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player1Reduced:
                    return new V1State(AffectedPlayer, OtherPlayer, IsInTieBreak, IsTieBreaEnabled);
                case ScoringStrategyAction.Player2Scored:
                case ScoringStrategyAction.Player2Reduced:
                    return new V1State(OtherPlayer, AffectedPlayer, IsInTieBreak, IsTieBreaEnabled);
                default:
                    throw new NopeException();
            }
        }

    }
}
