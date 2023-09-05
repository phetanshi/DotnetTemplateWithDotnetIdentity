using Application.Dtos;
using Application.Dtos.Enum;

namespace DotnetTemplateWithDotnetIdentity.Api.Services
{
    public interface IRoleMenuService
    {
        Task<List<MenuGroupDto>> GetLoginUserMenuAsync();
        Task<List<MenuGroupDto>> GetRoleMenusAsync(AppRoleEnum appRole);
        Task<AppRoleMenuMappingDto> CreateAsync(RoleManuMappingCreateDeleteDto roleMenu);
        Task<bool> DeleteAsync(RoleManuMappingCreateDeleteDto roleMenu);
    }
}
