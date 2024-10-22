using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy_Prediction_System.Classes
{
    public class BuildingTemperatureItem
    {
        public long Id { get; set; }
        public float Temp1 { get; set; }
        public float Temp2 { get; set; }
        public float Temp3 { get; set; }
        public float Temp4 { get; set; }
        public float Temp5 { get; set; }
        public string? TempUoM { get; set; }
        public DateTime? TempDateTime { get; set; }
    }
}
