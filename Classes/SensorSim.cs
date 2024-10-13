using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy_Prediction_System.Classes
{
    public class SensorSim
    {
        private readonly Random _random = new();

        public double GetRandomDouble(double minValue, double maxValue)
        {
            return _random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}