using Application.Dtos;
using Application.Dtos.Enum;
using AutoMapper;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.EntityFrameworkCore;
using Ps.EfCoreRepository.SqlServer;
using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Services.Definitions
{
    public class RoleMenuService : ServiceBase, IRoleMenuService
    {
        public RoleMenuService(IRepository repository, ILogger<RoleMenuService> logger, IConfiguration config, IMapper mapper, IHttpContextAccessor context) : base(repository, logger, config, mapper, context)
        {
        }
        public async Task<List<MenuGroupDto>> GetRoleMenusAsync(AppRoleEnum appRole)
        {
            return await GetMenuRoleMapping(new List<int> { (int)appRole });
        }

        public async Task<List<MenuGroupDto>> GetLoginUserMenuAsync()
        {
            if (AppHttpContext == null
                || AppHttpContext.HttpContext == null
                || AppHttpContext.HttpContext.User == null)
                throw new InvalidOperationException("User context not found");

            AppRoleEnum appRole = AppRoleEnum.None;
            List<int> roles = new List<int>();

            Claim appUserRoleClaim = AppHttpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimHelper.APP_USER_ROLE_KEY);
            if (appUserRoleClaim == null)
            {
                Claim userIdClaim = AppHttpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimHelper.USER_ID_KEY);

                if (userIdClaim == null)
                    throw new Exception("User id not found in the token");

                var userRoles = await Repository.GetListAsync<AppUserRoleMapping>(x => x.UserId == Convert.ToInt32(userIdClaim.Value));

                roles.AddRange(userRoles.Select(x => x.AppRoleId).ToList());
            }
            else
            {
                roles.Add(Convert.ToInt32(appUserRoleClaim.Value));
            }

            return await GetMenuRoleMapping(roles);
        }

        private async Task<List<MenuGroupDto>> GetMenuRoleMapping(List<int> roles)
        {
            var appRoleMenuMappings = await Repository.GetList<AppRoleMenuMapping>(x => roles.Contains((x.AppRoleId ?? 0))).ToListAsync();
            var menuGroups = await Repository.GetList<AppMenuGroup>(x => x.IsActive).Include(s => s.AppMenuItems).ToListAsync();

            var result = from roleMenu in appRoleMenuMappings
                         join menu in menuGroups on roleMenu.AppMenuGroupId equals menu.AppMenuGroupId
                         select new MenuGroupDto
                         {
                             AppMenuGroupName = menu.AppMenuGroupName,
                             IsActive = menu.IsActive,
                             AppRoleId = roleMenu.AppRoleId.HasValue
                                                    ? (AppRoleEnum)roleMenu.AppRoleId.Value
                                                    : AppRoleEnum.None,
                             MenuItems = Mapper.Map<List<MenuItemDto>>(menu.AppMenuItems.Where(s => s.IsActive).AsEnumerable())
                         };

            return result.ToList();
        }

        public async Task<AppRoleMenuMappingDto> CreateAsync(RoleManuMappingCreateDeleteDto roleMenu)
        {
            var obj = Mapper.Map<AppRoleMenuMapping>(roleMenu);
            await Repository.CreateAsync(obj);
            return Mapper.Map<AppRoleMenuMappingDto>(obj);
        }

        public async Task<bool> DeleteAsync(RoleManuMappingCreateDeleteDto roleMenu)
        {
            var obj = Mapper.Map<AppRoleMenuMapping>(roleMenu);
            await Repository.DeleteAsync<AppRoleMenuMapping>(x => x.AppMenuGroupId == roleMenu.AppMenuGroupId
                                                                    && x.AppRoleId == roleMenu.AppRoleId);
            return true;
        }




    }
}
