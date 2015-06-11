using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring.V1
{
    class V1State : IScoringState
    {

        private PlayerState _Player1;
        public PlayerState Player1
        {
            get
            {
                return _Player1;
            }
        }

        private PlayerState _Player2;
        public PlayerState Player2
        {
            get
            {
                return _Player2;
            }
        }

        public int Player1Set
        {
            get
            {
                return _Player1.Set;
            }
        }
        public int Player1Game
        {
            get
            {
                return _Player1.Game;
            }
        }
        public PointType Player1Point
        {
            get
            {
                return _Player1.Point;
            }
        }

        public int Player2Set
        {
            get
            {
                return _Player2.Set;
            }
        }
        public int Player2Game
        {
            get
            {
                return _Player2.Game;
            }
        }
        public PointType Player2Point
        {
            get
            {
                return _Player2.Point;
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
            _Player1 = new PlayerState(0, 0, new PointType(false, PointType.Zero));
            _Player2 = new PlayerState(0, 0, new PointType(false, PointType.Zero));
            _IsInTieBreak = false;
            _IsTieBreakEnabled = false;
        }

        public V1State(PlayerState NewPlayer1, PlayerState NewPlayer2, bool NewIsInTieBreak, bool NewIsTieBreakEnabled)
        {
            _Player1 = NewPlayer1;
            _Player2 = NewPlayer2;
            _IsInTieBreak = NewIsInTieBreak;
            _IsTieBreakEnabled = NewIsTieBreakEnabled;
        }

        public override string ToString()
        {
            return string.Format("{0} : {1}", _Player1, _Player2);
        }

    }
}
