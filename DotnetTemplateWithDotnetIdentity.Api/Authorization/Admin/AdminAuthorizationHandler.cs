using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Admin
{
    public class AdminAuthorizationHandler : AuthorizationHandler<AdminAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRoleService _userRoleService;
        private readonly IAppConfigService _appConfigService;

        public AdminAuthorizationHandler(IWebHostEnvironment env, IUserService userService, UserManager<AppUser> userManager, IUserRoleService userRoleService, IAppConfigService appConfigService)
        {
            _env = env;
            this._userService = userService;
            _userManager = userManager;
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
                    await context.WindowsAuthentication(requirement, IsSuperAdmin);
                }
                else
                {
                    await context.BearerTokenAuthentication(requirement, _userService, IsSuperAdmin);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsSuperAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            var claims = await _userManager.GetClaimsAsync(user);

            var roleClaim = claims.FirstOrDefault(x => (x.Type == AppClaimTypes.ROLE_CLAIM_TYPE
                                                        && (x.Value == AppClaimTypes.ADMIN_ROLE_CLAIM)));

            if (roleClaim == null)
                return false;

            return true;
        }
    }
}
