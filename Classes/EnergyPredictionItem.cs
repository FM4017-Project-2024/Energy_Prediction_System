using System;
using System.ComponentModel.DataAnnotations;

namespace Energy_Prediction_System.Classes
{
    public class EnergyPredictionItem
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public float EnergyPrediction { get; set; }

        [Required]
        [MaxLength(10)]
        public string EnergyPredictionUoM { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
