using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Input is out of range. The range is between {0} to {1}.", i_MinValue, i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message) : base(i_Message)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}
