using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin.Scoring.V1
{
    public struct PlayerState
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
