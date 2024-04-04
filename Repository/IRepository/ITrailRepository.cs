using NationalPark2._0.Models;

namespace NationalPark2._0.Repository.IRepository
{
    public interface ITrailRepository
    {
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool TrailExist(int TrailId);
        bool TrailExist(string TrailName);
        ICollection<Trail> GetTrails(); 
        ICollection<Trail> GetTrailsByNpId(int NpId);
        Trail GetTrailById(int TrailId);
        Trail GetTrailByName(string TrailName);
        bool Save();
    }
}
