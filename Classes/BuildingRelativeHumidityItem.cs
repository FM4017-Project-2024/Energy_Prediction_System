using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy_Prediction_System.Classes
{
    public class BuildingRelativeHumidityItem
    {
        public long Id { get; set; }
        public float RelHumidity1 { get; set; }
        public float RelHumidity2 { get; set; }
        public float RelHumidity3 { get; set; }
        public float RelHumidity4 { get; set; }
        public string? RelHumidityUoM { get; set; }
        public DateTime RelHumidityDateTime { get; set; }
    }
}
