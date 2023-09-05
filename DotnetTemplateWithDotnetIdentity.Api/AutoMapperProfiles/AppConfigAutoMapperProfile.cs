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
            CreateMap<AppConfigCreateDto, AppConfig>();
        }
    }
}
