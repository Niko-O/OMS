using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;

namespace TennisPlugin
{
    public static class Extensions
    {

        public static String ToDisplayString(this Scoring.PointType Point)
        {
            switch (Point)
            {
                case Scoring.PointType.Zero:
                    return "0";
                case Scoring.PointType.Fifteen:
                    return "15";
                case Scoring.PointType.Thirty:
                    return "30";
                case Scoring.PointType.Fourty:
                    return "40";
                case Scoring.PointType.Advantage:
                    return "AD";
            }
            throw new NopeException();
        }

    }
}
