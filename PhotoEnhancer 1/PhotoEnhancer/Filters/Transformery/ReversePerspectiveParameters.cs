using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoEnhancer
{
    class ReversePerspectiveParameters : IParameters
    {
        public double CuttedPart { get; set; }

        public ParameterInfo[] GetDescription()
        {
            return new[]
            {
                new ParameterInfo() {
                    Name = "Процент сужения",
                    MinValue = 0,
                    MaxValue = 100,
                    DefaultValue = 100,
                    Increment = 5
                    }
            };
        }

        public void SetValues(double[] values)
        {
            CuttedPart = values[0];
        }
    }
}

