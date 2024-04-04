using System.ComponentModel.DataAnnotations.Schema;

namespace NationalPark2._0.Models
{
    public class Trail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Distance {  get; set; }
        public string Elevation {  get; set; }
        public enum DifficultyType { Easy,Moderate,Difficult }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId {  get; set; }
        public NationalPark NationalPark { get; set; }
        public DateTime Created { get; set; }
    }
}
