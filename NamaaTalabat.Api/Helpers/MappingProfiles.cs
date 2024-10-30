using AutoMapper;
using NamaaTalabat.Api.DTOS;
using NamaaTalabat.Core.Entities;

namespace NamaaTalabat.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        

        public MappingProfiles() {

           
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.Brand, O => O.MapFrom(o => o.Brand.Name))
                .ForMember(p => p.Category , o => o.MapFrom(o => o.Category.Name))
                .ForMember(p=>p.PictureUrl , o => o.MapFrom<PirctureUrlResolver>())
                .ReverseMap();
        }
    }
}
