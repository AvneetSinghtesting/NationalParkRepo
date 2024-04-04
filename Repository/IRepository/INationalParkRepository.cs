using NationalPark2._0.Models;

namespace NationalPark2._0.Repository.IRepository
{
    public interface INationalParkRepository
    {
        bool CreateNationalPark(NationalPark nationalPark);
        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);   
        NationalPark GetNationalPark(int nationalParkId);
        ICollection<NationalPark> GetAllNationalParks();
        bool Save();
        bool nationalParkExists(int nationalParkId);
        bool nationalParkExists(string Name);
    }
}
