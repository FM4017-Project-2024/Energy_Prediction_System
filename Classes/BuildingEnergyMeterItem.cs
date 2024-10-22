using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy_Prediction_System.Classes
{
    public class BuildingEnergyMeterItem
    {
        public long Id { get; set; }
        public float EnergyMeter1 { get; set; }
        public string? EnergyMeterUoM { get; set; }
        public DateTime EnergyMeterDateTime { get; set; }
    }
}
