using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnUtils;
using OnUtils.Extensions;

namespace TennisPlugin.Scoring
{
    public struct PointType
    {

        public const int Zero = 0;
        public const int Fifteen = 1;
        public const int Thirty = 2;
        public const int Fourty = 3;
        public const int Advantage = 4;

        private bool _IsTieBreakPoint;
        public bool IsTieBreakPoint
        {
            get
            {
                return _IsTieBreakPoint;
            }
        }

        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
        }

        public PointType(bool NewIsTieBreakPoint, int NewValue)
        {
            if (NewIsTieBreakPoint)
            {
                if (NewValue < 0)
                {
                    throw new ArgumentOutOfRangeException("NewValue", NewValue, "NewValue muss positiv sein.");
                }
            }
            else
            {
                if (NewValue < Zero || NewValue > Advantage)
                {
                    throw new ArgumentOutOfRangeException("NewValue", NewValue, "NewValue muss zwischen PointType.Zero und PointType.Advantage (" + Zero + " und " + Advantage + ") liegen.");
                }
            }
            _IsTieBreakPoint = NewIsTieBreakPoint;
            _Value = NewValue;
        }

        public override string ToString()
        {
            if (_IsTieBreakPoint)
            {
                return _Value.ToString();
            }
            else
            {
                switch (_Value)
                {
                    case Zero:
                        return "0";
                    case Fifteen:
                        return "15";
                    case Thirty:
                        return "30";
                    case Fourty:
                        return "40";
                    case Advantage:
                        return "AD";
                    default:
                        throw new NopeException();
                }
            }
        }

    }
}
