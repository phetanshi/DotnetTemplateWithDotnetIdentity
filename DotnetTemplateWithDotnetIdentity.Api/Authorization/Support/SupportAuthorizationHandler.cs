using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Support
{
    public class SupportAuthorizationHandler : AuthorizationHandler<SupportAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IAppConfigService _appConfigService;

        public SupportAuthorizationHandler(IWebHostEnvironment env, IUserService userService, IUserRoleService userRoleService, IAppConfigService appConfigService)
        {
            _env = env;
            _userService = userService;
            _appConfigService = appConfigService;
            _userRoleService = userRoleService;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SupportAuthorizationRequirement requirement)
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
                    await context.WindowsAuthentication(requirement, _userService, IsSupportUser);
                }
                else
                {
                    await context.AzureAdAuthentication(requirement, _userService, IsSupportUser);
                }

                //if (_env.EnvironmentName.ToLower() == "development")
                //{
                //    await context.WindowsAuthentication(requirement, _userService, IsSupportUser);
                //}
                //else
                //{
                //    await context.AzureAdAuthentication(requirement, _userService, IsSupportUser);
                //}
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsSupportUser(int userId)
        {
            List<UserRoleMappingDto> config = await _userRoleService.GetAsync(userId);
            return config.FirstOrDefault(x => x.UserId == userId && (x.AppRoleId == AppRoleEnum.Support
                                                                    || x.AppRoleId == AppRoleEnum.Admin)) == null ? false : true;
        }
    }
}
