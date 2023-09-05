using Application.Dtos;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Ps.EfCoreRepository.SqlServer;

namespace DotnetTemplateWithDotnetIdentity.Api.Services.Definitions
{
    public class UserRoleService : ServiceBase, IUserRoleService
    {
        public UserRoleService(IRepository repository, ILogger<UserRoleService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }

        public async Task<List<UserRoleMappingDto>> GetAsync(int userId)
        {
            var roleMappings = await Repository.GetListAsync<AppUserRoleMapping>(x => x.UserId == userId);
            var dto = Mapper.Map<List<UserRoleMappingDto>>(roleMappings);
            return dto;
        }

        public async Task<UserRoleMappingDto> CreateAsync(UserRoleMappingCreateDeleteDto userRole)
        {
            var obj = Mapper.Map<AppUserRoleMapping>(userRole);
            await Repository.CreateAsync(obj);
            return Mapper.Map<UserRoleMappingDto>(obj);
        }

        public async Task<UserRoleMappingDto> UpdateAsync(UserRoleMappingDto userRole)
        {
            var obj = Mapper.Map<AppUserRoleMapping>(userRole);
            await Repository.UpdateAsync(obj);
            return Mapper.Map<UserRoleMappingDto>(obj);
        }

        public async Task<bool> DeleteAsync(UserRoleMappingCreateDeleteDto userRole)
        {
            await Repository.DeleteAsync<AppUserRoleMapping>(x => x.UserId == userRole.UserId && x.AppRoleId == (int)userRole.AppRoleId);
            return true;
        }
    }
}
