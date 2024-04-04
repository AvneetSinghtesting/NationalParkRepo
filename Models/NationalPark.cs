using Microsoft.VisualBasic;

namespace NationalPark2._0.Models
{
    public class NationalPark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established {  get; set; }
        public byte[] Picture { get; set; }

    }
}
