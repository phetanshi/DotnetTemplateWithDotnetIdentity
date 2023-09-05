using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Default
{
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAppConfigService _appConfigService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public DefaultAuthorizationHandler(IWebHostEnvironment env, IAppConfigService appConfigService, IUserService userService, IUserRoleService userRoleService)
        {
            _env = env;
            this._appConfigService = appConfigService;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultAuthorizationRequirement requirement)
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
                    await context.WindowsAuthentication(requirement, _userService, IsActiveEmployee);
                }
                else
                {
                    await context.AzureAdAuthentication(requirement, _userService, IsActiveEmployee);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsActiveEmployee(int userId)
        {
            UserReadDto user = await _userService.GetAsync(userId);
            return user?.IsActive ?? false;
        }
    }
}
