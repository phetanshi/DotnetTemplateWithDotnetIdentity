using Application.Dtos;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;

namespace DotnetTemplateWithDotnetIdentity.Api.AutoMapperProfiles
{
    public class AppConfigAutoMapperProfile : Profile
    {
        public AppConfigAutoMapperProfile()
        {
            CreateMap<AppConfig, AppConfigDto>().ReverseMap();
            CreateMap<AppConfigCreateDto, AppConfig>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true)); //While create, it will be true only
        }
    }
}
