using System.ComponentModel.DataAnnotations;

namespace HotelsApp.Data.Models
{
    public class Hotel
    {
        #region Properties
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public double Distance { get; set; }
        #endregion
    }
}
