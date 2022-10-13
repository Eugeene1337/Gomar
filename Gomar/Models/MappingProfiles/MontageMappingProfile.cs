using AutoMapper;
using Gomar.Models.ViewModels;

namespace Gomar.Models.MappingProfiles
{
    public class MontageMappingProfile : Profile
    {
        public MontageMappingProfile()
        {
            CreateMap<Montage, MontageViewModel>().ReverseMap();
        }
    }
}
