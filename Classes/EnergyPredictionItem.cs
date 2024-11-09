using System;
using System.ComponentModel.DataAnnotations;

namespace Energy_Prediction_System.Classes
{
    public class EnergyPredictionItem
    {
        public long Id { get; set; }

        public float EnergyPrediction { get; set; }

        public string EnergyPredictionUoM { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime ExecuteTime { get; set; } = DateTime.Now;
    }
}
