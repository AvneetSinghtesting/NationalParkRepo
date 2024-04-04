using System.ComponentModel.DataAnnotations.Schema;

namespace NationalPark2._0.Models.DTO
{
    public class TrailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Distance { get; set; }
        public string Elevation { get; set; }
        public enum Difficulty { Easy, Moderate, Difficult }
        public Difficulty difficulty { get; set; }
        public int NationalParkId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
        public NationalPark nationalPark { get; set; }
    }
}
