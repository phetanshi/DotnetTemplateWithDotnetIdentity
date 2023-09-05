using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Admin
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IAppConfigService _appConfigService;

        public AdminAuthorizationHandler(IWebHostEnvironment env, IUserService userService, IUserRoleService userRoleService, IAppConfigService appConfigService)
        {
            _env = env;
            _userService = userService;
            _userRoleService = userRoleService;
            _appConfigService = appConfigService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAuthorizationRequirement requirement)
        {
            try
            {
                if (!context?.User?.Identity.IsAuthenticated ?? false)
                {
                    context.Fail();
                    return;
                }

                if (context.User.Identity.AuthenticationType == "NTLM")
                {
                    await context.WindowsAuthentication(requirement, _userService, IsSuperAdmin);
                }
                else
                {
                    await context.AzureAdAuthentication(requirement, _userService, IsSuperAdmin);
                }

                //if (_env.EnvironmentName.ToLower() == "development")
                //{
                //    await context.WindowsAuthentication(requirement, _userService, IsSuperAdmin);
                //}
                //else
                //{
                //    await context.AzureAdAuthentication(requirement, _userService, IsSuperAdmin);
                //}
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsSuperAdmin(int userId)
        {
            List<UserRoleMappingDto> config = await _userRoleService.GetAsync(userId);
            return config.FirstOrDefault(x => x.UserId == userId && x.AppRoleId == AppRoleEnum.Admin) == null ? false : true;
        }
    }
}
