using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    public class SaturationParameters : IParameters
    {
            public double RedChanel { get; set; }
            public double GreenChanel { get; set; }
            public double BlueChanel { get; set; }

        public ParameterInfo[] GetDescription()
            {
                return new[]
                {
                new ParameterInfo() {
                    Name = "R",
                    MinValue = 0,
                    MaxValue = 10,
                    DefaultValue = 1,
                    Increment = 0.05
                    },
                new ParameterInfo() {
                    Name = "G",
                    MinValue = 0,
                    MaxValue = 10,
                    DefaultValue = 1,
                    Increment = 0.05
                    },
                new ParameterInfo() {
                    Name = "B",
                    MinValue = 0,
                    MaxValue = 10,
                    DefaultValue = 1,
                    Increment = 0.05
                    }
                };

        }

            public void SetValues(double[] values)
            {
            RedChanel = values[0];
            GreenChanel = values[1];
            BlueChanel = values[2];
        }
    }
}
