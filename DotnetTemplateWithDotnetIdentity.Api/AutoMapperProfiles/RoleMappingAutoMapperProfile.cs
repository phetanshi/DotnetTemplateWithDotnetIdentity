using Application.Dtos;
using Application.Dtos.Enum;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;

namespace DotnetTemplateWithDotnetIdentity.Api.AutoMapperProfiles
{
    public class RoleMappingAutoMapperProfile : Profile
    {
        public RoleMappingAutoMapperProfile()
        {
            CreateMap<AppUserRoleMapping, UserRoleMappingDto>()
                .ForMember(dest => dest.AppRoleId, opt => opt.MapFrom(src => (AppRoleEnum)src.AppRoleId))
                .ReverseMap();

            CreateMap<UserRoleMappingCreateDeleteDto, AppUserRoleMapping>()
                .ForMember(dest => dest.AppRoleId, opt => opt.MapFrom(src => (int)src.AppRoleId))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ReverseMap();

            CreateMap<AppMenuGroup, MenuGroupDto>();
            CreateMap<AppMenuItem, MenuItemDto>();
            CreateMap<AppRoleMenuMapping, AppRoleMenuMappingDto>().ReverseMap();

            CreateMap<RoleManuMappingCreateDeleteDto, AppRoleMenuMapping>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<AppRoleMenuMapping, MenuGroupDto>()
                .ForMember(dest => dest.AppRoleId, opt => opt.MapFrom(src => (int)(src.AppRoleId ?? 0)))
                .ReverseMap();

            CreateMap<RoleManuMappingCreateDeleteDto, AppRoleMenuMapping>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
