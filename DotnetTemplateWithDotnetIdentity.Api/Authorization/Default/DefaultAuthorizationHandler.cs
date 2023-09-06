using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Default
{
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IAppConfigService _appConfigService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public DefaultAuthorizationHandler(IWebHostEnvironment env, IUserService userService, IAppConfigService appConfigService, UserManager<AppUser> userManager, IUserRoleService userRoleService)
        {
            _env = env;
            _userService = userService;
            this._appConfigService = appConfigService;
            this._userManager = userManager;
            this._userRoleService = userRoleService;
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
                    await context.WindowsAuthentication(requirement, IsActiveEmployee);
                }
                else
                {
                    await context.BearerTokenAuthentication(requirement, _userService, IsActiveEmployee);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsActiveEmployee(string email) 
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var isUserLocked = await _userManager.IsLockedOutAsync(user);
            return !isUserLocked;
        }
    }
}
