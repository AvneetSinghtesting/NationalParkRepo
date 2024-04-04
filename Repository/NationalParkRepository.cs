using NationalPark2._0.Data;
using NationalPark2._0.Models;
using NationalPark2._0.Repository.IRepository;

namespace NationalPark2._0.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetAllNationalParks()
        {
            return _context.NationalParks.ToList();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            //return _context.NationalParks.Where(d => d.Id == nationalParkId).FirstOrDefault(); OR
            return _context.NationalParks.FirstOrDefault(d => d.Id == nationalParkId);
        }

        public bool nationalParkExists(int nationalParkId)
        {
            //return _context.NationalParks.Find(nationalParkId)!=null?true:false; OR
            return _context.NationalParks.Any(d=>d.Id==nationalParkId);
        }

        public bool nationalParkExists(string Name)
        {
            return _context.NationalParks.Any(d=>d.Name==Name);
        }

        public bool Save()
        {
            return _context.SaveChanges()==1?true:false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
