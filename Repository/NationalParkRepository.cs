using NationalPark_Front.Repository;
using NationalParkFrontEnd.Models;
using NationalParkFrontEnd.Repository.IRepository;

namespace NationalParkFrontEnd.Repository
{
    public class NationalParkRepository : Repository<NationalPark>, INationalParkRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NationalParkRepository(IHttpClientFactory httpClientFactory):base(httpClientFactory)
        {
                _httpClientFactory = httpClientFactory;
        }
    }
}
