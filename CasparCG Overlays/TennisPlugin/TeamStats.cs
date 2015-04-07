using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisPlugin
{
    class TeamStats
    {

        public enum PointsValue
        {
            Zero = 0,
            Fifteen = 1,
            Thirty = 2,
            Fourty = 3
        }

        public PointsValue Points { get; set; }
        public int Games { get; set; }
        public int Sets { get; set; }

        public String PointsAsString
        {
            get
            {
                switch (Points)
                {
                    case PointsValue.Zero:
                        return "0";
                    case PointsValue.Fifteen:
                        return "15";
                    case PointsValue.Thirty:
                        return "30";
                    case PointsValue.Fourty:
                        return "40";
                }
                throw new OnUtils.NopeException();
            }
        }

        public TeamStats()
        {
            Points = PointsValue.Zero;
            Games = 0;
            Sets = 0;
        }

        public void IncrementPoints()
        {
            if (Points == PointsValue.Fourty)
            {
                Points = PointsValue.Zero;
                IncrementGames();
            }
            else
            {
                Points++;
            }
        }

        public void IncrementGames()
        {
            if (Games == 6)
            {
                Games = 0;
                Sets++;
            }
            else
            {
                Games++;
            }
        }

        public void DecrementPoints()
        {
            if (Points == PointsValue.Zero)
            {
                Points = PointsValue.Fourty;
                DecrementGames();
            }
            else
            {
                Points--;
            }
        }

        public void DecrementGames()
        {
            if (Games == 0)
            {
                Games = 6;
                Sets--;
            }
            else
            {
                Games--;
            }
        }

    }
}
