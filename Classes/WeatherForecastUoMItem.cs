using System.ComponentModel.DataAnnotations;

namespace Energy_Prediction_System.Classes
{
    public class WeatherForecastUoMItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Attribute { get; set; }

        [Required]
        [MaxLength(10)]
        public string UoM { get; set; }
    }
}
