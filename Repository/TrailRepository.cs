using Microsoft.EntityFrameworkCore;
using NationalPark2._0.Data;
using NationalPark2._0.Models;
using NationalPark2._0.Repository.IRepository;

namespace NationalPark2._0.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateTrail(Trail trail)
        {
            _context.Trailers.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trailers.Remove(trail);
            return Save();
        }

        public Trail GetTrailById(int TrailId)
        {
            return _context.Trailers.FirstOrDefault(i => i.Id == TrailId);
        }

        public Trail GetTrailByName(string TrailName)
        {
            throw new NotImplementedException();
        }

        public ICollection<Trail> GetTrails()
        {
            return _context.Trailers.ToList<Trail>();
        }

        public ICollection<Trail> GetTrailsByNpId(int NpId)
        {
            return _context.Trailers.Include(t=>t.NationalPark).Where(npId=>npId.NationalParkId == NpId).ToList<Trail>();
        }

        public bool Save()
        {
            return _context.SaveChanges()==1?true:false;
        }

        public bool TrailExist(int TrailId)
        {
            return _context.Trailers.Any(e=>e.Id== TrailId);
        }

        public bool TrailExist(string TrailName)
        {
            return _context.Trailers.Any(e=>e.Name== TrailName);
        }

        public bool UpdateTrail(Trail trail)
        {
            _context.Trailers.Update(trail);
            return Save();
        }
    }
}
