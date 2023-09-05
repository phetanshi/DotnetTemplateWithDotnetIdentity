using DotnetTemplateWithDotnetIdentity.Api.Areas.Identity.Data;
using DotnetTemplateWithDotnetIdentity.Api.Authorization;
using DotnetTemplateWithDotnetIdentity.Api.Identity;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DotnetTemplateWithDotnetIdentity.Api.AppStart
{
    public static class AppIdentity
    {
        public static WebApplicationBuilder AddAppIdentity(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("AppDbConnection") ?? throw new InvalidOperationException("Connection string 'AppDbConnection' not found.");

            builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityContext>();

            builder.Services.AddIdentityServer()
                .AddApiAuthorization<AppUser, AppIdentityContext>()
                .AddProfileService<AppProfileService>();
                
            builder.Services.AddScoped<IProfileService, AppProfileService>();

            return builder;
        }
    }
}
