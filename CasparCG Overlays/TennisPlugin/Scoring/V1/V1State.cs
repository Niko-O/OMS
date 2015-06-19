using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;

namespace TennisPlugin.Scoring.V1
{
    class V1State : IScoringState
    {

        private PlayerState Player1;
        private PlayerState Player2;

        public int Player1Set
        {
            get
            {
                return Player1.Set;
            }
        }
        public int Player1Game
        {
            get
            {
                return Player1.Game;
            }
        }
        public PointType Player1Point
        {
            get
            {
                return Player1.Point;
            }
        }

        public int Player2Set
        {
            get
            {
                return Player2.Set;
            }
        }
        public int Player2Game
        {
            get
            {
                return Player2.Game;
            }
        }
        public PointType Player2Point
        {
            get
            {
                return Player2.Point;
            }
        }

        private bool _IsInTieBreak;
        public bool IsInTieBreak
        {
            get
            {
                return _IsInTieBreak;
            }
        }

        private bool _IsTieBreakEnabled;
        public bool IsTieBreakEnabled
        {
            get
            {
                return _IsTieBreakEnabled;
            }
        }

        public V1State()
        {
            Player1 = new PlayerState(0, 0, new PointType(false, PointType.Zero));
            Player2 = new PlayerState(0, 0, new PointType(false, PointType.Zero));
            _IsInTieBreak = false;
            _IsTieBreakEnabled = false;
        }

        private V1State(PlayerState NewPlayer1, PlayerState NewPlayer2, bool NewIsInTieBreak, bool NewIsTieBreakEnabled)
        {
            Player1 = NewPlayer1;
            Player2 = NewPlayer2;
            _IsInTieBreak = NewIsInTieBreak;
            _IsTieBreakEnabled = NewIsTieBreakEnabled;
        }

        public bool CanProcess(ScoringStrategyAction Action)
        {
            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player2Scored:
                    return true;
                case ScoringStrategyAction.Player1Reduced:
                    return Player1.Point.Value > 0;
                case ScoringStrategyAction.Player2Reduced:
                    return Player2.Point.Value > 0;
                case ScoringStrategyAction.EnableTieBreak:
                    if (_IsTieBreakEnabled) return false;
                    if (_IsInTieBreak) return false;
                    if (Player1.Point.Value != 0) return false;
                    if (Player2.Point.Value != 0) return false;
                    if (Player1.Game > 6 || Player2.Game > 6) return false;
                    return true;
                case ScoringStrategyAction.DisableTieBreak:
                    if (!_IsTieBreakEnabled) return false;
                    if (_IsInTieBreak) return false;
                    if (Player1.Point.Value != 0) return false;
                    if (Player2.Point.Value != 0) return false;
                    return true;
                default:
                    throw new NopeException();
            }
        }

        public IScoringState Process(ScoringStrategyAction Action)
        {
            if (!CanProcess(Action))
            {
                throw new InvalidOperationException();
            }

            switch (Action)
            {
                case ScoringStrategyAction.EnableTieBreak:
                    return new V1State(Player1, Player2, _IsInTieBreak, true);
                case ScoringStrategyAction.DisableTieBreak:
                    return new V1State(Player1, Player2, _IsInTieBreak, false);
            }

            PlayerState AffectedPlayer;
            PlayerState OtherPlayer;
            switch (Action)
            {
                case ScoringStrategyAction.Player1Scored:
                case ScoringStrategyAction.Player1Reduced:
                    AffectedPlayer = Player1;
                    OtherPlayer = Player2;
                    break;
                case ScoringStrategyAction.Player2Scored:
                case ScoringStrategyAction.Player2Reduced:
                    AffectedPlayer = Player2;
                    OtherPlayer = Player1;
                    break;
                default:
                    throw new NopeException();
            }
            bool IsInTieBreak = _IsInTieBreak;
            bool IsTieBreaEnabled = IsTieBreakEnabled;


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

                if (_IsInTieBreak)
                {
                    SetCompleted();
                }
                else
                {
                    if (_IsTieBreakEnabled)
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
                        if (AffectedPlayer.Game >= 5 && AffectedPlayer.Game >= OtherPlayer.Game + 1)
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
                    if (_IsInTieBreak)
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
                                if (_IsTieBreakEnabled && AffectedPlayer.Game == 6 && OtherPlayer.Game == 6)
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

        public override string ToString()
        {
            return string.Format("{0} : {1}", Player1, Player2);
        }

        private struct PlayerState
        {

            public int Set { get; set; }
            public int Game { get; set; }
            public PointType Point { get; set; }

            public PlayerState(int NewSet, int NewGame, PointType NewPoint)
                : this()
            {
                Set = NewSet;
                Game = NewGame;
                Point = NewPoint;
            }

            public override string ToString()
            {
                return string.Format("{0}-{1}-{2}", Set, Game, Point);
            }

        }

    }
}
