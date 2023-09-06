using Application.Dtos;
using Application.Dtos.Enum;
using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization.Support
{
    public class SupportAuthorizationHandler : AuthorizationHandler<SupportAuthorizationRequirement>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRoleService _userRoleService;
        private readonly IAppConfigService _appConfigService;

        public SupportAuthorizationHandler(IWebHostEnvironment env, IUserService userService, UserManager<AppUser> userManager, IUserRoleService userRoleService, IAppConfigService appConfigService)
        {
            _env = env;
            this._userService = userService;
            _userManager = userManager;
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
                    await context.WindowsAuthentication(requirement, IsSupportUser);
                }
                else
                {
                    await context.BearerTokenAuthentication(requirement, _userService, IsSupportUser);
                }
            }
            catch (Exception ex)
            {
                context.Fail();
                throw;
            }
        }

        private async Task<bool> IsSupportUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return false;

            var claims = await _userManager.GetClaimsAsync(user);

            var roleClaim = claims.FirstOrDefault(x => (x.Type == AppClaimTypes.ROLE_CLAIM_TYPE 
                                                        && (x.Value == AppClaimTypes.SUPPORT_ROLE_CLAIM 
                                                                || x.Value == AppClaimTypes.ADMIN_ROLE_CLAIM)));

            if (roleClaim == null)
                return false;

            return true;
        }
    }
}
