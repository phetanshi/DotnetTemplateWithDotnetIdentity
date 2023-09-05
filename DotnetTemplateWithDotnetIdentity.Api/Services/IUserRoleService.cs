using Application.Dtos;

namespace DotnetTemplateWithDotnetIdentity.Api.Services
{
    public interface IUserRoleService
    {
        Task<List<UserRoleMappingDto>> GetAsync(int userId);
        Task<UserRoleMappingDto> CreateAsync(UserRoleMappingCreateDeleteDto userRole);
        Task<UserRoleMappingDto> UpdateAsync(UserRoleMappingDto userRole);
        Task<bool> DeleteAsync(UserRoleMappingCreateDeleteDto userRole);

    }
}
