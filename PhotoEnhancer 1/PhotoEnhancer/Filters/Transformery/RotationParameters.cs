using System;

namespace PhotoEnhancer
{
    public class RotationParameters : IParameters
    {
        public double AngleInDegrees { get; set; }

        public ParameterInfo[] GetDescription()
        {
            return new[]
            {
                new ParameterInfo() {
                    Name = "Угол поворота в °",
                    MinValue = -360,
                    MaxValue = 360,
                    DefaultValue = 0,
                    Increment = 5
                    }
            };
        }

        public void SetValues(double[] values)
        {
            AngleInDegrees = values[0];
        }
    }
}
