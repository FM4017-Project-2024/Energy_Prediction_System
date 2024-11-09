using System.ComponentModel.DataAnnotations;

namespace Energy_Prediction_System.Classes
{
    public class WeatherForecastUoMItem
    {
        public int Id { get; set; }
        public string Attribute { get; set; }
        public string UoM { get; set; }
    }
}
