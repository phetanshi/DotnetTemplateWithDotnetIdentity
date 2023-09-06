using DotnetTemplateWithDotnetIdentity.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotnetTemplateWithDotnetIdentity.Api.Identity;
using System.Security.Claims;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Data;

namespace DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data
{
    public static class InitalIdentityData
    {
        internal static void SeedIdentity(this IServiceProvider serviceProvider)
        {
            var scope =  serviceProvider.CreateScope();
            var identityDbContext = scope.ServiceProvider.GetService<AppIdentityContext>();
            if (identityDbContext != null)
            {
                identityDbContext.Database.EnsureCreated();

                var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
                if(userManager != null)
                {
                    userManager.SeedAdmin();
                    userManager.SeedSupport();
                }
            }
        }
        internal static void SeedAdmin(this UserManager<AppUser> userManager)
        {
            //Check if it's already seeded
            if (userManager.FindByEmailAsync("ps-admin@padmasekhar.com").Result != null)
                return;

            //Create User
            var user = new AppUser
            {
                UserName = "ps-admin@padmasekhar.com",
                Email = "ps-admin@padmasekhar.com",
                FirstName = "Ps",
                LastName = "Admin",
                EmailConfirmed = true,
            };

            var userResult = userManager.CreateAsync(user, "MySelf%654").Result;
            if (!userResult.Succeeded)
                throw new Exception("Error occured while seeding Admin user");

            //Add superUser  Claim
            var superUserClaim = new Claim(AppClaimTypes.ROLE_CLAIM_TYPE, AppClaimTypes.ADMIN_ROLE_CLAIM);
            var roleResult = userManager.AddClaimAsync(user, superUserClaim).Result;

            if (!roleResult.Succeeded)
                throw new Exception("Error occured while seeding Admin claims");
        }
        internal static void SeedSupport(this UserManager<AppUser> userManager)
        {
            //Check if it's already seeded
            if (userManager.FindByEmailAsync("app-support@padmasekhar.com").Result != null)
                return;

            //Create User
            var user = new AppUser
            {
                UserName = "app-support@padmasekhar.com",
                Email = "app-support@padmasekhar.com",
                FirstName = "Ps",
                LastName = "Support",
                EmailConfirmed = true,
            };

            var userResult = userManager.CreateAsync(user, "MySelf%654").Result;
            if (!userResult.Succeeded)
                throw new Exception("Error occured while seeding Support user");

            //Add superUser  Claim
            var superUserClaim = new Claim(AppClaimTypes.ROLE_CLAIM_TYPE, AppClaimTypes.SUPPORT_ROLE_CLAIM);
            var roleResult = userManager.AddClaimAsync(user, superUserClaim).Result;

            if (!roleResult.Succeeded)
                throw new Exception("Error occured while seeding Support claims");
        }
    }
}
