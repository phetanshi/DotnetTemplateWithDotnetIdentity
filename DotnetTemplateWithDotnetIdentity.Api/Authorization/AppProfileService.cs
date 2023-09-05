using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Data.Models;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DotnetTemplateWithDotnetIdentity.Api.Authorization
{
    public class AppProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;
        public AppProfileService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            if (user != null)
            {
                // Add first name and last name as claims.
                context.IssuedClaims.Add(new Claim("user_id", user?.UserName ?? "-"));
                context.IssuedClaims.Add(new Claim("email", user?.Email ?? "-"));
                context.IssuedClaims.Add(new Claim("first_name", user?.FirstName ?? user?.UserName ?? user?.Email ?? "Unknown"));
                context.IssuedClaims.Add(new Claim("last_name", user?.LastName ?? ""));
                context.IssuedClaims.Add(new Claim("is_normal_user", IsNormalUser(user).ToString()));
                context.IssuedClaims.Add(new Claim("is_admin_user", IsAdminUser(user).ToString()));
                context.IssuedClaims.Add(new Claim("is_support_user", IsSupportUser(user).ToString()));
            }
        }

        private bool IsNormalUser(AppUser user)
        {
            return true;
        }
        private bool IsAdminUser(AppUser user)
        {
            return false;
        }
        private bool IsSupportUser(AppUser user)
        {
            return false;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
