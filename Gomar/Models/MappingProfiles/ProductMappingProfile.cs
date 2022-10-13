using AutoMapper;
using Gomar.Models.ViewModels;

namespace Gomar.Models.MappingProfiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
