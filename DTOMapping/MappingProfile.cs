using AutoMapper;
using NationalPark2._0.Models;
using NationalPark2._0.Models.DTO;

namespace NationalPark2._0.DTOMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalPark,NationalParkDTO>().ReverseMap();
            CreateMap<Trail,TrailDTO>().ReverseMap();
        }
    }
}
